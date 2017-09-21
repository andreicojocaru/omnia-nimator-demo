using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nimator.Plugins.Couchbase.Models;
using Nimator.Plugins.Couchbase.Models.Raw;

namespace Nimator.Plugins.Couchbase
{
    public class CouchbaseDataRetriever : ICouchbaseDataRetriever
    {
        private const string DefaultPoolName = "default";

        private readonly CouchbaseDataRetrieverSettings _settings;
        private readonly HttpClient _httpClient;

        public CouchbaseDataRetriever(CouchbaseDataRetrieverSettings settings)
        {
            _settings = settings;
            _httpClient = new HttpClient();

            if (settings.Credentials != null && settings.Credentials.NotEmpty)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", settings.Credentials.Base64String);
            }
        }

        public async Task<bool> CheckServerAvailabilityAsync()
        {
            var response = await _httpClient.GetAsync(_settings.ServerUrl);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CheckPoolAvailabilityAsync(string poolName = null)
        {
            var url = $"{_settings.ServerUrl}/pools/{poolName ?? DefaultPoolName}";
            var response = await _httpClient.GetAsync(url);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CheckBucketAvailabilityAsync(string bucketName, string poolName = null)
        {
            var url = $"{_settings.ServerUrl}/pools/{poolName ?? DefaultPoolName}/buckets/{bucketName}";
            var response = await _httpClient.GetAsync(url);
            return response.IsSuccessStatusCode;
        }

        public async Task<MemoryUsageModel> RetrieveMemoryUsageAsync(string bucketName, string poolName = null)
        {
            var url = $"{_settings.ServerUrl}/pools/{poolName ?? DefaultPoolName}/buckets/{bucketName}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var rawModel = JsonConvert.DeserializeObject<RawBucketMemory>(content);

            var node = rawModel.Nodes.First();

            var model = new MemoryUsageModel
            {
                Used = node.SystemStats.Mem_Total - node.SystemStats.Mem_Free,
                Total = node.SystemStats.Mem_Total
            };

            return model;
        }

        public async Task<BucketSizeModel> RetrieveBucketRecordsAsync(string bucketName, string poolName = null)
        {
            var url = $"{_settings.ServerUrl}/pools/{poolName ?? DefaultPoolName}/buckets/{bucketName}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var rawModel = JsonConvert.DeserializeObject<RawBucketUsage>(content);

            return new BucketSizeModel
            {
                Total = rawModel.BasicStats.ItemCount
            };
        }
    }
}

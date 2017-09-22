using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nimator.Plugins.Couchbase.Models;
using Nimator.Plugins.Couchbase.Models.Raw;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class MemoryUsageCheck : ICheck
    {
        private readonly CouchbaseMemoryUsageSettings _settings;

        public MemoryUsageCheck(CouchbaseMemoryUsageSettings settings)
        {
            if (settings == null || settings.AreBasicSettingsEmpty)
            {
                throw new ArgumentException(nameof(settings));
            }

            _settings = settings;
        }

        public async Task<ICheckResult> RunAsync()
        {
            using (var httpClient = HttpClientFactory.GetAuthorizedHttpClient(_settings.Credentials))
            {
                var url = $"{_settings.ServerUrl}/pools/{_settings.PoolName}/buckets/{_settings.BucketName}";
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var rawModel = JsonConvert.DeserializeObject<RawBucketMemory>(content);

                var node = rawModel.Nodes.First();

                var model = new MemoryUsageModel
                {
                    Used = node.SystemStats.Mem_Total - node.SystemStats.Mem_Free,
                    Total = node.SystemStats.Mem_Total
                };

                NotificationLevel level = NotificationLevel.Okay;

                if (model.PercentageAvailable < _settings.AvailableMemoryThresholdPercentage)
                {
                    level = NotificationLevel.Warning;
                }

                return new CheckResult(ShortName, level);
            }
        }

        public string ShortName => nameof(MemoryUsageCheck);
    }
}

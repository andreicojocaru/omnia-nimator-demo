using System;
using System.Threading.Tasks;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class PoolAvailabilityCheck : ICheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CouchbaseClusterSettings _settings;

        public PoolAvailabilityCheck(IHttpClientFactory httpClientFactory, CouchbaseClusterSettings settings)
        {
            if (settings == null || settings.AreBasicSettingsEmpty)
            {
                throw new ArgumentException(nameof(_settings));
            }

            _httpClientFactory = httpClientFactory;
            _settings = settings;
        }

        public async Task<ICheckResult> RunAsync()
        {
            using (var httpClient = _httpClientFactory.GetHttpClient())
            {
                var url = $"{_settings.ServerUrl}/pools/{_settings.PoolName}";

                var response = await httpClient.GetAsync(url);
                return new CheckResult(ShortName, response.IsSuccessStatusCode ? NotificationLevel.Okay : NotificationLevel.Critical);
            }
        }

        public string ShortName => nameof(PoolAvailabilityCheck);
    }
}

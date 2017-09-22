using System;
using System.Threading.Tasks;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class ServerAvailabilityCheck : ICheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CouchbaseClusterSettings _settings;

        public ServerAvailabilityCheck(IHttpClientFactory httpClientFactory, CouchbaseClusterSettings settings)
        {
            if (settings == null || settings.AreBasicSettingsEmpty)
            {
                throw new ArgumentException(nameof(settings));
            }

            _httpClientFactory = httpClientFactory;
            _settings = settings;
        }

        public async Task<ICheckResult> RunAsync()
        {
            using (var httpClient = _httpClientFactory.GetHttpClient())
            {
                var response = await httpClient.GetAsync(_settings.ServerUrl);
                return new CheckResult(ShortName, response.IsSuccessStatusCode ? NotificationLevel.Okay : NotificationLevel.Critical);
            }
        }

        public string ShortName => nameof(ServerAvailabilityCheck);
    }
}

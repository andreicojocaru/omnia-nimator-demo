using System;
using System.Threading.Tasks;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class PoolAvailabilityCheck : ICheck
    {
        private readonly CouchbaseClusterSettings _settings;

        public PoolAvailabilityCheck(CouchbaseClusterSettings settings)
        {
            if (settings == null || settings.AreBasicSettingsEmpty)
            {
                throw new ArgumentException(nameof(_settings));
            }

            _settings = settings;
        }

        public async Task<ICheckResult> RunAsync()
        {
            using (var httpClient = HttpClientFactory.GetAuthorizedHttpClient(_settings.Credentials))
            {
                var url = $"{_settings.ServerUrl}/pools/{_settings.PoolName}";

                var response = await httpClient.GetAsync(url);
                return new CheckResult(ShortName, response.IsSuccessStatusCode ? NotificationLevel.Okay : NotificationLevel.Critical);
            }
        }

        public string ShortName => nameof(PoolAvailabilityCheck);
    }
}

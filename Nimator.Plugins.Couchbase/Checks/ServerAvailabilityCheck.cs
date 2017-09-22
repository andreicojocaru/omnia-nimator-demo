using System;
using System.Threading.Tasks;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class ServerAvailabilityCheck : ICheck
    {
        private readonly CouchbaseClusterSettings _settings;

        public ServerAvailabilityCheck(CouchbaseClusterSettings settings)
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
                var response = await httpClient.GetAsync(_settings.ServerUrl);
                return new CheckResult(ShortName, response.IsSuccessStatusCode ? NotificationLevel.Okay : NotificationLevel.Critical);
            }
        }

        public string ShortName => nameof(ServerAvailabilityCheck);
    }
}

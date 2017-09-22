using System;
using System.Threading.Tasks;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class BucketAvailabilityCheck : ICheck
    {
        private readonly CouchbaseClusterSettings _settings;

        public BucketAvailabilityCheck(CouchbaseClusterSettings settings)
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
                var url = $"{_settings.ServerUrl}/pools/{_settings.PoolName}/buckets/{_settings.BucketName}";

                var response = await httpClient.GetAsync(url);
                return new CheckResult(ShortName, response.IsSuccessStatusCode ? NotificationLevel.Okay : NotificationLevel.Critical);
            }
        }

        public string ShortName => nameof(BucketAvailabilityCheck);
    }
}

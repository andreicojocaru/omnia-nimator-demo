﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nimator.Plugins.Couchbase.Models.Raw;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class BucketSizeCheck : ICheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CouchbaseBucketSizeSettings _settings;

        public BucketSizeCheck(IHttpClientFactory httpClientFactory, CouchbaseBucketSizeSettings settings)
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
                var url = $"{_settings.ServerUrl}/pools/{_settings.PoolName}/buckets/{_settings.BucketName}";
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var rawModel = JsonConvert.DeserializeObject<RawBucketUsage>(content);

                var level = NotificationLevel.Okay;

                if (rawModel.BasicStats.ItemCount > _settings.MaxRecords)
                {
                    level = NotificationLevel.Warning;
                }

                return new CheckResult(ShortName, level);
            }
        }

        public string ShortName => nameof(BucketSizeCheck);
    }
}

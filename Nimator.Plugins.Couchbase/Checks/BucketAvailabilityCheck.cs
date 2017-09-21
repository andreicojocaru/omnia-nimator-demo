using System.Threading.Tasks;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class BucketAvailabilityCheck : ICheck
    {
        private readonly ICouchbaseDataRetriever _dataRetriever;

        private readonly string _bucketName;
        private readonly string _poolName;

        public BucketAvailabilityCheck(ICouchbaseDataRetriever dataRetriever, string bucketName, string poolName)
        {
            _dataRetriever = dataRetriever;

            _bucketName = bucketName;
            _poolName = poolName;
        }

        public async Task<ICheckResult> RunAsync()
        {
            var isAvailable = await _dataRetriever.CheckBucketAvailabilityAsync(_bucketName, _poolName);
            return new CheckResult(ShortName, isAvailable ? NotificationLevel.Okay : NotificationLevel.Critical);
        }

        public string ShortName => nameof(BucketAvailabilityCheck);
    }
}

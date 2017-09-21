using System.Threading.Tasks;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class BucketSizeCheck : ICheck
    {
        private readonly ICouchbaseDataRetriever _dataRetriever;
        private readonly int _maxRecords;

        private readonly string _bucketName;
        private readonly string _poolName;

        public BucketSizeCheck(ICouchbaseDataRetriever dataRetriever, int maxRecords, string bucketName, string poolName)
        {
            _dataRetriever = dataRetriever;
            _maxRecords = maxRecords;

            _bucketName = bucketName;
            _poolName = poolName;
        }

        public async Task<ICheckResult> RunAsync()
        {
            var sizeModel = await _dataRetriever.RetrieveBucketRecordsAsync(_bucketName, _poolName);

            NotificationLevel level = NotificationLevel.Okay;

            if (sizeModel.Total > _maxRecords)
            {
                level = NotificationLevel.Warning;
            }

            return new CheckResult(ShortName, level);
        }

        public string ShortName => nameof(BucketSizeCheck);
    }
}

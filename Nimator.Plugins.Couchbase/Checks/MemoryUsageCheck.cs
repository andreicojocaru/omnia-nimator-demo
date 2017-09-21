using System.Threading.Tasks;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class MemoryUsageCheck : ICheck
    {
        private readonly ICouchbaseDataRetriever _dataRetriever;
        private readonly int _threshold;

        private readonly string _bucketName;
        private readonly string _poolName;

        public MemoryUsageCheck(ICouchbaseDataRetriever dataRetriever, int threshold, string bucketName, string poolName)
        {
            _dataRetriever = dataRetriever;
            _threshold = threshold;

            _bucketName = bucketName;
            _poolName = poolName;
        }

        public async Task<ICheckResult> RunAsync()
        {
            var model = await _dataRetriever.RetrieveMemoryUsageAsync(_bucketName, _poolName);

            NotificationLevel level = NotificationLevel.Okay;

            if (model.PercentageAvailable < _threshold)
            {
                level = NotificationLevel.Warning;
            }

            return new CheckResult(ShortName, level);
        }

        public string ShortName => nameof(MemoryUsageCheck);
    }
}

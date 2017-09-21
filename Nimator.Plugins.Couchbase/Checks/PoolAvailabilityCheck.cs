using System.Threading.Tasks;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class PoolAvailabilityCheck : ICheck
    {
        private readonly ICouchbaseDataRetriever _dataRetriever;
        private readonly string _poolName;

        public PoolAvailabilityCheck(ICouchbaseDataRetriever dataRetriever, string poolName)
        {
            _dataRetriever = dataRetriever;
            _poolName = poolName;
        }

        public async Task<ICheckResult> RunAsync()
        {
            var isAvailable = await _dataRetriever.CheckPoolAvailabilityAsync(_poolName);
            return new CheckResult(ShortName, isAvailable ? NotificationLevel.Okay : NotificationLevel.Critical);
        }

        public string ShortName => nameof(PoolAvailabilityCheck);
    }
}

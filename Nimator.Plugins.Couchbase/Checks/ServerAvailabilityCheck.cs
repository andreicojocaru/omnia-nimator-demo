using System.Threading.Tasks;

namespace Nimator.Plugins.Couchbase.Checks
{
    public class ServerAvailabilityCheck : ICheck
    {
        private readonly ICouchbaseDataRetriever _dataRetriever;

        public ServerAvailabilityCheck(ICouchbaseDataRetriever dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        public async Task<ICheckResult> RunAsync()
        {
            var isAvailable = await _dataRetriever.CheckServerAvailabilityAsync();
            return new CheckResult(ShortName, isAvailable ? NotificationLevel.Okay : NotificationLevel.Critical);
        }

        public string ShortName => nameof(ServerAvailabilityCheck);
    }
}

using Nimator.Plugins.Couchbase.Checks;
using Nimator.Plugins.Couchbase.Models;

namespace Nimator.Plugins.Couchbase.Settings
{
    public class ServerAvailabilityCheckSettings : ICheckSettings
    {
        public string ServerUrl { get; set; }

        public BasicAuthorizationCredentials Credentials { get; set; }

        public ICheck ToCheck()
        {
            var settings = new CouchbaseDataRetrieverSettings
            {
                Credentials = Credentials,
                ServerUrl = ServerUrl
            };

            return new ServerAvailabilityCheck(new CouchbaseDataRetriever(settings));
        }
    }
}

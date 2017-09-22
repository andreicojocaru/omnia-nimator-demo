using Nimator.Plugins.Couchbase.Checks;
using Nimator.Plugins.Couchbase.Models.Credentials;
using Nimator.Plugins.Couchbase.Models.Settings;

namespace Nimator.Plugins.Couchbase.Settings
{
    public class ServerAvailabilityCheckSettings : ICheckSettings
    {
        public string ServerUrl { get; set; }

        public BasicAuthorizationCredentials Credentials { get; set; }

        public ICheck ToCheck()
        {
            var settings = new CouchbaseClusterSettings
            {
                ServerUrl = ServerUrl
            };

            return new ServerAvailabilityCheck(new AuthorizedHttpClientFactory(Credentials), settings);
        }
    }
}

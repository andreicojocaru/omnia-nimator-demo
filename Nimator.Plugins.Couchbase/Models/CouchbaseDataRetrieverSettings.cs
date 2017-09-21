namespace Nimator.Plugins.Couchbase.Models
{
    public class CouchbaseDataRetrieverSettings
    {
        public string ServerUrl { get; set; }

        public BasicAuthorizationCredentials Credentials { get; set; }
    }
}

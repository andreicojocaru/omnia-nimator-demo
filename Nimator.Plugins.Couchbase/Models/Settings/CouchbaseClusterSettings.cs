namespace Nimator.Plugins.Couchbase.Models.Settings
{
    public class CouchbaseClusterSettings
    {
        public string ServerUrl { get; set; }

        public BasicAuthorizationCredentials Credentials { get; set; }

        public string PoolName { get; set; } = "default";

        public string BucketName { get; set; } = "default";

        public bool AreBasicSettingsEmpty => string.IsNullOrEmpty(ServerUrl) || string.IsNullOrEmpty(PoolName) || string.IsNullOrEmpty(BucketName);
    }
}

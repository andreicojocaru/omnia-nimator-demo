namespace Nimator.Plugins.Couchbase.Tests
{
    internal static class MockRemoteJsonData
    {
        public static string GetRawBucketUsageJson(int itemCount)
        {
            return $"{{\"BasicStats\": {{\"ItemCount\": \"{itemCount}\"}}}}";
        }

        public static string GetRawMemoryUsageJson(int total, int free)
        {
            return $"{{\"Nodes\": [{{\"SystemStats\": {{\"mem_total\": {total}, \"mem_free\": {free}}} }}]}}";
        }
    }
}

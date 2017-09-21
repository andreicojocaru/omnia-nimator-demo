using System.Threading.Tasks;
using Nimator.Plugins.Couchbase.Models;

namespace Nimator.Plugins.Couchbase
{
    public interface ICouchbaseDataRetriever
    {
        Task<bool> CheckServerAvailabilityAsync();

        Task<bool> CheckPoolAvailabilityAsync(string poolName = null);

        Task<bool> CheckBucketAvailabilityAsync(string bucketName, string poolName = null);

        Task<MemoryUsageModel> RetrieveMemoryUsageAsync(string bucketName, string poolName = null);

        Task<BucketSizeModel> RetrieveBucketRecordsAsync(string bucketName, string poolName = null);
    }
}
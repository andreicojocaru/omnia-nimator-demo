using System.Net.Http;

namespace Nimator.Plugins.Couchbase
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}
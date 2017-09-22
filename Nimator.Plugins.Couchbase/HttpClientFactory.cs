using System.Net.Http;
using System.Net.Http.Headers;
using Nimator.Plugins.Couchbase.Models;

namespace Nimator.Plugins.Couchbase
{
    static class HttpClientFactory
    {
        public static HttpClient GetAuthorizedHttpClient(BasicAuthorizationCredentials credentials)
        {
            var client = new HttpClient();

            if (credentials != null && credentials.NotEmpty)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials.Base64String);
            }

            return client;
        }
    }
}

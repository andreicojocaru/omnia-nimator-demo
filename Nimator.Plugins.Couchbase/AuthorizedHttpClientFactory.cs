using System.Net.Http;
using System.Net.Http.Headers;
using Nimator.Plugins.Couchbase.Models.Credentials;

namespace Nimator.Plugins.Couchbase
{
    public class AuthorizedHttpClientFactory : IHttpClientFactory
    {
        private readonly IAuthorizationCredentials _credentials;

        public AuthorizedHttpClientFactory(IAuthorizationCredentials credentials)
        {
            _credentials = credentials;
        }

        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();

            if (_credentials != null && _credentials.NotEmpty)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_credentials.AuthorizationHeaderKey, _credentials.AuthorizationHeaderValue);
            }

            return client;
        }
    }
}

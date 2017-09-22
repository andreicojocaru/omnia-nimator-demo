using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nimator.Plugins.Couchbase.Tests
{
    public class MockHttpClientHandler : HttpClientHandler
    {
        private readonly HttpResponseMessage _mockResponse;

        public MockHttpClientHandler(HttpResponseMessage mockResponse)
        {
            _mockResponse = mockResponse;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mockResponse);
        }
    }
}

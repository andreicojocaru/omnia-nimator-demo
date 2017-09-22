using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Nimator.Plugins.Couchbase.Checks;
using Nimator.Plugins.Couchbase.Models.Settings;
using NUnit.Framework;

namespace Nimator.Plugins.Couchbase.Tests.Checks
{
    [TestFixture]
    public class PoolAvailabilityCheckTests
    {
        private PoolAvailabilityCheck _sut;
        private Mock<IHttpClientFactory> _httpClientFactoryMock;

        [SetUp]
        public void Setup()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();

            _sut = new PoolAvailabilityCheck(_httpClientFactoryMock.Object, new CouchbaseClusterSettings
            {
                ServerUrl = "http://dummy:1123"
            });
        }

        [Test]
        public void Constructor_WithNullSettings_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new PoolAvailabilityCheck(_httpClientFactoryMock.Object, null));
        }

        [Test]
        public void Constructor_WithEmptySettings_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new PoolAvailabilityCheck(_httpClientFactoryMock.Object, new CouchbaseBucketSizeSettings()));
        }

        [Test]
        public async Task RunAsync_SuccessResponse_ShouldReturnOk()
        {
            var httpClient = new HttpClient(new MockHttpClientHandler(new HttpResponseMessage(HttpStatusCode.OK)));
            _httpClientFactoryMock.Setup(f => f.GetHttpClient()).Returns(httpClient);

            var result = await _sut.RunAsync();

            Assert.NotNull(result);
            Assert.AreEqual(NotificationLevel.Okay, result.Level);
        }

        [Test]
        public async Task RunAsync_ErrorResponse_ShouldReturnCritical()
        {
            var httpClient = new HttpClient(new MockHttpClientHandler(new HttpResponseMessage(HttpStatusCode.BadRequest)));
            _httpClientFactoryMock.Setup(f => f.GetHttpClient()).Returns(httpClient);

            var result = await _sut.RunAsync();

            Assert.NotNull(result);
            Assert.AreEqual(NotificationLevel.Critical, result.Level);
        }
    }
}

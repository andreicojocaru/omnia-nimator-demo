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
    public class MemoryUsageCheckTests
    {
        private MemoryUsageCheck _sut;
        private Mock<IHttpClientFactory> _httpClientFactoryMock;

        [SetUp]
        public void Setup()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();

            _sut = new MemoryUsageCheck(_httpClientFactoryMock.Object, new CouchbaseMemoryUsageSettings
            {
                ServerUrl = "http://dummy:1123",
                AvailableMemoryThresholdPercentage = 15
            });
        }

        [Test]
        public void Constructor_WithNullSettings_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new MemoryUsageCheck(_httpClientFactoryMock.Object, null));
        }

        [Test]
        public void Constructor_WithEmptySettings_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new MemoryUsageCheck(_httpClientFactoryMock.Object, new CouchbaseMemoryUsageSettings()));
        }

        [Test]
        public async Task RunAsync_AvailableMemoryGreaterThanThreshold_ShouldReturnOk()
        {
            var remoteResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockRemoteJsonData.GetRawMemoryUsageJson(100, 50))
            };

            var httpClient = new HttpClient(new MockHttpClientHandler(remoteResponse));
            _httpClientFactoryMock.Setup(f => f.GetHttpClient()).Returns(httpClient);

            var result = await _sut.RunAsync();

            Assert.NotNull(result);
            Assert.AreEqual(NotificationLevel.Okay, result.Level);
        }

        [Test]
        public async Task RunAsync_AvailableMemoryLowerThanThreshold_ShouldReturnWarning()
        {
            var remoteResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockRemoteJsonData.GetRawMemoryUsageJson(100, 10))
            };

            var httpClient = new HttpClient(new MockHttpClientHandler(remoteResponse));
            _httpClientFactoryMock.Setup(f => f.GetHttpClient()).Returns(httpClient);

            var result = await _sut.RunAsync();

            Assert.NotNull(result);
            Assert.AreEqual(NotificationLevel.Warning, result.Level);
        }
    }
}

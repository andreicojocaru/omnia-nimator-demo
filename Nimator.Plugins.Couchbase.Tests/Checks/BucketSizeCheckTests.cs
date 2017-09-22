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
    public class BucketSizeCheckTests
    {
        private BucketSizeCheck _sut;
        private Mock<IHttpClientFactory> _httpClientFactoryMock;

        [SetUp]
        public void Setup()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();

            _sut = new BucketSizeCheck(_httpClientFactoryMock.Object, new CouchbaseBucketSizeSettings
            {
                ServerUrl = "http://dummy:1123",
                MaxRecords = 5
            });
        }

        [Test]
        public void Constructor_WithNullSettings_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new BucketSizeCheck(_httpClientFactoryMock.Object, null));
        }

        [Test]
        public void Constructor_WithEmptySettings_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new BucketSizeCheck(_httpClientFactoryMock.Object, new CouchbaseBucketSizeSettings()));
        }

        [Test]
        public async Task RunAsync_ItemsLowerThanThreshold_ShouldReturnOk()
        {
            var remoteResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockRemoteJsonData.GetRawBucketUsageJson(1))
            };

            var httpClient = new HttpClient(new MockHttpClientHandler(remoteResponse));
            _httpClientFactoryMock.Setup(f => f.GetHttpClient()).Returns(httpClient);

            var result = await _sut.RunAsync();

            Assert.NotNull(result);
            Assert.AreEqual(NotificationLevel.Okay, result.Level);
        }

        [Test]
        public async Task RunAsync_ItemsBiggerThanThreshold_ShouldReturnWarning()
        {
            var remoteResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockRemoteJsonData.GetRawBucketUsageJson(100))
            };

            var httpClient = new HttpClient(new MockHttpClientHandler(remoteResponse));
            _httpClientFactoryMock.Setup(f => f.GetHttpClient()).Returns(httpClient);

            var result = await _sut.RunAsync();

            Assert.NotNull(result);
            Assert.AreEqual(NotificationLevel.Warning, result.Level);
        }
    }
}

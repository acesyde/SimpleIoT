using System.Net;
using System.Threading.Tasks;
using SimpleIoT.Api.Integration.Tests.Fixtures;
using Xunit;

namespace SimpleIoT.Api.Integration.Tests
{
    public class HealthCheckTests : IClassFixture<FakeApplicationFactory<Startup>>
    {
        private readonly FakeApplicationFactory<Startup> _fakeApplicationFactory;

        public HealthCheckTests(FakeApplicationFactory<Startup> fakeApplicationFactory)
        {
            _fakeApplicationFactory = fakeApplicationFactory;
        }

        [Fact]
        public async Task TestLiveProbeAlwaysReturns200()
        {
            // A
            var httpClient = _fakeApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/health").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        }
    }
}

using SimpleIoT.Api.Integration.Tests.Fixtures;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SimpleIoT.Api.Integration.Tests;

[Collection("integration")]
public class SwaggerTests
{
    private readonly FakeApplicationFactory _factory;

    public SwaggerTests(FakeApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TestSwaggerJson()
    {
        // A
        var httpClient = _factory.CreateClient();

        // A
        var httpResponseMessage = await httpClient.GetAsync("/swagger/v1/swagger.json").ConfigureAwait(false);

        // A
        Assert.NotNull(httpResponseMessage);
        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.Equal("application/json", httpResponseMessage.Content.Headers.ContentType.MediaType);
    }

    [Fact]
    public async Task TestSwagger()
    {
        // A
        var httpClient = _factory.CreateClient();

        // A
        var httpResponseMessage = await httpClient.GetAsync("/swagger").ConfigureAwait(false);

        // A
        Assert.NotNull(httpResponseMessage);
        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.Equal("text/html", httpResponseMessage.Content.Headers.ContentType.MediaType);
    }
}
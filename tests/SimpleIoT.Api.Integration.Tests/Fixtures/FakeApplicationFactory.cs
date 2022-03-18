using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace SimpleIoT.Api.Integration.Tests.Fixtures;

public sealed class FakeApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }

    protected override void Dispose(bool disposing)
    {
        Server?.Dispose();
    }
}
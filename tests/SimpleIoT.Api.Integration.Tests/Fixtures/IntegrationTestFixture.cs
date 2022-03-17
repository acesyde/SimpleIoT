using Xunit;

namespace SimpleIoT.Api.Integration.Tests.Fixtures;

[CollectionDefinition("integration")]
public class IntegrationTestFixture : ICollectionFixture<FakeApplicationFactory<Startup>>
{
}
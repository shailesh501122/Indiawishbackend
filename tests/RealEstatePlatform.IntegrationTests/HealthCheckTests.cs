using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace RealEstatePlatform.IntegrationTests;

public sealed class HealthCheckTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public HealthCheckTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Health_Endpoint_Should_Return_Success()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/health");
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}

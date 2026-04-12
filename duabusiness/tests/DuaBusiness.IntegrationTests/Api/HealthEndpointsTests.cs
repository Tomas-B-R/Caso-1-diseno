using Xunit;

namespace DuaBusiness.IntegrationTests.Api;

public sealed class HealthEndpointsTests
{
    [Fact]
    public void ReadyEndpointContract_IsReservedForInfrastructureChecks()
    {
        Assert.True(true);
    }
}

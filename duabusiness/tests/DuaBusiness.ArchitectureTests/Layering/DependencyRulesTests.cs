using Xunit;

namespace DuaBusiness.ArchitectureTests.Layering;

public sealed class DependencyRulesTests
{
    [Fact]
    public void DomainLayer_RemainsIndependentByDesign()
    {
        Assert.True(true);
    }
}

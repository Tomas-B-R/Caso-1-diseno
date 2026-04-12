using DuaBusiness.Domain.ValueObjects;
using Xunit;

namespace DuaBusiness.UnitTests.Domain;

public sealed class ConfidenceScoreTests
{
    [Fact]
    public void Create_ClampsValueToMaximum()
    {
        var score = ConfidenceScore.Create(1.5m);

        Assert.Equal(1m, score.Value);
    }
}

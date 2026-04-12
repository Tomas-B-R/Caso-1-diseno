namespace DuaBusiness.Domain.ValueObjects;

public readonly record struct ConfidenceScore(decimal Value)
{
    public static ConfidenceScore Empty => new(0m);

    public bool RequiresManualReview => Value < 0.75m;

    public static ConfidenceScore Create(decimal value)
    {
        var normalized = decimal.Clamp(value, 0m, 1m);
        return new ConfidenceScore(normalized);
    }
}

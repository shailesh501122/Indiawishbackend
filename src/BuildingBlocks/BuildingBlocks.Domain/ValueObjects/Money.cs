namespace BuildingBlocks.Domain.ValueObjects;

public sealed record Money(decimal Amount, string Currency)
{
    public static Money Inr(decimal amount) => new(amount, "INR");
}

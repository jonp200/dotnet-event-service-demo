namespace EventService.Domain.Entities;

public sealed class Account
{
    public Guid Id { get; }

    public long BalanceMinorUnits { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Account(Guid id, long balanceMinorUnits)
    {
        Id = id;

        ArgumentOutOfRangeException.ThrowIfNegative(balanceMinorUnits);

        BalanceMinorUnits = balanceMinorUnits;
        UpdatedAt = DateTime.UtcNow;
    }

    public DebitResult Debit(long amountMinorUnits)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amountMinorUnits);

        if (BalanceMinorUnits < amountMinorUnits)
            return DebitResult.InsufficientFunds;

        BalanceMinorUnits -= amountMinorUnits;
        UpdatedAt = DateTime.UtcNow;

        return DebitResult.Applied;
    }
}

public enum DebitResult
{
    Applied,
    InsufficientFunds
}
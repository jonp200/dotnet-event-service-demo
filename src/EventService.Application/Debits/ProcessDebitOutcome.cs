namespace EventService.Application.Debits;

public enum DebitStatus
{
    Applied,
    InsufficientFunds,
    ConflictingRequest,
    AccountNotFound
}

public sealed record ProcessDebitOutcome(DebitStatus Status, long? BalanceMinorUnits = null);
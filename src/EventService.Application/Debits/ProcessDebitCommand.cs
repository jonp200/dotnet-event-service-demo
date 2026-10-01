namespace EventService.Application.Debits;

public sealed record ProcessDebitCommand(Guid AccountId, long AmountMinorUnits);
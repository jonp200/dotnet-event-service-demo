namespace EventService.Application.Debits;

public sealed record ProcessDebitCommand(Guid AccountId, Guid RequestId, long AmountMinorUnits);
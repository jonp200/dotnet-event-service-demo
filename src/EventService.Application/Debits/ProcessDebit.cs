using EventService.Application.Persistence;
using EventService.Domain.Entities;

namespace EventService.Application.Debits;

public class ProcessDebit(IAppDbContext dbContext)
{
    public async Task<ProcessDebitOutcome> ExecuteAsync(ProcessDebitCommand command, CancellationToken ct)
    {
        var account = await dbContext.FindAccountByIdAsync(command.AccountId, ct);
        if (account is null)
            return new ProcessDebitOutcome(DebitStatus.AccountNotFound);

        var debitResult = account.Debit(command.AmountMinorUnits);

        var outcome = new ProcessDebitOutcome(
            debitResult == DebitResult.Applied ? DebitStatus.Applied : DebitStatus.InsufficientFunds,
            account.BalanceMinorUnits);

        if (debitResult == DebitResult.Applied)
            await dbContext.SaveChangesAsync(ct);

        return outcome;
    }
}

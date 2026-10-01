using EventService.Domain.Entities;

namespace EventService.Application.Debits;

public class ProcessDebit(IDebitUnitOfWorkFactory unitOfWorkFactory)
{
    public async Task<ProcessDebitOutcome> ExecuteAsync(ProcessDebitCommand command, CancellationToken ct)
    {
        await using var tx = await unitOfWorkFactory.BeginAsync(ct);

        var account = await tx.GetAccountForUpdateAsync(command.AccountId, ct);
        if (account is null)
            return new ProcessDebitOutcome(DebitStatus.AccountNotFound);

        var debitResult = account.Debit(command.AmountMinorUnits);

        var outcome = new ProcessDebitOutcome(
            debitResult == DebitResult.Applied
                ? DebitStatus.Applied
                : DebitStatus.InsufficientFunds,
            account.BalanceMinorUnits);

        if (debitResult == DebitResult.Applied)
            await tx.SaveAccountAsync(account, ct);

        return outcome;
    }
}
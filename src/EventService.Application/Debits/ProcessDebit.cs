using EventService.Application.Repositories;
using EventService.Domain.Entities;

namespace EventService.Application.Debits;

public class ProcessDebit(IAccountStore store)
{
    public async Task<DebitResult> ExecuteAsync(Guid accountId, Guid requestId, long amount, CancellationToken ct)
    {
        var account = await store.FindById(accountId, ct);

        var result = account.Debit(amount);
        if (result == DebitResult.InsufficientFunds)
            return result;

        await store.UpdateBalance(account.Id, amount, ct);

        return result;
    }
}
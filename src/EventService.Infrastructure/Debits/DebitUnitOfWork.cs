using EventService.Application.Debits;
using EventService.Domain.Entities;
using EventService.Infrastructure.Persistence;
using Serilog;

namespace EventService.Infrastructure.Debits;

public class DebitUnitOfWork(InMemoryStore store, ILogger logger) : IDebitUnitOfWork
{
    public Task<Account?> GetAccountForUpdateAsync(Guid accountId, CancellationToken ct)
    {
        var account = store.Accounts.FirstOrDefault(x => x.Id == accountId);

        return Task.FromResult(account);
    }

    public Task SaveAccountAsync(Account account, CancellationToken ct)
    {
        var targetAccount = store.Accounts.FirstOrDefault(x => x.Id == account.Id);
        if (targetAccount == null)
            throw new InvalidOperationException($"Account with id {account.Id} does not exist");

        targetAccount.BalanceMinorUnits = account.BalanceMinorUnits;
        targetAccount.UpdatedAt = account.UpdatedAt;

        return Task.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        logger.Information("`{0}` is not necessary to call with in-memory store", nameof(DisposeAsync));

        return ValueTask.CompletedTask;
    }
}
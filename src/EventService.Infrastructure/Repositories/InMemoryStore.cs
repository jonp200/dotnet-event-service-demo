using EventService.Application.Debits;
using EventService.Application.Repositories;
using EventService.Domain.Entities;

namespace EventService.Infrastructure.Repositories;

public class InMemoryStore : IAccountStore
{
    private Dictionary<Guid, Account> Accounts { get; } = new();
    private Dictionary<Guid, Transaction> Transactions { get; } = new();

    public Task<Account> FindById(Guid id, CancellationToken _)
    {
        PurgeExpiredTransactions(CancellationToken.None).Wait(_);

        var account = Accounts[id];

        return account == null ? throw new NullReferenceException("Account not found") : Task.FromResult(account);
    }

    public Task UpdateBalance(Guid id, long balanceMinorUnits, CancellationToken _)
    {
        var account = FindById(id, _).Result;

        account.BalanceMinorUnits = balanceMinorUnits;

        return Task.CompletedTask;
    }

    private TimeSpan TransactionExpiration { get; } = TimeSpan.FromMinutes(2);

    private Task PurgeExpiredTransactions(CancellationToken _)
    {
        var expiration = DateTime.UtcNow.Add(TransactionExpiration);

        var expiredTransactions = Transactions.Where(t => t.Value.Timestamp >= expiration);

        foreach (var transaction in expiredTransactions)
            Transactions.Remove(transaction.Key);

        return Task.CompletedTask;
    }
}

internal class Transaction
{
    public Guid RequestId { get; set; }

    public DebitStatus DebitStatus { get; set; }

    public DateTime Timestamp { get; set; }
}
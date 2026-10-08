using EventService.Application.Persistence;
using EventService.Domain.Entities;

namespace EventService.Infrastructure.Persistence;

public sealed class InMemoryAppDbContext(InMemoryStore store) : IAppDbContext
{
    private readonly Dictionary<Guid, Account> _trackedAccounts = new();
    private bool _holdsLock;
    private bool _disposed;

    public async Task<Account?> FindAccountByIdAsync(Guid accountId, CancellationToken ct)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ct.ThrowIfCancellationRequested();

        if (!_holdsLock)
        {
            await store.UpdateLock.WaitAsync(ct);
            _holdsLock = true;
        }

        if (_trackedAccounts.TryGetValue(accountId, out var tracked))
            return tracked;

        var stored = store.Accounts.FirstOrDefault(account => account.Id == accountId);
        if (stored is null)
            return null;

        tracked = Copy(stored);
        _trackedAccounts.Add(accountId, tracked);

        return tracked;
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ct.ThrowIfCancellationRequested();

        if (!_holdsLock)
            return Task.CompletedTask;

        // Copy before writing so validation failures cannot partially commit.
        var snapshots = _trackedAccounts.Values.Select(Copy).ToDictionary(account => account.Id);
        for (var index = 0; index < store.Accounts.Count; index++)
        {
            if (snapshots.TryGetValue(store.Accounts[index].Id, out var snapshot))
                store.Accounts[index] = snapshot;
        }

        EndTransaction();

        return Task.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        EndTransaction();
        _disposed = true;

        return ValueTask.CompletedTask;
    }

    private static Account Copy(Account account) =>
        new(account.Id, account.BalanceMinorUnits) { UpdatedAt = account.UpdatedAt };

    private void EndTransaction()
    {
        _trackedAccounts.Clear();
        if (!_holdsLock)
            return;

        _holdsLock = false;
        store.UpdateLock.Release();
    }
}
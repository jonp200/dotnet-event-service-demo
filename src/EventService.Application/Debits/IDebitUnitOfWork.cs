using EventService.Domain.Entities;

namespace EventService.Application.Debits;

public interface IDebitUnitOfWork : IAsyncDisposable
{
    Task<Account?> GetAccountForUpdateAsync(Guid accountId, CancellationToken ct);

    Task SaveAccountAsync(Account account, CancellationToken ct);
}
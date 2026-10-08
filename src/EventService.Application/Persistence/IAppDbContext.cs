using EventService.Domain.Entities;

namespace EventService.Application.Persistence;

public interface IAppDbContext : IAsyncDisposable
{
    Task<Account?> FindAccountByIdAsync(Guid accountId, CancellationToken ct);

    Task SaveChangesAsync(CancellationToken ct);
}
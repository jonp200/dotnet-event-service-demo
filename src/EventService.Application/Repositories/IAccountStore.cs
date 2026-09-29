using EventService.Domain.Entities;

namespace EventService.Application.Repositories;

public interface IAccountStore
{
    Task<Account> FindById(Guid id, CancellationToken ct);

    Task UpdateBalance(Guid id, long balanceMinorUnits, CancellationToken ct);
}
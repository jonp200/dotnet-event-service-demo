using EventService.Domain.Entities;

namespace EventService.Infrastructure.Persistence;

public sealed class InMemoryStore
{
    internal SemaphoreSlim UpdateLock { get; } = new(1, 1);

    internal IList<Account> Accounts { get; } =
    [
        new(Guid.Parse("20528c0b-1c16-40f0-a7c2-41764b5c1401"), 10_000)
    ];
}
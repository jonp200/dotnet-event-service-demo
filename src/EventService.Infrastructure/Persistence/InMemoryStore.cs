using EventService.Domain.Entities;

namespace EventService.Infrastructure.Persistence;

/// <summary>
/// Use only for early-stage development to validate the system behavior.
/// Data updating here is prone to race-condition for concurrent requests.
/// </summary>
public sealed class InMemoryStore
{
    public IList<Account> Accounts { get; } =
    [
        new(Guid.Parse("20528c0b-1c16-40f0-a7c2-41764b5c1401"), 10_000) // Seed data
    ];
}
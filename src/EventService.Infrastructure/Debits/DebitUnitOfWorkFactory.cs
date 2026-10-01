using EventService.Application.Debits;
using EventService.Infrastructure.Persistence;
using Serilog;

namespace EventService.Infrastructure.Debits;

public sealed class DebitUnitOfWorkFactory(InMemoryStore store, ILogger logger) : IDebitUnitOfWorkFactory
{
    public Task<IDebitUnitOfWork> BeginAsync(CancellationToken _)
    {
        var unitOfWork = new DebitUnitOfWork(store, logger);

        return Task.FromResult<IDebitUnitOfWork>(unitOfWork);
    }
}
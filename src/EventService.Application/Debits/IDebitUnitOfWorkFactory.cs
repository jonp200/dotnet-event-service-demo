namespace EventService.Application.Debits;

public interface IDebitUnitOfWorkFactory
{
    Task<IDebitUnitOfWork> BeginAsync(CancellationToken ct);
}
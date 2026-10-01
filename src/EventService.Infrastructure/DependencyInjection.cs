using EventService.Application.Debits;
using EventService.Infrastructure.Debits;
using EventService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryStore>();
        
        services.AddScoped<IDebitUnitOfWorkFactory, DebitUnitOfWorkFactory>();
    }
}
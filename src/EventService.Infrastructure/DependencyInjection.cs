using EventService.Application.Persistence;
using EventService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        // Use in-memory store for early-stage development
        services.AddSingleton<InMemoryStore>();
        services.AddScoped<IAppDbContext, InMemoryAppDbContext>();
    }
}

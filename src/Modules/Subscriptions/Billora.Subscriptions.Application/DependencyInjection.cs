using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Billora.Subscriptions.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        // Process-wide, so snake_case columns bind to PascalCase DTO members without aliases.
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}

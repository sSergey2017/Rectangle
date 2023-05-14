using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rectangle.Application.Interfaces;

namespace Rectangle.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

        services.AddDbContext<RectangleDbContext>(options =>
        {
            options.UseMySql(connectionString, serverVersion,
                mysqlOptions =>
                    mysqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null));
        });

        services.AddScoped<IRectangleDbContext>(provider =>
            provider.GetService<RectangleDbContext>());

        return services;
    }
}
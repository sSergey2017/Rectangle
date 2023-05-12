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
        services.AddDbContext<RectangleDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IRectangleDbContext>(provider =>
            provider.GetService<RectangleDbContext>());

        return services;
    }
}
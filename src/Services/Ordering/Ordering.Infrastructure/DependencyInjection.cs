namespace Ordering.Infrastructure; 
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("Database");
       // Add Service to the Container
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }
}
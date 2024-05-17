namespace Ordering.Application;
public static class DependencyInjection
{ 
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });
        services.AddFeatureManagement();
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
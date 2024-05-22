namespace Auth.Api;

public class DependencyInjectionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IAuthService authService)
    {
        context.Items["AuthService"] = authService;
        await next(context);
    }
}

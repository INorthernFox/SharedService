using Microsoft.AspNetCore.Builder;

namespace SharedService.Framework.Middlewares;

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app) =>
        app.UseMiddleware<ExceptionMiddleware>();
}
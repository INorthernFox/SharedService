using Microsoft.AspNetCore.Builder;
using Serilog;

namespace SharedService.Framework;

public static class BuilderExtensions
{
    public static WebApplicationBuilder CreateLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SharedKernel;

namespace SharedService.Framework;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddOpenApiDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((doc, ctx, ct) =>
            {
                doc.Servers = [new OpenApiServer { Url = "/" }];
                return Task.CompletedTask;
            });

            options.AddSchemaTransformer((schema, context, _) =>
            {
                if (context.JsonTypeInfo.Type == typeof(Envelope<Errors>))
                {
                    if (schema.Properties.TryGetValue("errors", out var errorsProp))
                    {
                        errorsProp.Items.Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Error", };
                    }
                }

                return Task.CompletedTask;
            });
        });

        return builder;
    }
}
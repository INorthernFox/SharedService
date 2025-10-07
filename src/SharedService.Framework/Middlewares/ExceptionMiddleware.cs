using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedKernel;
using SharedService.Core.Exceptions;

namespace SharedService.Framework.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        (int code, Error[] errors) = exception switch
        {
            BadRequestException => (
                StatusCodes.Status400BadRequest,
                JsonSerializer.Deserialize<Error[]>(exception.Message) ?? [Error.Failure("Deserialization failed")]
            ),
            NotFoundException => (
                StatusCodes.Status404NotFound,
                JsonSerializer.Deserialize<Error[]>(exception.Message) ?? [Error.NotFound("Not found")]
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                [Error.Failure("Internal Server Error")]
            )
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        await context.Response.WriteAsJsonAsync(errors);
    }
}
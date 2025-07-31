using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred at {Time}", DateTime.UtcNow);

        var (title, detail, statusCode) = exception switch
        {
            ValidationException => ("Validation Error", exception.Message, StatusCodes.Status400BadRequest),
            BadRequestException => ("Bad Request", exception.Message, StatusCodes.Status400BadRequest),
            NotFoundException => ("Not Found", exception.Message, StatusCodes.Status404NotFound),
            _ => ("Internal Server Error", exception.Message, StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = statusCode,
            Instance = context.Request.Path,
            Extensions =
            {
                // TraceId for debugging
                ["traceId"] = context.TraceIdentifier
            }
        };

        // Add Validation Errors if applicable
        if (exception is ValidationException validationEx)
        {
            problemDetails.Extensions["validationErrors"] = validationEx.Errors;
        }

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}

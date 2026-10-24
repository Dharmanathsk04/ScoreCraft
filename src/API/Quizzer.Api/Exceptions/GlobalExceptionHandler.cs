using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Quizzer.Api.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Error occurred. Message: {message}", exception.Message);

        var errorDetails = environment.IsDevelopment() ? JsonConvert.SerializeObject(exception) : "Please contact with admin";

        var problemDetails = new ProblemDetails
        {
            Title = "Internal server error.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = $"An unhandled exception occurred. {errorDetails} "
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}

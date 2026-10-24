using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Core;
public static class ResultExtension
{
    public static IResult ConvertToProblemDetails<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        if (result.Errors != null && result.Errors.Any())
        {
            return TypedResults.Problem(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Extensions = new Dictionary<string, object?>
                {
                    {"errors", result.Errors}
                }
            });
        }
        return TypedResults.Problem(new ProblemDetails
        {
            Detail = result.Error.Message,
            Status = GetTitleAndStatusCode(result.Error).status,
            Title = GetTitleAndStatusCode(result.Error).title
        });
    }

    public static IResult ConvertToResult<T>(this Result<T> result)
    {
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ConvertToProblemDetails();
    }

    private static (int status, string title) GetTitleAndStatusCode(Error error)
    {
        var statusCode = error.ErrorType switch
        {
            ErrorType.Failure => (StatusCodes.Status400BadRequest, error.ErrorCode),
            ErrorType.Validation => (StatusCodes.Status400BadRequest, error.ErrorCode),
            ErrorType.NotFound => (StatusCodes.Status404NotFound, error.ErrorCode),
            ErrorType.Unexpected => (StatusCodes.Status500InternalServerError, error.ErrorCode),
            ErrorType.Conflict => (StatusCodes.Status409Conflict, error.ErrorCode),
            ErrorType.Unauthorized => (StatusCodes.Status401Unauthorized, error.ErrorCode),
            ErrorType.Forbidden => (StatusCodes.Status403Forbidden, error.ErrorCode),
            ErrorType.None => (StatusCodes.Status400BadRequest, error.ErrorCode),
            ErrorType.Custom => (StatusCodes.Status400BadRequest, error.ErrorCode),
            _ => (StatusCodes.Status400BadRequest, error.ErrorCode)
        };

        return statusCode;
    }



}
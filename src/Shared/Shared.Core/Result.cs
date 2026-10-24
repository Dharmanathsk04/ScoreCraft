namespace Shared.Core;

public sealed class Result : IBaseResult
{
    public bool IsSuccess { get; }
    public Error Error { get; }
    public List<Error>? Errors { get; }

    private Result(bool isSuccess, Error error, List<Error>? errors = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result NotFound()
    {
        return new Result(false, Error.NotFound("NotFound"));
    }

    public static Result BadRequest()
    {
        return new Result(false, Error.Failure("Bad Request"));
    }
}

public sealed class Result<TValue> : IBaseResult
{
    public bool IsSuccess { get; }
    public Error Error { get; }
    public List<Error>? Errors { get; }
    public TValue? Value { get; }

    private Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = Error.None;
        Errors = null ;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    private Result(List<Error> errors)
    {
        IsSuccess = false;
        Value = default;
        Error = Error.None;
        Errors = errors;
    }

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error)
    {
        return error == null ? throw new ArgumentNullException(nameof(error), "Invalid error object. Error can not be null.") : new Result<TValue>(error);
    }

    public static implicit operator Result<TValue>(List<Error> errors)
    {
        return new Result<TValue>(errors);
    }
}


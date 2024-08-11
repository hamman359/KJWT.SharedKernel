namespace KJWT.SharedKernel.Results;

public class Result : Result<Result>
{
    public Result() : base()
    {
    }

    protected internal Result(ResultStatus status) : base(status)
    {

    }

    public static Result SuccessWithMessage(string successMessage)
    {
        return new Result() { SuccessMessage = successMessage };
    }

    public static Result Success()
    {
        return new Result();
    }

    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(value);
    }

    public static Result<T> Success<T>(T value, string successMessage)
    {
        return new Result<T>(value, successMessage);
    }

    public new static Result Error(Error error)
    {
        return new Result(ResultStatus.Error) { Errors = new[] { error } };
    }

    public new static Result Error(ErrorList errors)
    {
        return new Result(ResultStatus.Error)
        {
            CorrelationId = errors?.CorrelationId ?? string.Empty,
            Errors = errors?.Errors ?? []
        };
    }

    public new static Result Invalid(ValidationError validationError)
    {
        return new Result(ResultStatus.Invalid) { ValidationErrors = [validationError] };
    }

    public new static Result Invalid(params ValidationError[] validationErrors)
    {
        return new Result(ResultStatus.Invalid)
        {
            ValidationErrors = new List<ValidationError>(validationErrors)
        };
    }

    public new static Result Invalid(IEnumerable<ValidationError> validationErrors)
    {
        return new Result(ResultStatus.Invalid) { ValidationErrors = validationErrors };
    }

    public new static Result NotFound()
    {
        return new Result(ResultStatus.NotFound);
    }

    public new static Result NotFound(params Error[] errors)
    {
        return new Result(ResultStatus.NotFound) { Errors = errors };
    }

    public new static Result Forbidden()
    {
        return new Result(ResultStatus.Forbidden);
    }

    public new static Result Forbidden(params Error[] errors)
    {
        return new Result(ResultStatus.Forbidden) { Errors = errors };
    }

    public new static Result Unauthorized()
    {
        return new Result(ResultStatus.Unauthorized);
    }

    public new static Result Unauthorized(params Error[] errors)
    {
        return new Result(ResultStatus.Unauthorized) { Errors = errors };
    }

    public new static Result Conflict()
    {
        return new Result(ResultStatus.Conflict);
    }

    public new static Result Conflict(params Error[] errors)
    {
        return new Result(ResultStatus.Conflict) { Errors = errors };
    }

    public new static Result CriticalError(params Error[] errors)
    {
        return new Result(ResultStatus.CriticalError) { Errors = errors };
    }

    public new static Result Unavailable(params Error[] errors)
    {
        return new Result(ResultStatus.Unavailable) { Errors = errors };
    }

    public new static Result NoContent()
    {
        return new Result(ResultStatus.NoContent);
    }
}

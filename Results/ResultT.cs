using System.Text.Json.Serialization;

namespace KJWT.SharedKernel.Results;

public class Result<T> : IResult
{
    protected Result()
    {
    }

    public Result(T value)
    {
        Value = value;
    }

    protected internal Result(T value, string successMessage) : this(value)
    {
        SuccessMessage = successMessage;
    }

    protected Result(ResultStatus status)
    {
        Status = status;
    }

    public static implicit operator T(Result<T> result) => result.Value;
    public static implicit operator Result<T>(T value) => new Result<T>(value);

    public static implicit operator Result<T>(Result result) => new Result<T>(default(T))
    {
        Status = result.Status,
        Errors = result.Errors,
        SuccessMessage = result.SuccessMessage,
        CorrelationId = result.CorrelationId,
        ValidationErrors = result.ValidationErrors
    };

    [JsonInclude]
    public T Value { get; init; }

    [JsonIgnore]
    public Type ValueType => typeof(T);

    [JsonInclude]
    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;

    public bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.NoContent or ResultStatus.Created;

    public bool IsFailure => !IsSuccess;

    [JsonInclude]
    public string SuccessMessage { get; protected set; } = string.Empty;

    [JsonInclude]
    public string CorrelationId { get; set; } = string.Empty;

    [JsonInclude]
    public string Location { get; protected set; } = string.Empty;

    [JsonInclude]
    public IEnumerable<Error> Errors { get; protected set; } = [];

    [JsonInclude]
    public IEnumerable<ValidationError> ValidationErrors { get; protected set; } = [];

    public object GetValue()
    {
        return Value;
    }

    public PagedResult<T> ToPagedResult(PagedInfo pagedInfo)
    {
        var pagedResult = new PagedResult<T>(pagedInfo, Value)
        {
            Status = Status,
            SuccessMessage = SuccessMessage,
            CorrelationId = CorrelationId,
            Errors = Errors,
            ValidationErrors = ValidationErrors
        };

        return pagedResult;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public static Result<T> Success(T value, string successMessage)
    {
        return new Result<T>(value, successMessage);
    }

    public static Result<T> Created(T value)
    {
        return new Result<T>(ResultStatus.Created) { Value = value };
    }

    public static Result<T> Created(T value, string location)
    {
        return new Result<T>(ResultStatus.Created) { Value = value, Location = location };
    }

    public static Result<T> Error(Error error)
    {
        return new Result<T>(ResultStatus.Error) { Errors = new[] { error } };
    }

    public static Result<T> Error(ErrorList errors)
    {
        return new Result<T>(ResultStatus.Error)
        {
            CorrelationId = errors?.CorrelationId ?? string.Empty,
            Errors = errors?.Errors ?? []
        };
    }

    public static Result<T> Invalid(ValidationError validationError)
    {
        return new Result<T>(ResultStatus.Invalid) { ValidationErrors = [validationError] };
    }

    public static Result<T> Invalid(params ValidationError[] validationErrors)
    {
        return new Result<T>(ResultStatus.Invalid)
        {
            ValidationErrors = new List<ValidationError>(validationErrors)
        };
    }

    public static Result<T> Invalid(IEnumerable<ValidationError> validationErrors)
    {
        return new Result<T>(ResultStatus.Invalid) { ValidationErrors = validationErrors };
    }

    public static Result<T> NotFound()
    {
        return new Result<T>(ResultStatus.NotFound);
    }

    public static Result<T> NotFound(params Error[] errors)
    {
        return new Result<T>(ResultStatus.NotFound) { Errors = errors };
    }

    public static Result<T> Forbidden()
    {
        return new Result<T>(ResultStatus.Forbidden);
    }

    public static Result<T> Forbidden(params Error[] errors)
    {
        return new Result<T>(ResultStatus.Forbidden) { Errors = errors };
    }

    public static Result<T> Unauthorized()
    {
        return new Result<T>(ResultStatus.Unauthorized);
    }

    public static Result<T> Unauthorized(params Error[] errors)
    {
        return new Result<T>(ResultStatus.Unauthorized) { Errors = errors };
    }

    public static Result<T> Conflict()
    {
        return new Result<T>(ResultStatus.Conflict);
    }

    public static Result<T> Conflict(params Error[] errors)
    {
        return new Result<T>(ResultStatus.Conflict) { Errors = errors };
    }

    public static Result<T> CriticalError(params Error[] errors)
    {
        return new Result<T>(ResultStatus.CriticalError) { Errors = errors };
    }

    public static Result<T> Unavailable(params Error[] errors)
    {
        return new Result<T>(ResultStatus.Unavailable) { Errors = errors };
    }

    public static Result<T> NoContent()
    {
        return new Result<T>(ResultStatus.NoContent);
    }
}

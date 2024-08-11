namespace KJWT.SharedKernel.Results;

public static class ResultExtensions
{
    /// <summary>
    /// Returns true if the result is successful (status is Ok).
    /// </summary>
    public static bool IsOk(this IResult result) => result.Status == ResultStatus.Ok;

    /// <summary>
    /// Returns true if the result is created (status is Created).
    /// </summary>
    public static bool IsCreated(this IResult result) => result.Status == ResultStatus.Created;

    /// <summary>
    /// Returns true if the result is an error (status is Error).
    /// </summary>
    public static bool IsError(this IResult result) => result.Status == ResultStatus.Error;

    /// <summary>
    /// Returns true if the result is forbidden (status is Forbidden).
    /// </summary>
    public static bool IsForbidden(this IResult result) => result.Status == ResultStatus.Forbidden;

    /// <summary>
    /// Returns true if the result is unauthorized (status is Unauthorized).
    /// </summary>
    public static bool IsUnauthorized(this IResult result) => result.Status == ResultStatus.Unauthorized;

    /// <summary>
    /// Returns true if the result is invalid (status is Invalid).
    /// </summary>
    public static bool IsInvalid(this IResult result) => result.Status == ResultStatus.Invalid;

    /// <summary>
    /// Returns true if the result is not found (status is NotFound).
    /// </summary>
    public static bool IsNotFound(this IResult result) => result.Status == ResultStatus.NotFound;

    /// <summary>
    /// Returns true if the result is no content (status is NoContent).
    /// </summary>
    public static bool IsNoContent(this IResult result) => result.Status == ResultStatus.NoContent;

    /// <summary>
    /// Returns true if the result is a conflict (status is Conflict).
    /// </summary>
    public static bool IsConflict(this IResult result) => result.Status == ResultStatus.Conflict;

    /// <summary>
    /// Returns true if the result is a critical error (status is CriticalError).
    /// </summary>
    public static bool IsCriticalError(this IResult result) => result.Status == ResultStatus.CriticalError;

    /// <summary>
    /// Returns true if the result is unavailable (status is Unavailable).
    /// </summary>
    public static bool IsUnavailable(this IResult result) => result.Status == ResultStatus.Unavailable;

    /// <summary>
    /// Transforms a Result's type from a source type to a destination type. If the Result is successful, the func parameter is invoked on the Result's source value to map it to a destination type.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="result"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Result<TDestination> Map<TSource, TDestination>(this Result<TSource> result, Func<TSource, TDestination> func)
    {
        switch(result.Status)
        {
            case ResultStatus.Ok:
                return func(result);

            case ResultStatus.Created:
                return string.IsNullOrEmpty(result.Location)
                    ? Result<TDestination>.Created(func(result.Value))
                    : Result<TDestination>.Created(func(result.Value), result.Location);

            case ResultStatus.NotFound:
                return result.Errors.Any()
                    ? Result<TDestination>.NotFound(result.Errors.ToArray())
                    : Result<TDestination>.NotFound();

            case ResultStatus.Unauthorized:
                return result.Errors.Any()
                    ? Result<TDestination>.Unauthorized(result.Errors.ToArray())
                    : Result<TDestination>.Unauthorized();

            case ResultStatus.Forbidden:
                return result.Errors.Any()
                    ? Result<TDestination>.Forbidden(result.Errors.ToArray())
                    : Result<TDestination>.Forbidden();

            case ResultStatus.Invalid:
                return Result<TDestination>.Invalid(result.ValidationErrors);

            case ResultStatus.Error:
                return Result<TDestination>.Error(new ErrorList(result.Errors.ToArray(), result.CorrelationId));

            case ResultStatus.Conflict:
                return result.Errors.Any()
                    ? Result<TDestination>.Conflict(result.Errors.ToArray())
                    : Result<TDestination>.Conflict();

            case ResultStatus.CriticalError:
                return Result<TDestination>.CriticalError(result.Errors.ToArray());

            case ResultStatus.Unavailable:
                return Result<TDestination>.Unavailable(result.Errors.ToArray());

            case ResultStatus.NoContent:
                return Result<TDestination>.NoContent();

            default:
                throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
        }
    }
}

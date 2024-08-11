using System.Text;

using KJWT.SharedKernel.Results;

using Microsoft.AspNetCore.Mvc;

using Http = Microsoft.AspNetCore.Http;

namespace KJWT.SharedKernel.AspNetCore.Exceptions;

#if NET7_0_OR_GREATER


public static partial class ResultExtensions
{
    /// <summary>
    /// Convert a <see cref="Result{T}"/> to an instance of <see cref="Microsoft.AspNetCore.Http.IResult"/>
    /// </summary>
    /// <typeparam name="T">The value being returned</typeparam>
    /// <param name="result">The Ardalis.Result to convert to an Microsoft.AspNetCore.Http.IResult</param>
    /// <returns></returns>
    public static Http.IResult ToMinimalApiResult<T>(this Result<T> result)
    {
        return ToMinimalApiResult((IResult)result);
    }

    /// <summary>
    /// Convert a <see cref="Result"/> to an instance of <see cref="Microsoft.AspNetCore.Http.IResult"/>
    /// </summary>
    /// <param name="result">The Ardalis.Result to convert to an Microsoft.AspNetCore.Http.IResult</param>
    /// <returns></returns>
    public static Http.IResult ToMinimalApiResult(this Result result)
    {
        return ToMinimalApiResult((IResult)result);
    }

    internal static Http.IResult ToMinimalApiResult(this IResult result) =>
        result.Status switch
        {
            ResultStatus.Ok => result is Result
                ? Http.Results.Ok()
                : Http.Results.Ok(result.GetValue()),
            ResultStatus.Created => Http.Results.Created("", result.GetValue()),
            ResultStatus.NoContent => Http.Results.NoContent(),
            ResultStatus.NotFound => NotFoundEntity(result),
            ResultStatus.Unauthorized => UnAuthorized(result),
            ResultStatus.Forbidden => Forbidden(result),
            ResultStatus.Invalid => Http.Results.BadRequest(result.ValidationErrors),
            ResultStatus.Error => UnprocessableEntity(result),
            ResultStatus.Conflict => ConflictEntity(result),
            ResultStatus.Unavailable => UnavailableEntity(result),
            ResultStatus.CriticalError => CriticalEntity(result),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
        };

    private static Http.IResult UnprocessableEntity(IResult result)
    {
        var details = new StringBuilder("Next error(s) occurred:");

        foreach(var error in result.Errors)
            details.Append("* ").Append(error).AppendLine();

        return Http.Results.UnprocessableEntity(new ProblemDetails
        {
            Title = "Something went wrong.",
            Detail = details.ToString()
        });
    }

    private static Http.IResult NotFoundEntity(IResult result)
    {
        var details = new StringBuilder("Next error(s) occurred:");

        if(result.Errors.Any())
        {
            foreach(var error in result.Errors)
                details.Append("* ").Append(error).AppendLine();

            return Http.Results.NotFound(new ProblemDetails
            {
                Title = "Resource not found.",
                Detail = details.ToString()
            });
        }
        else
        {
            return Http.Results.NotFound();
        }
    }

    private static Http.IResult ConflictEntity(IResult result)
    {
        var details = new StringBuilder("Next error(s) occurred:");

        if(result.Errors.Any())
        {
            foreach(var error in result.Errors)
                details.Append("* ").Append(error).AppendLine();

            return Http.Results.Conflict(new ProblemDetails
            {
                Title = "There was a conflict.",
                Detail = details.ToString()
            });
        }
        else
        {
            return Http.Results.Conflict();
        }
    }

    private static Http.IResult CriticalEntity(IResult result)
    {
        var details = new StringBuilder("Next error(s) occurred:");

        if(result.Errors.Any())
        {
            foreach(var error in result.Errors)
                details.Append("* ").Append(error).AppendLine();

            return Http.Results.Problem(new ProblemDetails()
            {
                Title = "Something went wrong.",
                Detail = details.ToString(),
                Status = Http.StatusCodes.Status500InternalServerError
            });
        }
        else
        {
            return Http.Results.StatusCode(Http.StatusCodes.Status500InternalServerError);
        }
    }

    private static Http.IResult UnavailableEntity(IResult result)
    {
        var details = new StringBuilder("Next error(s) occurred:");

        if(result.Errors.Any())
        {
            foreach(var error in result.Errors)
                details.Append("* ").Append(error).AppendLine();

            return Http.Results.Problem(new ProblemDetails
            {
                Title = "Service unavailable.",
                Detail = details.ToString(),
                Status = Http.StatusCodes.Status503ServiceUnavailable
            });
        }
        else
        {
            return Http.Results.StatusCode(Http.StatusCodes.Status503ServiceUnavailable);
        }
    }

    private static Http.IResult Forbidden(IResult result)
    {
        var details = new StringBuilder("Next error(s) occurred:");

        if(result.Errors.Any())
        {
            foreach(var error in result.Errors)
                details.Append("* ").Append(error).AppendLine();

            return Http.Results.Problem(new ProblemDetails
            {
                Title = "Forbidden.",
                Detail = details.ToString(),
                Status = Http.StatusCodes.Status403Forbidden
            });
        }
        else
        {
            return Http.Results.Forbid();
        }
    }

    private static Http.IResult UnAuthorized(IResult result)
    {
        var details = new StringBuilder("Next error(s) occurred:");

        if(result.Errors.Any())
        {
            foreach(var error in result.Errors)
                details.Append("* ").Append(error).AppendLine();

            return Http.Results.Problem(new ProblemDetails
            {
                Title = "Unauthorized.",
                Detail = details.ToString(),
                Status = Http.StatusCodes.Status401Unauthorized
            });
        }
        else
        {
            return Http.Results.Unauthorized();
        }
    }
}
#endif

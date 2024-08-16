﻿using FluentValidation;
using FluentValidation.Results;

using KJWT.SharedKernel.Results;

using MediatR;

namespace DmTools.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    /// <summary>
    /// Validates the request.
    /// Ifany errors, returns validation result.
    /// Otherwise, returns the result of the next() delegate execution.
    /// Skips the validation if there are not any validators defined.
    /// </summary>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        ValidationFailure[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            //.Select(failure => new Error(
            //    failure.PropertyName,
            //    failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }


    private static TResult CreateValidationResult<TResult>(ValidationFailure[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return Result<TResult>.Invalid(errors.Select(x => x.AsErrors()).ToList());
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}

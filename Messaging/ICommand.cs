using KJWT.SharedKernel.Results;

using MediatR;

namespace KJWT.SharedKernel.Messaging;

public interface ICommand
    : IRequest<Result>
{
}

public interface ICommand<TResponse>
    : IRequest<Result<TResponse>>
{
}

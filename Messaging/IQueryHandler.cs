using KJWT.SharedKernel.Results;

using MediatR;

namespace KJWT.SharedKernel.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

using KJWT.SharedKernel.Results;

using MediatR;

namespace KJWT.SharedKernel.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

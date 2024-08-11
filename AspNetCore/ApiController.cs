using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace KJWT.SharedKernel.AspNetCore;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender;


}
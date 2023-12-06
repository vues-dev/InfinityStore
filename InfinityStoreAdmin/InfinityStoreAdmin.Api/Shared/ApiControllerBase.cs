using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InfinityStoreAdmin.Api.Shared
{
    [ApiController]
    [Route("store-admin-api")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;
    }
}

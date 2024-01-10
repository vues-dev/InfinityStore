using Asp.Versioning;
using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup;
using Microsoft.AspNetCore.Mvc;

namespace InfinityStoreAdmin.Api.Features.GetGames;

[OperationGroup("CommonOperations")]
[ApiVersion(1)]
public class GetGamesController : ApiControllerBase
{
    [HttpPost("get-games")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<GetGamesResult> GetGamesAsync(GetGamesQuery query)
    {
       var result = await Mediator.Send(query);

       return result;
    }
}
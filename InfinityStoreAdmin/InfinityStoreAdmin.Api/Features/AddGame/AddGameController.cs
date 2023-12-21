using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup;
using Microsoft.AspNetCore.Mvc;

namespace InfinityStoreAdmin.Api.Features.AddGame
{
    [OperationGroup("CommonOperations")]
    public class AddGameController : ApiControllerBase
    {
        [HttpPost("add-game")]
        public async Task<IActionResult> AddGame(AddGameCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}

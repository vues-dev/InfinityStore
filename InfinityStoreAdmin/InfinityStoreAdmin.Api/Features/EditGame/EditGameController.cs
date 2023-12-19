using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfinityStoreAdmin.Api.Features.EditGame
{
    [OperationGroup("CommonOperations")]
    public class EditGameController : ApiControllerBase
    {
        [HttpPost("edit-game")]
        public async Task<IActionResult> EditGame(EditGameCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}

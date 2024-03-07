using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Oathsworn.Models;

namespace Oathsworn.Controllers
{
    [Route("api")]
    public class ReferenceController : Controller
    {
        // Fake endpoints to get models into NSwag
        
        [HttpPost]
        [Route("gamestate")]
        [ProducesResponseType(StatusCodes.Status418ImATeapot)]
        [ApiExplorerSettings(GroupName = "Reference")]
        public IActionResult GameState([FromBody] GameStateModel model)
        {
            return new StatusCodeResult(418);
        }
    }
}
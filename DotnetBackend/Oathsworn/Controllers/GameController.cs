using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Oathsworn.Business;
using Oathsworn.Models;

namespace Oathsworn.Controllers
{
    [Route("api")]
    public class GameController : Controller
    {
        private IGame _game;
        public GameController(IGame game)
        {
            _game = game;
        }

        [HttpGet]
        [Route("start-encounter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public IActionResult StartEncounter()
        {
            return Ok(_game.StartEncounter());
        }

        [HttpGet]
        [Route("get-gamestate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public IActionResult GetGameState([FromQuery] int encounterId)
        {
            return Ok(_game.GetGameState(encounterId));
        }

        [HttpPost]
        [Route("start-attack")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public IActionResult StartAttack([FromQuery] int encounterId, [FromBody] AttackModel model)
        {
            return Ok(_game.StartAttack(encounterId, model));
        }

        [HttpPost]
        [Route("reroll-attack")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public IActionResult RerollAttack([FromQuery] int encounterId, [FromBody] RerollModel model)
        {
            return Ok(_game.RerollAttack(encounterId, model));
        }
    }
}
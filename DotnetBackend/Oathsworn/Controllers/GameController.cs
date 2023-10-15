using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Oathsworn.Business;
using Oathsworn.Models;
using System.Linq;
using System;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value.Errors));
            }
            try
            {
                return Ok(_game.StartAttack(encounterId, model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("reroll-attack")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public IActionResult RerollAttack([FromQuery] int encounterId, [FromBody] RerollModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value.Errors));
            }
            try
            {
                return Ok(_game.RerollAttack(encounterId, model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("complete-attack")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public IActionResult CompleteAttack([FromQuery] int encounterId, [FromQuery] int attackId)
        {
            try
            {
                return Ok(_game.CompleteAttack(encounterId, attackId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
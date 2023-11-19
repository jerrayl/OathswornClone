using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Oathsworn.Business;
using Oathsworn.Models;
using System.Linq;
using System;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("start-encounter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public IActionResult StartEncounter()
        {
            return Ok(_game.StartEncounter());
        }

        [HttpPost]
        [Route("create-player")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public IActionResult Move([FromQuery] int encounterId, [FromBody] CreatePlayerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value.Errors));
            }
            try
            {
                _game.CreatePlayer(model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public async Task<IActionResult> Move([FromQuery] int encounterId, [FromBody] MoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value.Errors));
            }
            try
            {
                await _game.Move(encounterId, model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("spend-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public async Task<IActionResult> SpendToken([FromQuery] int encounterId, [FromBody] SpendTokenModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value.Errors));
            }
            try
            {
                await _game.SpendToken(encounterId, model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        public async Task<IActionResult> CompleteAttack([FromQuery] int encounterId, [FromQuery] int attackId)
        {
            try
            {
                await _game.CompleteAttack(encounterId, attackId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("end-turn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public async Task<IActionResult> EndTurn([FromQuery] int encounterId)
        {
            try
            {
                await _game.EndTurn(encounterId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
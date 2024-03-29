using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Oathsworn.Business;
using Oathsworn.Models;
using System;
using System.Threading.Tasks;
using Oathsworn.Infrastructure;

namespace Oathsworn.Controllers
{
    public class GameController : BaseController
    {
        private IGame _game;
        public GameController(IGame game)
        {
            _game = game;
        }

        [HttpPost]
        [Route("move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public async Task<IActionResult> Move([FromQuery] int encounterId, [FromBody] MoveModel model)
        {
            try
            {
                await _game.Move(encounterId, model);
                return Ok();
            }
            catch (ErrorMessageException e)
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
            try
            {
                await _game.SpendToken(encounterId, model);
                return Ok();
            }
            catch (ErrorMessageException e)
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
            try
            {
                return Ok(_game.StartAttack(encounterId, model));
            }
            catch (ErrorMessageException e)
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
            try
            {
                return Ok(_game.RerollAttack(encounterId, model));
            }
            catch (ErrorMessageException e)
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
            catch (ErrorMessageException e)
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
            catch (ErrorMessageException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("continue-enemy-action")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Game")]
        public async Task<IActionResult> ContinueEnemyAction([FromQuery] int encounterId)
        {
            try
            {
                await _game.ContinueEnemyAction(encounterId);
                return Ok();
            }
            catch (ErrorMessageException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
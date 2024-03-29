using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Oathsworn.Business;
using Oathsworn.Models;
using Oathsworn.Infrastructure;

namespace Oathsworn.Controllers
{
    public class UserManagementController : BaseController
    {
        private IUserManagement _userManagement;
        public UserManagementController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        [HttpPost]
        [Route("create-player")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "UserManagment")]
        public IActionResult CreatePlayer([FromBody] PlayerSummaryModel model)
        {
            try
            {
                _userManagement.CreatePlayer(model);
                return Ok();
            }
            catch (ErrorMessageException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("create-free-company")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "UserManagment")]
        public IActionResult CreateFreeCompany([FromBody] CreateFreeCompanyModel model)
        {
            try
            {
                _userManagement.CreateFreeCompany(model);
                return Ok();
            }
            catch (ErrorMessageException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("join-free-company")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "UserManagment")]
        public IActionResult JoinFreeCompany([FromBody] JoinFreeCompanyModel model)
        {
            try
            {
                _userManagement.JoinFreeCompany(model);
                return Ok();
            }
            catch (ErrorMessageException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("players")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "UserManagment")]
        public IActionResult GetPlayers()
        {
            return Ok(_userManagement.GetPlayers());
        }

        [HttpGet]
        [Route("free-companies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "UserManagment")]
        public IActionResult GetFreeCompanies()
        {
            return Ok(_userManagement.GetFreeCompanies());
        }

        [HttpGet]
        [Route("encounters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "UserManagment")]
        public IActionResult GetEncounters()
        {
           return Ok(_userManagement.GetEncounters());
        }
    }
}
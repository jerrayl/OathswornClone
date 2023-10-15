using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RAZE.Business;

namespace RAZE.Controllers
{
    [Route("api")]
    public class InfoController : Controller
    {
        private IInfo _info;
        public InfoController(IInfo info)
        {
            _info = info;
        }

        [HttpGet]
        [Route("troops")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Info")]
        public IActionResult GetTroops()
        {
            return Ok(_info.GetTroops());
        }

        [HttpGet]
        [Route("buildings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Info")]
        public IActionResult GetBuildings()
        {
            return Ok(_info.GetBuildings());
        }
    }
}

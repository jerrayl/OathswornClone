using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Oathsworn.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class BaseController : Controller
    {
    }
}
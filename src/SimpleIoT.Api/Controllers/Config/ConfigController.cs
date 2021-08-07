using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleIoT.Api.Controllers.Config.Models.Response;

namespace SimpleIoT.Api.Controllers.Config
{
    [Route("/api/config")]
    [ApiController]
    public class ConfigController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetConfigurationResponse),StatusCodes.Status200OK)]
        public IActionResult GetConfiguration()
        {
            return Ok(new GetConfigurationResponse());
        }
    }
}
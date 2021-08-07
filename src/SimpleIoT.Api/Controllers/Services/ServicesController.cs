using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleIoT.Api.Controllers.Services.Models.Response;

namespace SimpleIoT.Api.Controllers.Services
{
    [Route("/api/services")]
    [ApiController]
    public class ServicesController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetServicesResponse[]),StatusCodes.Status200OK)]
        public IActionResult GetService()
        {
            return Ok(Array.Empty<GetServicesResponse>());
        }
    }
}
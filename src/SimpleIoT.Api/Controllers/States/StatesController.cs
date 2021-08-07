using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleIoT.Api.Controllers.States.Models.Request;
using SimpleIoT.Api.Controllers.States.Models.Response;

namespace SimpleIoT.Api.Controllers.States
{
    [Route("/api/states")]
    [ApiController]
    public class StatesController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetStatesResponse[]),StatusCodes.Status200OK)]
        public IActionResult GetStates()
        {
            return Ok(Array.Empty<GetStatesResponse>());
        }
        
        [HttpGet("{entityId}")]
        [ProducesResponseType(typeof(GetStateResponse),StatusCodes.Status200OK)]
        public IActionResult GetStateByEntityId([Required, MinLength(1), MaxLength(255)]string entityId)
        {
            return Ok(new GetStateResponse());
        }
        
        [HttpPost("{entityId}")]
        [ProducesResponseType(typeof(GetStateResponse),StatusCodes.Status200OK)]
        public IActionResult SetEntityStateById([Required, MinLength(1), MaxLength(255)]string entityId, [FromBody]PostStateRequest request)
        {
            return Ok(new GetStateResponse());
        }
    }
}
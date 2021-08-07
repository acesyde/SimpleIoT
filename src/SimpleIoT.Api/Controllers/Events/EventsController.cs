using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleIoT.Api.Controllers.Events.Models.Request;
using SimpleIoT.Api.Controllers.Events.Models.Response;

namespace SimpleIoT.Api.Controllers.Events
{
    [Route("/api/events")]
    [ApiController]
    public class EventsController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetEventsResponse[]), StatusCodes.Status200OK)]
        public IActionResult GetEvents()
        {
            return Ok(Array.Empty<GetEventsResponse>());
        }

        [HttpPost("{eventName}")]
        [ProducesResponseType(typeof(PostEventResponse), StatusCodes.Status200OK)]
        public IActionResult PostEvent(
            [Required, MinLength(1), MaxLength(255)] string eventName,
            [FromBody] PostEventRequest request
        )
        {
            return Ok(new PostEventResponse());
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace SimpleIoT.Api.Controllers.Events.Models.Request
{
    public class PostEventRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public object Value { get; set; }
    }
}
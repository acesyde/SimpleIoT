using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleIoT.Api.Controllers.States.Models.Request
{
    public class PostStateRequest
    {
        [Required]
        public string State { get; set; }
        public Dictionary<string,object> Attributes { get; set; }
    }
}
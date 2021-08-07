using System;
using System.Collections.Generic;

namespace SimpleIoT.Api.Controllers.States.Models.Response
{
    public class GetStateResponse
    {
        public string EntityId { get; set; }
        public DateTime LastChanged { get; set; }
        public DateTime LastUpdated { get; set; }
        public string State { get; set; }
        public Dictionary<string,object> Attributes { get; set; }
    }
}
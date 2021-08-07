namespace SimpleIoT.Api.Controllers.Events.Models.Response
{
    public class GetEventsResponse
    {
        public string Event { get; set; }
        public int ListenerCount { get; set; }
    }
}
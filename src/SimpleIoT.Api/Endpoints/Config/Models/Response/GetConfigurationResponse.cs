namespace SimpleIoT.Api.Controllers.Config.Models.Response;

public class GetConfigurationResponse
{
    public string[] Components { get; set; }
    public string TimeZone { get; set; }
    public string Version { get; set; }
}
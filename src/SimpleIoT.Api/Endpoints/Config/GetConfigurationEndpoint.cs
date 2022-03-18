using FastEndpoints;
using SimpleIoT.Api.Controllers.Config.Models.Response;

namespace SimpleIoT.Api.Endpoints.Config;

public class GetConfigurationEndpoint : EndpointWithoutRequest<GetConfigurationResponse>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/config");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return SendOkAsync(new GetConfigurationResponse(), ct);
    }
}
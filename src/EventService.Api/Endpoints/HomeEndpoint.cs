namespace EventService.Api.Endpoints;

public static class HomeEndpoint
{
    public static void MapHomeEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => new { status = "ok" });
    }
}
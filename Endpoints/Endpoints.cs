using minimal_api_template.Common;
using minimal_api_template.Common.Filters;

namespace minimal_api_template.Endpoints;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("")
            .AddEndpointFilter<RequestLoggingFilter>()
            .RequireAuthorization()
            .WithOpenApi();

        endpoints
            .MapGroup("weatherforecast")
            .WithTags("Weatherforecast")
            .MapEndpoint<Weather.GetWeather>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
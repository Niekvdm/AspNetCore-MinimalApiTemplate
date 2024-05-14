using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using minimal_api_template.Common;
using minimal_api_template.Common.Extensions;

namespace minimal_api_template.Endpoints.Weather;

public class GetWeather : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/", Handle)
            .WithSummary("Gets the weather information")
            .WithRequestValidation<Request>()
            .AllowAnonymous();
    }

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Country)
                .NotNull()
                .NotEmpty();
        }
    }

    public record Request(string Country);

    public class Response
    {
        public string Country { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    private static string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private static Results<Ok<Response[]>, NotFound> Handle(
        [AsParameters] Request request
    )
    {
        var response = Enumerable
            .Range(1, 5)
            .Select(
                x =>
                    new Response
                    {
                        Country = request.Country,
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = _summaries[Random.Shared.Next(_summaries.Length)],
                        Date = DateTime.UtcNow
                    }
            )
            .ToArray();

        return response == null
            ? TypedResults.NotFound()
            : TypedResults.Ok(response);
    }
}
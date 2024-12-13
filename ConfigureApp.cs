using minimal_api_template.Endpoints;
using Scalar.AspNetCore;
using Serilog;

namespace minimal_api_template;

public static class ConfigureApp
{
    public static void Configure(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.MapOpenApi();
        app.MapScalarApiReference();
        app.UseHttpsRedirection();
        app.MapEndpoints();
        
        app.UseCors("AllowOrigin");
        
        app.UseAuthorization();
    }
}
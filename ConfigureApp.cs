using minimal_api_template.Endpoints;
using Serilog;

namespace minimal_api_template;

public static class ConfigureApp
{
    public static void Configure(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.MapEndpoints();
        
        app.UseCors("AllowOrigin");
        
        app.UseAuthorization();
    }
}
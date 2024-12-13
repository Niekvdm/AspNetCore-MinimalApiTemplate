using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Serilog;

namespace minimal_api_template;

public static class ConfigureServices
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddSerilog();
        builder.AddOpenApi();
        builder.AddCors();
        builder.AddAuthorization();

        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        builder.Services.Configure<JsonOptions>(
            options => { options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; }
        );

        builder.RegisterServices();
    }

    private static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options =>
            {
                options.AddPolicy(
                    "AllowOrigin",
                    policy =>
                    {
                        var allowedOrigins = builder.Configuration.GetSection("Kestrel:AllowedOrigins").Get<string[]>();

                        policy
                            .WithOrigins(allowedOrigins)
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .AllowAnyHeader();
                    }
                );
            }
        );
    }

    private static void AddOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
    }

    private static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(
            (context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); }
        );
    }

    private static void AddAutoMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Program));
    }

    private static void AddAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
    }
    
    private static void RegisterServices(this WebApplicationBuilder builder)
    {
        
    }
}
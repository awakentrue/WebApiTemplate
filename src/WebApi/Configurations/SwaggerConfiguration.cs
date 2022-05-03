using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace WebApi.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo {Title = "WebAPI", Version = "v1"});

            var openApiSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            
            options.AddSecurityDefinition(openApiSecurityScheme.Reference.Id, openApiSecurityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { openApiSecurityScheme, Array.Empty<string>() }
            });
        });

        return services;
    }
}
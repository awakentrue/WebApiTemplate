using Application;
using Application.Contracts;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using WebApi.Configurations;
using WebApi.Filters;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationLayer();
        services.AddInfrastructureLayer(Configuration);
        services.AddSerilogLogging(Configuration);
        services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));
        services.AddSwagger();
        services.AddApiBehaviorConfiguration();
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUser, CurrentUser>();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
        }

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
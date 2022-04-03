using Application;
using Infrastructure;
using WebApi.Configurations;
using WebApi.Middlewares;

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
        services.AddApplicationLayer(Configuration);
        services.AddInfrastructureLayer(Configuration);
        services.AddSerilogLogging(Configuration);
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddApiBehaviorConfiguration();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
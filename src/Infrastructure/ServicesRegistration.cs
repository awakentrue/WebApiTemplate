using System.Text;
using Application.Contracts.Authorization;
using Domain.Common;
using Domain.Common.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class ServicesRegistration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenFactory, JwtTokenFactory>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IAccountService, AccountService>();

        // var connectionString = configuration.GetConnectionString("DefaultConnection");
        // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("db")
                                                                      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        
        services.AddJwtSettings(configuration, out var jwtSettings);
        services.AddJwtTokenAuthentication(jwtSettings);

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IBookRepository, BookRepository>();
        
        return services;
    }

    private static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration configuration, out JwtSettings jwtSettings)
    {
        jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);

        services.AddSingleton(jwtSettings);
        
        return services;
    }
    
    private static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret)),
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

        return services;
    }
}
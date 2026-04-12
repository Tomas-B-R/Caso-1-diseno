using DuaBusiness.Infrastructure.Configuration;
using DuaBusiness.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DuaBusiness.Api.Extensions;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddProblemDetails();

        services.AddCors(options =>
        {
            options.AddPolicy(
                "frontend",
                policy => policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true));
        });

        var authenticationSection = configuration.GetSection(ApiSecurityOptions.SectionName);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authenticationSection["Authority"];
                options.Audience = authenticationSection["Audience"];
                options.RequireHttpsMetadata = bool.TryParse(authenticationSection["RequireHttpsMetadata"], out var requireHttpsMetadata)
                    ? requireHttpsMetadata
                    : true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicyCatalog.CustomsAgentPolicy, policy =>
                policy.RequireRole("CustomsAgent", "Manager"));

            options.AddPolicy(AuthorizationPolicyCatalog.ManagerPolicy, policy =>
                policy.RequireRole("Manager"));

            options.AddPolicy(AuthorizationPolicyCatalog.PlatformOperatorPolicy, policy =>
                policy.RequireRole("PlatformOperator"));
        });

        return services;
    }
}

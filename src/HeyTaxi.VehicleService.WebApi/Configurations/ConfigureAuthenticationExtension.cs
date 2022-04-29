using System.Security.Cryptography;
using HeyTaxi.VehicleService.WebApi.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HeyTaxi.VehicleService.WebApi.Configurations;

public static class ConfigureAuthenticationExtension
{
    
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                JwtOptions jwtOptions = new();
                configuration.GetSection(JwtOptions.Section).Bind(jwtOptions);
        
                RSACryptoServiceProvider rsa = new();
                rsa.ImportFromPem(jwtOptions.RsaPemFileContents);

                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                    ValidIssuer = jwtOptions.Issuer,
                    RequireExpirationTime = false,
                    RequireSignedTokens = true,
                    ValidateAudience = false,
                    ValidateIssuer = true,
                };
            });

        return services;
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TRILHAR.Infra.Data;
using System;
using System.Threading.Tasks;

namespace TRILHAR.Services.Api.Extensions
{
    public static class TokenConfigExtensions
    {
        public static void AddToken(this IServiceCollection services, IConfiguration configuration)
        {
            var aplicacao = configuration.GetSection("Aplicacao").Get<Aplicacao>();

            var KEY = "siggo-authentication-valid";
            var ISSUER = aplicacao.IdentityServer;
            var AUDIENCE = $"{aplicacao.IdentityServer}/resources";
            var SYMMETRIC_KEY = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(KEY));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.Authority = aplicacao.IdentityServer;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SYMMETRIC_KEY,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidIssuer = ISSUER,
                    ValidAudience = AUDIENCE,
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                            context.Response.StatusCode = 401;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}

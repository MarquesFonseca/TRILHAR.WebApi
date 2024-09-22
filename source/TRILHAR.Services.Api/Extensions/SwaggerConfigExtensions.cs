using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TRILHAR.Services.Api.Extensions
{
    /// <summary>
    /// SwaggerConfigExtensions
    /// </summary>
    public static class SwaggerConfigExtensions
    {
        /// <summary>
        /// AddSwagger
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo
                    {
                        Title = "TRILHAR API",
                        Version = "v1",
                        Description = "IQUES SISTEMAS"
                    });

                c.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Title = "TRILHAR API",
                        Version = "v2",
                        Description = "Externa"
                    });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT neste formato: Bearer {token}. \r\n\r\n Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
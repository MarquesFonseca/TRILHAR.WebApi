using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TRILHAR.Business.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRILHAR.Services.Api.Extensions
{
    /// <summary>
    /// AutoMapper Extension
    /// </summary>
    public static class AddAutoMapperSetupExtensions
    {
        /// <summary>
        /// Extensão autoMapper
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Registering Mappings automatically only works if the 
            // Automapper Profile classes are in ASP.NET project
            AutoMapperConfig.RegisterMappings();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRILHAR.Infra.Data.EF;
using System;
using System.Data.SqlClient;

namespace TRILHAR.Infra.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string TRILHAR_CONNECTION_STRING_NAME = "TRILHAR";

        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration) 
        {
            var trilharConnectionString = configuration.GetConnectionString(TRILHAR_CONNECTION_STRING_NAME);
            services.AddScoped<SqlConnection>(_ => new SqlConnection(trilharConnectionString));

            // Adiciona o TrilharContext ao DI com o uso do SqlServer
            services.AddDbContext<TrilharContext>(options =>
                options.UseSqlServer(trilharConnectionString));

            return services;
        }
    }
}

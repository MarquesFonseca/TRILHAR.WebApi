using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace TRILHAR.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AplicarConfiguracoes(this ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces()
                .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .Where(x => x.Name.Contains("Trilhar") == false && x.Name.Contains("Trilhar") == false)
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}

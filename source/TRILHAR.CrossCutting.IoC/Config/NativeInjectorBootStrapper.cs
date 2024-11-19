using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Extensions;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.Interfaces.Usuario;
using TRILHAR.Business.Notificacoes;
using TRILHAR.Business.Services;
using TRILHAR.CrossCutting.IoC.Usuario;
using TRILHAR.Infra.Data.EF;
using TRILHAR.Infra.Data.Repositories;

namespace TRILHAR.CrossCutting.IoC.Config
{
    public class NativeInjectorBootStrapper
    {
        public static IServiceCollection Registered { get; private set; }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<TrilharContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsuarioAtual, UsuarioAtual>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IObjectExtensionGenerics<AlunoEntity>, ObjectExtensionGenerics<AlunoEntity>>();
            services.AddScoped<IObjectExtensionGenerics<AptidaoTurmaMatriculaEntity>, ObjectExtensionGenerics<AptidaoTurmaMatriculaEntity>>();
            services.AddScoped<IObjectExtensionGenerics<FrequenciaEntity>, ObjectExtensionGenerics<FrequenciaEntity>>();
            services.AddScoped<IObjectExtensionGenerics<MatriculaAlunoTurmaEntity>, ObjectExtensionGenerics<MatriculaAlunoTurmaEntity>>();
            services.AddScoped<IObjectExtensionGenerics<TurmaEntity>, ObjectExtensionGenerics<TurmaEntity>>();
            services.AddScoped<IObjectExtensionGenerics<VFrequenciaAlunoTurmaEntity>, ObjectExtensionGenerics<VFrequenciaAlunoTurmaEntity>>();
            services.AddScoped<IObjectExtensionGenerics<VMatriculaAlunoTurmaEntity>, ObjectExtensionGenerics<VMatriculaAlunoTurmaEntity>>();
            
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();

            services.AddScoped<IAptidaoTurmaMatriculaService, AptidaoTurmaMatriculaService>();
            services.AddScoped<IAptidaoTurmaMatriculaRepository, AptidaoTurmaMatriculaRepository>();

            services.AddScoped<IFrequenciaService, FrequenciaService>();
            services.AddScoped<IFrequenciaRepository, FrequenciaRepository>();

            services.AddScoped<IMatriculaAlunoTurmaService, MatriculaAlunoTurmaService>();
            services.AddScoped<IMatriculaAlunoTurmaRepository, MatriculaAlunoTurmaRepository>();

            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();

            services.AddScoped<IVFrequenciaAlunoTurmaService, VFrequenciaAlunoTurmaService>();
            services.AddScoped<IVFrequenciaAlunoTurmaRepository, VFrequenciaAlunoTurmaRepository>();
            
            services.AddScoped<IVMatriculaAlunoTurmaService,    VMatriculaAlunoTurmaService>();
            services.AddScoped<IVMatriculaAlunoTurmaRepository, VMatriculaAlunoTurmaRepository>();
        
        
        }
    }
}

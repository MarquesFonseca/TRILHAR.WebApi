using Microsoft.AspNetCore.Http;
using TRILHAR.Business.Interfaces.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRILHAR.CrossCutting.IoC.Usuario
{
    public class UsuarioAtual : IUsuarioAtual
    {
        private readonly IHttpContextAccessor _accessor;

        public UsuarioAtual(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// CPF do usuário		
        /// </summary>
        public string Cpf => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("CPF"))?.Value;

        /// <summary>
        /// e-mail do usuário
        /// </summary>
        public string Email => _accessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type.Equals("Email"))?.Value;

        /// <summary>
        /// Numero da matricula do usuario
        /// </summary>
        public string Matricula => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Matricula"))?.Value;

        /// <summary>
        /// Ano do Exercicio		
        /// </summary>
        public int AnoExercicio
        {
            get
            {
                var anoExercicio = _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("AnoExercicio"))?.Value;
                if (anoExercicio == null)
                {
                    return 0;
                }
                return Convert.ToInt32(anoExercicio);
            }
        }

        /// <summary>
        /// Nome ou apelido do Usuário		
        /// </summary>
        public string NomeApelido => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("NomeApelido"))?.Value;

        /// <summary>
        /// Data e hora que foi efeutado a Autenticação
        /// </summary>
        public DateTime? DataHoraLogin
        {
            get
            {

                var dataHoraLogin = _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("DataHoraLogin"))?.Value;
                if (dataHoraLogin == null)
                {
                    return null;
                }
                return Convert.ToDateTime(dataHoraLogin);
            }
        }
    }
}
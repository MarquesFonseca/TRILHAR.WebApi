using System;
using System.Collections.Generic;
using System.Text;

namespace TRILHAR.Business.Interfaces.Usuario
{
    public interface IUsuarioAtual
    {
        /// <summary>
        /// CPF do usuário		
        /// </summary>
        string Cpf { get; }

        /// <summary>
        /// e-mail do usuário
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Numero da matricula do usuario
        /// </summary>
        string Matricula { get; }

        /// <summary>
        /// Ano do Exercicio		
        /// </summary>
        int AnoExercicio { get; }

        /// <summary>
        /// Nome ou apelido do Usuário		
        /// </summary>
        string NomeApelido { get; }

        /// <summary>
        /// Data e hora que foi efeutado a Autenticação
        /// </summary>
        DateTime? DataHoraLogin { get; }

    }
}

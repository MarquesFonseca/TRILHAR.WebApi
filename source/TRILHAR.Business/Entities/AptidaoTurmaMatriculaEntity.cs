using System;
using Dapper.Contrib.Extensions;
using TRILHAR.Business.Enums;

namespace TRILHAR.Business.Entities
{
    public class AptidaoTurmaMatriculaEntity : EntityBase
    {
        //[Key]
        //public int Codigo { get; set; }

        public AlunoEntity Aluno { get; set; }

        public TipoClassificacao TipoClassificacao { get; set; }
    }
}
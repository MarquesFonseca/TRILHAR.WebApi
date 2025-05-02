using Dapper.Contrib.Extensions;
using TRILHAR.Business.Enums;

namespace TRILHAR.Business.Entities
{
    public class AptidaoTurmaMatriculaEntity : EntityBase
    {
        [Key]
        public CriancaEntity Aluno { get; set; }

        public TipoClassificacao TipoClassificacao { get; set; }
    }
}
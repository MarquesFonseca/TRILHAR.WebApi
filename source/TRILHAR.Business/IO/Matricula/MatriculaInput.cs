using System;
using System.Collections.Generic;
using System.Text;

namespace TRILHAR.Business.IO.Matricula
{
    public class MatriculaInput
    {
        public int Codigo { get; set; }
        public int CodigoAluno { get; set; }
        public int CodigoTurma { get; set; }
        public bool Ativo { get; set; }
        public int? CodigoUsuarioLogado { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}

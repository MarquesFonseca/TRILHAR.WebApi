using System;
using System.Collections.Generic;
using System.Text;
using TRILHAR.Business.Entities;

namespace TRILHAR.Business.IO.Aluno
{
    public class AlunoInput
    {
        public int Codigo { get; set; }
        public string CodigoCadastro { get; set; }
        public string NomeCrianca { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string OutroResponsavel { get; set; }
        public string Telefone { get; set; }
        public string EnderecoEmail { get; set; }
        public bool Alergia { get; set; }
        public string DescricaoAlergia { get; set; }
        public bool RestricaoAlimentar { get; set; }
        public string DescricaoRestricaoAlimentar { get; set; }
        public bool DeficienciaOuSituacaoAtipica { get; set; }
        public string DescricaoDeficiencia { get; set; }
        public bool Batizado { get; set; }
        public DateTime? DataBatizado { get; set; }
        public string IgrejaBatizado { get; set; }
        public bool Ativo { get; set; }
        public int? CodigoUsuarioLogado { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}

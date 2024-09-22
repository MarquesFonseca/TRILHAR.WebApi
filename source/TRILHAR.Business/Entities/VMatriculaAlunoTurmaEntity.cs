using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace TRILHAR.Business.Entities
{
    [Table("VMatriculaAlunoTurma")]
    public class VMatriculaAlunoTurmaEntity : EntityBase
    {
        // Atributos
        private int _Codigo;
        private int _CodigoAluno;
        private int _CodigoTurma;
        private bool _Ativo;
        private int _CodigoUsuarioLogado;
        private DateTime? _DataAtualizacao;
        private DateTime? _DataCadastro;
        private string _AlunoCodigoCadastro;
        private string _AlunoNomeCrianca;
        private DateTime? _AlunoDataNascimento;
        private string _AlunoNomeMae;
        private string _AlunoNomePai;
        private string _AlunoOutroResponsavel;
        private string _AlunoTelefone;
        private string _AlunoEnderecoEmail;
        private bool _AlunoAlergia;
        private string _AlunoDescricaoAlergia;
        private bool _AlunoRestricaoAlimentar;
        private string _AlunoDescricaoRestricaoAlimentar;
        private bool _AlunoDeficienciaOuSituacaoAtipica;
        private string _AlunoDescricaoDeficiencia;
        private bool _AlunoBatizado;
        private DateTime? _AlunoDataBatizado;
        private string _AlunoIgrejaBatizado;
        private bool _AlunoAtivo;
        private string _TurmaDescricao;
        private DateTime? _TurmaIdadeInicialAluno;
        private DateTime? _TurmaIdadeFinalAluno;
        private int _TurmaAnoLetivo;
        private int _TurmaSemestreLetivo;
        private bool _TurmaAtivo;

        // Propriedades
        #region Codigo
        [Required(ErrorMessage = "Informe o campo Codigo")]
        [Display(Name = "Codigo")]
        [ExplicitKey]
        public int Codigo { get { return _Codigo; } set { _Codigo = value; } }
        #endregion

        #region CodigoAluno
        //[Required(ErrorMessage = "Informe o campo CodigoAluno")]
        [Display(Name = "CodigoAluno")]
        public int CodigoAluno { get { return _CodigoAluno; } set { _CodigoAluno = value; } }
        #endregion

        #region CodigoTurma
        //[Required(ErrorMessage = "Informe o campo CodigoTurma")]
        [Display(Name = "CodigoTurma")]
        public int CodigoTurma { get { return _CodigoTurma; } set { _CodigoTurma = value; } }
        #endregion

        #region Ativo
        //[Required(ErrorMessage = "Informe o campo Ativo")]
        [Display(Name = "Ativo")]
        public bool Ativo { get { return _Ativo; } set { _Ativo = value; } }
        #endregion

        #region CodigoUsuarioLogado
        //[Required(ErrorMessage = "Informe o campo CodigoUsuarioLogado")]
        [Display(Name = "CodigoUsuarioLogado")]
        public int CodigoUsuarioLogado { get { return _CodigoUsuarioLogado; } set { _CodigoUsuarioLogado = value; } }
        #endregion

        #region DataAtualizacao
        //[Required(ErrorMessage = "Informe o campo DataAtualizacao")]
        [Display(Name = "DataAtualizacao")]
        public DateTime? DataAtualizacao { get { return _DataAtualizacao; } set { _DataAtualizacao = value; } }
        #endregion

        #region DataCadastro
        //[Required(ErrorMessage = "Informe o campo DataCadastro")]
        [Display(Name = "DataCadastro")]
        public DateTime? DataCadastro { get { return _DataCadastro; } set { _DataCadastro = value; } }
        #endregion

        #region AlunoCodigoCadastro
        //[Required(ErrorMessage = "Informe o campo AlunoCodigoCadastro")]
        [Display(Name = "AlunoCodigoCadastro")]
        public string AlunoCodigoCadastro { get { return _AlunoCodigoCadastro; } set { _AlunoCodigoCadastro = value; } }
        #endregion

        #region AlunoNomeCrianca
        //[Required(ErrorMessage = "Informe o campo AlunoNomeCrianca")]
        [Display(Name = "AlunoNomeCrianca")]
        public string AlunoNomeCrianca { get { return _AlunoNomeCrianca; } set { _AlunoNomeCrianca = value; } }
        #endregion

        #region AlunoDataNascimento
        //[Required(ErrorMessage = "Informe o campo AlunoDataNascimento")]
        [Display(Name = "AlunoDataNascimento")]
        public DateTime? AlunoDataNascimento { get { return _AlunoDataNascimento; } set { _AlunoDataNascimento = value; } }
        #endregion

        #region AlunoNomeMae
        //[Required(ErrorMessage = "Informe o campo AlunoNomeMae")]
        [Display(Name = "AlunoNomeMae")]
        public string AlunoNomeMae { get { return _AlunoNomeMae; } set { _AlunoNomeMae = value; } }
        #endregion

        #region AlunoNomePai
        //[Required(ErrorMessage = "Informe o campo AlunoNomePai")]
        [Display(Name = "AlunoNomePai")]
        public string AlunoNomePai { get { return _AlunoNomePai; } set { _AlunoNomePai = value; } }
        #endregion

        #region AlunoOutroResponsavel
        //[Required(ErrorMessage = "Informe o campo AlunoOutroResponsavel")]
        [Display(Name = "AlunoOutroResponsavel")]
        public string AlunoOutroResponsavel { get { return _AlunoOutroResponsavel; } set { _AlunoOutroResponsavel = value; } }
        #endregion

        #region AlunoTelefone
        //[Required(ErrorMessage = "Informe o campo AlunoTelefone")]
        [Display(Name = "AlunoTelefone")]
        public string AlunoTelefone { get { return _AlunoTelefone; } set { _AlunoTelefone = value; } }
        #endregion

        #region AlunoEnderecoEmail
        //[Required(ErrorMessage = "Informe o campo AlunoEnderecoEmail")]
        [Display(Name = "AlunoEnderecoEmail")]
        public string AlunoEnderecoEmail { get { return _AlunoEnderecoEmail; } set { _AlunoEnderecoEmail = value; } }
        #endregion

        #region AlunoAlergia
        //[Required(ErrorMessage = "Informe o campo AlunoAlergia")]
        [Display(Name = "AlunoAlergia")]
        public bool AlunoAlergia { get { return _AlunoAlergia; } set { _AlunoAlergia = value; } }
        #endregion

        #region AlunoDescricaoAlergia
        //[Required(ErrorMessage = "Informe o campo AlunoDescricaoAlergia")]
        [Display(Name = "AlunoDescricaoAlergia")]
        public string AlunoDescricaoAlergia { get { return _AlunoDescricaoAlergia; } set { _AlunoDescricaoAlergia = value; } }
        #endregion

        #region AlunoRestricaoAlimentar
        //[Required(ErrorMessage = "Informe o campo AlunoRestricaoAlimentar")]
        [Display(Name = "AlunoRestricaoAlimentar")]
        public bool AlunoRestricaoAlimentar { get { return _AlunoRestricaoAlimentar; } set { _AlunoRestricaoAlimentar = value; } }
        #endregion

        #region AlunoDescricaoRestricaoAlimentar
        //[Required(ErrorMessage = "Informe o campo AlunoDescricaoRestricaoAlimentar")]
        [Display(Name = "AlunoDescricaoRestricaoAlimentar")]
        public string AlunoDescricaoRestricaoAlimentar { get { return _AlunoDescricaoRestricaoAlimentar; } set { _AlunoDescricaoRestricaoAlimentar = value; } }
        #endregion

        #region AlunoDeficienciaOuSituacaoAtipica
        //[Required(ErrorMessage = "Informe o campo AlunoDeficienciaOuSituacaoAtipica")]
        [Display(Name = "AlunoDeficienciaOuSituacaoAtipica")]
        public bool AlunoDeficienciaOuSituacaoAtipica { get { return _AlunoDeficienciaOuSituacaoAtipica; } set { _AlunoDeficienciaOuSituacaoAtipica = value; } }
        #endregion

        #region AlunoDescricaoDeficiencia
        //[Required(ErrorMessage = "Informe o campo AlunoDescricaoDeficiencia")]
        [Display(Name = "AlunoDescricaoDeficiencia")]
        public string AlunoDescricaoDeficiencia { get { return _AlunoDescricaoDeficiencia; } set { _AlunoDescricaoDeficiencia = value; } }
        #endregion

        #region AlunoBatizado
        //[Required(ErrorMessage = "Informe o campo AlunoBatizado")]
        [Display(Name = "AlunoBatizado")]
        public bool AlunoBatizado { get { return _AlunoBatizado; } set { _AlunoBatizado = value; } }
        #endregion

        #region AlunoDataBatizado
        //[Required(ErrorMessage = "Informe o campo AlunoDataBatizado")]
        [Display(Name = "AlunoDataBatizado")]
        public DateTime? AlunoDataBatizado { get { return _AlunoDataBatizado; } set { _AlunoDataBatizado = value; } }
        #endregion

        #region AlunoIgrejaBatizado
        //[Required(ErrorMessage = "Informe o campo AlunoIgrejaBatizado")]
        [Display(Name = "AlunoIgrejaBatizado")]
        public string AlunoIgrejaBatizado { get { return _AlunoIgrejaBatizado; } set { _AlunoIgrejaBatizado = value; } }
        #endregion

        #region AlunoAtivo
        //[Required(ErrorMessage = "Informe o campo AlunoAtivo")]
        [Display(Name = "AlunoAtivo")]
        public bool AlunoAtivo { get { return _AlunoAtivo; } set { _AlunoAtivo = value; } }
        #endregion

        #region TurmaDescricao
        //[Required(ErrorMessage = "Informe o campo TurmaDescricao")]
        [Display(Name = "TurmaDescricao")]
        public string TurmaDescricao { get { return _TurmaDescricao; } set { _TurmaDescricao = value; } }
        #endregion

        #region TurmaIdadeInicialAluno
        //[Required(ErrorMessage = "Informe o campo TurmaIdadeInicialAluno")]
        [Display(Name = "TurmaIdadeInicialAluno")]
        public DateTime? TurmaIdadeInicialAluno { get { return _TurmaIdadeInicialAluno; } set { _TurmaIdadeInicialAluno = value; } }
        #endregion

        #region TurmaIdadeFinalAluno
        //[Required(ErrorMessage = "Informe o campo TurmaIdadeFinalAluno")]
        [Display(Name = "TurmaIdadeFinalAluno")]
        public DateTime? TurmaIdadeFinalAluno { get { return _TurmaIdadeFinalAluno; } set { _TurmaIdadeFinalAluno = value; } }
        #endregion

        #region TurmaAnoLetivo
        //[Required(ErrorMessage = "Informe o campo TurmaAnoLetivo")]
        [Display(Name = "TurmaAnoLetivo")]
        public int TurmaAnoLetivo { get { return _TurmaAnoLetivo; } set { _TurmaAnoLetivo = value; } }
        #endregion

        #region TurmaSemestreLetivo
        //[Required(ErrorMessage = "Informe o campo TurmaSemestreLetivo")]
        [Display(Name = "TurmaSemestreLetivo")]
        public int TurmaSemestreLetivo { get { return _TurmaSemestreLetivo; } set { _TurmaSemestreLetivo = value; } }
        #endregion

        #region TurmaAtivo
        //[Required(ErrorMessage = "Informe o campo TurmaAtivo")]
        [Display(Name = "TurmaAtivo")]
        public bool TurmaAtivo { get { return _TurmaAtivo; } set { _TurmaAtivo = value; } }
        #endregion
    }
}
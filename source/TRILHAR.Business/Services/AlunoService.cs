using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Aluno;

namespace TRILHAR.Business.Services
{
    public class AlunoService : BaseService, IAlunoService
    {
        private readonly IObjectExtensionGenerics<AlunoEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;

        public AlunoService(
            INotificador notificador,
            IAlunoRepository repository,
            IObjectExtensionGenerics<AlunoEntity> objectExtension,
            IMapper mapper
            ) : base(notificador)
        {
            _objectExtensionGenerics = objectExtension;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AtualizarRegistro(AlunoInput registro)
        {
            var registroEntity = _mapper.Map<AlunoInput, AlunoEntity>(registro);

            registroEntity = _objectExtensionGenerics.TrataCamposNulls(registroEntity);

            return await _repository.AtualizarRegistro(registroEntity);
        }

        public async Task<int> DeletarRegistro(int codigo)
        {
            return await _repository.DeletarRegistro(codigo);
        }

        public async Task<int> ExecuteSql(string sql, string condicao = null, object parametros = null, CommandType commandType = CommandType.Text)
        {
            return await _repository.ExecuteAsync(sql, condicao, parametros, commandType);
        }

        public async Task<int> NovoRegistro(AlunoInput registro)
        {
            //Convert o registro de entrada em uma entidade para inclusão
            var registroEntity = _mapper.Map<AlunoInput, AlunoEntity>(registro);

            registroEntity = _objectExtensionGenerics.TrataCamposNulls(registroEntity);

            return await _repository.NovoRegistro(registroEntity);
        }

        public async Task<IEnumerable<AlunoEntity>> RetornaAll()
        {
            return await _repository.RetornaAll();
        }

        public async Task<AlunoEntity> RetornaByCodigo(int codigo)
        {
            return await _repository.RetornaByCodigo(codigo);
        }

        public async Task<AlunoEntity> RetornaByCodigoCadastro(string codigoCadastro)
        {
            var retorno = await _repository.RetornaByCodigoCadastro(codigoCadastro);
            return retorno;
        }

        public async Task<AlunoEntity> RetornaByCondicao(string condicao, object parametros)
        {
            return await _repository.RetornaByCondicao(condicao, parametros);
        }

        public async Task<AlunoEntity> RetornaSingleBySqlConsultaCondicao(string sqlConsulta, string condicao = null, object parametros = null)
        {
            return await _repository.RetornaSingleBySqlConsultaCondicao(sqlConsulta, condicao, parametros);
        }

        public async Task<IEnumerable<AlunoEntity>> RetornaListaByCondicao(string condicao, object parametros)
        {
            return await _repository.RetornaListaByCondicao(condicao, parametros);
        }

        public async Task<int> RetornaMaxCodigoCadastro()
        {
            return await _repository.RetornaMaxCodigoCadastro();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
        }
    }
}

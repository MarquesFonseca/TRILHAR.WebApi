﻿using AutoMapper;
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

        public async Task<int> AtualizarRegistroAsync(AlunoInput registro)
        {
            var registroEntity = _mapper.Map<AlunoInput, AlunoEntity>(registro);

            registroEntity = _objectExtensionGenerics.TrataCamposNulls(registroEntity);

            return await _repository.AtualizarRegistroAsync(registroEntity);
        }

        public async Task<int> DeletarRegistroAsync(int codigo)
        {
            return await _repository.DeletarRegistroAsync(codigo);
        }

        public async Task<int> ExecuteSqlAsync(string sql, string condicao = null, object parametros = null, CommandType commandType = CommandType.Text)
        {
            return await _repository.ExecuteAsync(sql, condicao, parametros, commandType);
        }

        public async Task<int> NovoRegistroAsync(AlunoInput registro)
        {
            //Convert o registro de entrada em uma entidade para inclusão
            var registroEntity = _mapper.Map<AlunoInput, AlunoEntity>(registro);

            registroEntity = _objectExtensionGenerics.TrataCamposNulls(registroEntity);

            return await _repository.NovoRegistroAsync(registroEntity);
        }

        public async Task<IEnumerable<AlunoEntity>> RetornaAllAsync(bool isPaginacao = false, int page = 1, int pageSize = 10)
        {
            return await _repository.RetornaAllAsync(isPaginacao, page, pageSize);
        }

        public async Task<AlunoEntity> RetornaByCodigoAsync(int codigo)
        {
            return await _repository.RetornaByCodigoAsync(codigo);
        }

        public async Task<AlunoEntity> RetornaByCodigoCadastroAsync(string codigoCadastro)
        {
            var retorno = await _repository.RetornaByCodigoCadastroAsync(codigoCadastro);
            return retorno;
        }

        public async Task<AlunoEntity> RetornaByCondicaoAsync(string condicao, object parametros)
        {
            return await _repository.RetornaByCondicaoAsync(condicao, parametros);
        }

        public async Task<AlunoEntity> RetornaSingleBySqlConsultaCondicaoAsync(string sqlConsulta, string condicao = null, object parametros = null)
        {
            return await _repository.RetornaSingleBySqlConsultaCondicao(sqlConsulta, condicao, parametros);
        }

        public async Task<IEnumerable<AlunoEntity>> RetornaListaByCondicaoAsync(string condicao, object parametros)
        {
            return await _repository.RetornaListaByCondicaoAsync(condicao, parametros);
        }

        public async Task<int> RetornaMaxCodigoCadastroAsync()
        {
            return await _repository.RetornaMaxCodigoCadastroAsync();
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

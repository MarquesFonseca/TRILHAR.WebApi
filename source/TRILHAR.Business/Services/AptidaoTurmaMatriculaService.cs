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
//using TRILHAR.Business.IO.;
namespace TRILHAR.Business.Services
{
    public class AptidaoTurmaMatriculaService : BaseService, IAptidaoTurmaMatriculaService
    {
        private readonly IObjectExtensionGenerics<AptidaoTurmaMatriculaEntity> _objectExtensionGenerics;
        private readonly IAptidaoTurmaMatriculaRepository _repository;
        private readonly IMapper _mapper;

        public AptidaoTurmaMatriculaService(
            INotificador notificador,
            IAptidaoTurmaMatriculaRepository repository,
            IObjectExtensionGenerics<AptidaoTurmaMatriculaEntity> objectExtension,
            IMapper mapper
            ) : base(notificador)
        {
            _objectExtensionGenerics = objectExtension;
            _repository = repository;
            _mapper = mapper;
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

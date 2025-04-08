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
using TRILHAR.Business.IO.Pagina;

namespace TRILHAR.Business.Services
{
    public class PaginaService : BaseService, IPaginaService
    {
        private readonly IObjectExtensionGenerics<PaginaEntity> _objectExtensionGenerics;
        private readonly IPaginaRepository _repository;

        public PaginaService(
            INotificador notificador,
            IPaginaRepository repository,
            IObjectExtensionGenerics<PaginaEntity> objectExtension,
            IMapper mapper
            ) : base(notificador, mapper)
        {
            _objectExtensionGenerics = objectExtension;
            _repository = repository;
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

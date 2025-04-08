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
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IObjectExtensionGenerics<UsuarioEntity> _objectExtensionGenerics;
        private readonly IUsuarioRepository _repository;

        public UsuarioService(
            INotificador notificador,
            IUsuarioRepository repository,
            IObjectExtensionGenerics<UsuarioEntity> objectExtension,
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

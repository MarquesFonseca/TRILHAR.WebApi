using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Notificacoes;

namespace TRILHAR.Business.Services
{
    public abstract class BaseService
    {
        protected readonly INotificador _notificador;
        protected readonly IMapper _mapper;

        protected BaseService(INotificador notificador, IMapper mapper)
        {
            _notificador = notificador;
            _mapper = mapper;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        protected bool IsValid()
        {
            return _notificador.TemNotificacao();
        }
    }
}
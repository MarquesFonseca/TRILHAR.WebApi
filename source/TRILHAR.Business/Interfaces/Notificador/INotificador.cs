using TRILHAR.Business.Notificacoes;
using System.Collections.Generic;

namespace TRILHAR.Business.Interfaces.Notificador
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
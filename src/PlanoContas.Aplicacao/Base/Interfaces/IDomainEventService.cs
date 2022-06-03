using PlanoContas.Dominio.Entidades.Base;
using PlanoContas.Dominio.Eventos;

namespace PlanoContas.Aplicacao.Base.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}

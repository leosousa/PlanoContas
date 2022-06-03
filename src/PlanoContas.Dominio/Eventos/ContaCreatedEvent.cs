using PlanoContas.Dominio.Eventos.Base;
using Entidades = PlanoContas.Dominio.Entidades;

namespace PlanoContas.Dominio.Eventos
{
    public class ContaCreatedEvent : BaseEvent
    {
        public ContaCreatedEvent(Entidades.Conta item)
        {
            Item = item;
        }

        public Entidades.Conta Item { get; }
    }
}

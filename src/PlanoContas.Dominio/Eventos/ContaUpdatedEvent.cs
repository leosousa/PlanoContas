using PlanoContas.Dominio.Eventos.Base;

namespace PlanoContas.Dominio.Eventos
{
    public class ContaUpdatedEvent : BaseEvent
    {
        public ContaUpdatedEvent(Entidades.Conta item)
        {
            Item = item;
        }

        public Entidades.Conta Item { get; }
    }
}

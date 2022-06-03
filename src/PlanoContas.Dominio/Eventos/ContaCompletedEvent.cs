using PlanoContas.Dominio.Eventos.Base;

namespace PlanoContas.Dominio.Eventos
{
    public class ContaCompletedEvent : BaseEvent
    {
        public ContaCompletedEvent(Entidades.Conta item)
        {
            Item = item;
        }

        public Entidades.Conta Item { get; }
    }
}

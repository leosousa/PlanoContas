using PlanoContas.Dominio.Eventos.Base;

namespace PlanoContas.Dominio.Eventos
{
    public class ContaDeletedEvent : BaseEvent
    {
        public ContaDeletedEvent(Entidades.Conta item)
        {
            Item = item;
        }

        public Entidades.Conta Item { get; }
    }
}

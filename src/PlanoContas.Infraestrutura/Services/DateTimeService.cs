using PlanoContas.Aplicacao.Base.Interfaces;

namespace PlanoContas.Infraestrutura.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

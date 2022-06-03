using PlanoContas.Dominio.Eventos.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanoContas.Dominio.Entidades.Base
{
    /// <summary>
    /// Entidade base com dados comuns a todas as entidades
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identificador do registro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Usuário que criou o registro
        /// </summary>
        public string? CriadoPor { get; set; }

        /// <summary>
        /// Data de modificação do registro
        /// </summary>
        public DateTime? DataModificacao { get; set; }

        /// <summary>
        /// Usuário que modificou o registro
        /// </summary>
        public string? ModificadoPor { get; set; }


        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}

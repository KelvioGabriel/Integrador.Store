namespace Integrador.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}

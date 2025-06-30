namespace Familia.Domain.Shared.Extenstions
{
    public abstract class IdEntity<TId> where TId : notnull
    {
        protected IdEntity(TId id) => Id = id;
        public TId Id { get; private set; }
    }
}


namespace Domain.SeedWork
{
    public abstract class BaseEntity : BaseEntity<long>{}

    public abstract class BaseEntity<T>
    {
        public T Id { get; protected set; } = default!;
    }

}

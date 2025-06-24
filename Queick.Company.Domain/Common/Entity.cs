namespace Queick.Company.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; }

    public override bool Equals(object? other)
    {
        if (other is null || other.GetType() != GetType())
        {
            return false;
        }

        return ((Entity)other).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    public static bool operator ==(Entity? left, Entity? right)
    {
        return left?.Id == right?.Id;
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return left?.Id != right?.Id;
    }

    protected Entity(Guid id) => Id = id;

    protected Entity()
    {
    }
}
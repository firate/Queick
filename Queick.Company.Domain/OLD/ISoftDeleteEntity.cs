namespace Queick.Company.Domain;

public interface ISoftDeleteEntity
{
    bool IsDeleted { get;  }
    DateTimeOffset? DeletedAt { get; }
    public string DeletedBy { get;  }
}
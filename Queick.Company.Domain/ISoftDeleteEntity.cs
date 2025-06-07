namespace Queick.Company.Domain;

public interface ISoftDeleteEntity
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; }
}
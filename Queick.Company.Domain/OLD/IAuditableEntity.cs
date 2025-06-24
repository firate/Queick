namespace Queick.Company.Domain;

public interface IAuditableEntity
{
    string? CreatedBy { get;  }
    string? UpdatedBy { get;  }
    DateTimeOffset Created { get; }
    DateTimeOffset Updated { get;  }
}
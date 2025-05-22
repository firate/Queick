using Queick.Shared.Domain;

namespace Queick.Company.Domain;

public class Customer : IEntity, ISoftDeleteEntity, IActivatable
{
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalId { get; set; }
    public string Phone { get; set; }
    public string PhoneCountryCode { get; set; }
    public string Password { get; set; }
    
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";

    public List<CommunicationInfo> CommunicationInfos { get; set; } = [];
    public List<Address> Addresses { get; set; } = [];
    public bool IsActive { get; set; }
}
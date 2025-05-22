using Queick.Shared.Domain;

namespace Queick.Appointment.Domain;

public class Customer : IEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NationalId { get; set; }
    public required string Phone { get; set; }
    public required string PhoneCountryCode { get; set; }
    public required string Password { get; set; }

    public List<CommunicationInfo> CommInfos { get; set; } = [];
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}
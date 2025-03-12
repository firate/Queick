namespace Appointment.Entity;

public class Customer : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NationalId { get; set; }
    public required string Phone { get; set; }
    public required string PhoneCountryCode { get; set; }
    public required string Password { get; set; }

    public List<CommunicationInfo> CommInfos { get; set; } = [];
}
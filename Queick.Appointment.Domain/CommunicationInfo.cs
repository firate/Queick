namespace Appointment.Entity;

public class CommunicationInfo : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Value { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    private CommunicationType CommunicationType { get; set; }
    public required int CommunicationTypeId
    {
        get => (int) CommunicationType; 
        set => CommunicationType = (CommunicationType) value;
    }
    
}
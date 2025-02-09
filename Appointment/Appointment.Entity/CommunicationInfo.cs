namespace Appointment.Entity;

public class CommunicationInfo : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public CommunicationType CommunicationType { get; set; }
    public int CommunicationTypeId
    {
        get => (int) CommunicationType; 
        set => CommunicationType = (CommunicationType) value;
    }
    
}

public enum CommunicationType
{
    Email,
    Phone,
    Fax,
    Mobile
}
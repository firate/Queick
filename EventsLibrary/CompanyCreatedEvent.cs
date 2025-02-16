namespace EventsLibrary;

public class CompanyCreatedEvent
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
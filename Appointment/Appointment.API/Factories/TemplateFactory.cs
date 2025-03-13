namespace Appointment.API.Factories;

public class TemplateFactory
{
    public TemplateFactory()
    {
        
    }
}

public record TemplateDetailModel
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}

public record TemplateListModel
{
    public List<TemplateDetailModel> Models { get; set; } = [];
}

public interface ITemplateFactory
{
    
}
using Appointment.Entity;
using Queick.Appointment.Domain;

namespace Queick.Company.Domain;

public class Location : IEntity
{
    public string Name { get; set; }
    public long Id { get; set; }
}
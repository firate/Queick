using Queick.Shared.Domain;

namespace Queick.Company.Domain;

public class Location : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
}
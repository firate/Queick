namespace Queick.Company.Application.DTOs;

public class AddressCreationDto<T>
{
    public long EntityId { get; set; }
    public T Entity { get; set; }
    
    public string Name { get; set; }
    public bool IsPrimary { get; set; }
    
    public string EntityType => typeof(T).Name;
}
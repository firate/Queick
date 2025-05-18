using Queick.Shared.Domain;

namespace Queick.Company.Domain;

public class Customer : IEntity
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalId { get; set; }
    public string Phone { get; set; }
    public string PhoneCountryCode { get; set; }
    public string Password { get; set; }
    
}
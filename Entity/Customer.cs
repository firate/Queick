namespace Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalId { get; set; }
    public string Phone { get; set; }
    public string PhoneCountryCode { get; set; }
    public string Password { get; set; }
}
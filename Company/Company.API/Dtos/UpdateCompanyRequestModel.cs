namespace Company.API.Controllers;

public record UpdateCompanyRequestModel
{
    public string Name { get; set; }
    public string Description { get; set; }
}
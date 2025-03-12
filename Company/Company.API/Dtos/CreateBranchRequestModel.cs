namespace Company.API.Controllers;

public record CreateBranchRequestModel(string Name, long CompanyId, string Description);
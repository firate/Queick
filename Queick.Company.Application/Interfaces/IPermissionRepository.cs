using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IPermissionRepository
{
    Task<Permission> GetByIdAsync(long id);
    Task<Permission> GetByCodeAsync(string code);
    Task<List<Permission>> GetAllAsync();
    Task<List<Permission>> GetByCodesAsync(List<string> codes);
    Task SeedPermissionsAsync();
}
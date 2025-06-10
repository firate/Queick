using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IPermissionRepository: IBaseRepository<Permission>
{
    Task<List<Permission>> GetAllAsync();
    Task<List<Permission>> GetByCodesAsync(List<string> codes);
 
}
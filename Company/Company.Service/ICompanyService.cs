
using Appointment.Service.Helpers;
using Company.Entity;

namespace Appointment.Service;

public interface ICompanyService
{
    Task<CompanyEntity> CreateCompanyAsync(string name, string description);
    Task<CompanyEntity> GetCompanyByIdAsync(long id);
    Task<PaginationResult<CompanyEntity>> GetCompaniesAsync(string name, string description, int page, int pageSize,
        string sortBy, string sortDirection = "asc");
    
    // update company
    Task<CompanyEntity> UpdateCompanyAsync(long id, string name, string description);
    
    // delete company
    Task<bool> DeleteCompanyAsync(long id);    
}
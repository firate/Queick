using Queick.Company.Application.Common.Specification;
using Queick.Company.Application.DTOs;
using Queick.Company.Domain;
using CompanyDto = Queick.Company.Application.Common.Models.CompanyDto;

namespace Queick.Company.Application.Services.Interfaces;


/// <summary>
/// Şirket servisi için arayüz
/// </summary>
public interface ICompanyService
{
    Task<CompanyDto> GetCompanyByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<List<CompanyDto>> GetCompaniesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken = default);
    Task UpdateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken = default);
    Task DeleteCompanyAsync(long id, CancellationToken cancellationToken = default);
        
    // Task<List<CompanyDto>> CreateCompaniesAsync(List<CompanyDto> companiesDto, CancellationToken cancellationToken = default);
    // Task UpdateCompaniesAsync(List<CompanyDto> companiesDto, CancellationToken cancellationToken = default);
    // Task DeleteCompaniesAsync(List<long> ids, CancellationToken cancellationToken = default);
    //     
    // Task<TransferResultDto> TransferEmployeeBetweenCompaniesAsync(
    //     long employeeId, long sourceCompanyId, long targetCompanyId, 
    //     CancellationToken cancellationToken = default);
    //         
    // Task MergeCompaniesAsync(long targetCompanyId, List<long> companiesToMergeIds, CancellationToken cancellationToken = default);
}

#region Specification Sınıfları
    
/// <summary>
/// Şirket ID'sine göre specification
/// </summary>
// public class CompanyByIdSpecification : BaseSpecification<CompanyDomain>
// {
//     public CompanyByIdSpecification(long id) : base(c => c.Id == id)
//     {
//     }
// }
//     
//   
// /// <summary>
// /// Çoklu ID'lere göre şirket specification
// /// </summary>
// public class CompaniesByIdsSpecification : BaseSpecification<CompanyDomain>
// {
//     public CompaniesByIdsSpecification(IEnumerable<long> ids) : base(c => ids.Contains(c.Id))
//     {
//     }
// }
//     
// /// <summary>
// /// Çalışan ID'sine göre specification, şirket bilgisiyle birlikte
// /// </summary>
// public class EmployeeByIdSpecification : BaseSpecification<EmployeeDomain>
// {
//     public EmployeeByIdSpecification(long id) : base(e => e.Id == id)
//     {
//         AddInclude(e => e.Company);
//     }
// }
//     
// /// <summary>
// /// Şirket ID'lerine göre çalışan specification
// /// </summary>
// public class EmployeesByCompanyIdsSpecification : BaseSpecification<EmployeeDomain>
// {
//     public EmployeesByCompanyIdsSpecification(IEnumerable<long> companyIds) : base(e => companyIds.Contains(e.CompanyId))
//     {
//         AddInclude(e => e.Company);
//     }
// }
    
#endregion
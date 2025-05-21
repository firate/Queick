using CompanyDto = Queick.Company.Application.Common.Models.CompanyDto;

namespace Queick.Company.Application.Services.Interfaces;


/// <summary>
/// Şirket servisi için arayüz
/// </summary>
public interface ICompanyService
{
    Task<CompanyDto>? GetCompanyByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<List<CompanyDto>> GetCompaniesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken = default);
    Task<CompanyDto> UpdateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteCompanyAsync(long id, CancellationToken cancellationToken = default);
        
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

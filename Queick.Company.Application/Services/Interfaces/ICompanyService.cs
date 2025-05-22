using Queick.Company.Application.Common.Models;
using CompanyDto = Queick.Company.Application.Common.Models.CompanyDto;

namespace Queick.Company.Application.Services.Interfaces;


/// <summary>
/// Şirket servisi için arayüz
/// </summary>
public interface ICompanyService
{
    Task<CompanyDto>? GetCompanyByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<List<CompanyDto>> GetCompaniesAsync(CompanySearchRequestDto companySearchRequestDto, CancellationToken cancellationToken = default);
    Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto companyCreationDto, CancellationToken cancellationToken = default);
    Task<CompanyDto> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteCompanyAsync(long id, CancellationToken cancellationToken = default);
    Task<List<CompanyDto>> CreateCompaniesAsync(List<CompanyCreationDto> companiesDto, CancellationToken cancellationToken = default);
    
    
    // TODO: çoklu işlemleri doğrudan rabbitmq'ya gönderelim, başka bir servis üzerinden işlensin
    Task<bool> UpdateCompaniesAsync(List<CompanyDto> companiesDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteCompaniesAsync(List<long> ids, CancellationToken cancellationToken = default);
         
    // Task<TransferResultDto> TransferEmployeeBetweenCompaniesAsync( long employeeId, long sourceCompanyId, long targetCompanyId, CancellationToken cancellationToken = default);
             
    // Task MergeCompaniesAsync(long targetCompanyId, List<long> companiesToMergeIds, CancellationToken cancellationToken = default);
}

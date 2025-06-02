using Queick.Company.Application.DTOs;
using Queick.Company.Domain;
using Riok.Mapperly.Abstractions;

namespace Queick.Company.Application.Mapper;

// RequiredMappingStrategy.None ile Map'lenmemiş field'lar için uyarıları kapatırız.
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class ApplicationMapper: IApplicationMapper
{
    [MapProperty(nameof(CompanyDomain.Name), nameof(CompanyDto.Name))]
    [MapProperty(nameof(CompanyDomain.Description), nameof(CompanyDto.Description))]
    [MapProperty(nameof(CompanyDomain.Id), nameof(CompanyDto.Id))]
    public partial CompanyDto CompanyToCompanyDto(CompanyDomain company);
    
    public partial List<CompanyDto> CompanyListToCompanyDtoList(List<CompanyDomain> companies);
    
    public partial BranchDto BranchToBranchDto(Branch branch);
    
}
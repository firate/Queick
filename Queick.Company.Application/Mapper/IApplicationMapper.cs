using Queick.Company.Application.DTOs;
using Queick.Company.Domain;

namespace Queick.Company.Application.Mapper;

public interface IApplicationMapper
{
    CompanyDto CompanyToCompanyDto(CompanyDomain company);
    
    List<CompanyDto> CompanyListToCompanyDtoList(List<CompanyDomain> companies);
}
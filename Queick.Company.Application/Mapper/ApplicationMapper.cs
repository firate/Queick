using Queick.Company.Application.DTOs;
using Queick.Company.Domain;
using Riok.Mapperly.Abstractions;

namespace Queick.Company.Application.Mapper;

public interface IApplicationMapper
{
    CompanyDto CompanyToCompanyDto(CompanyDomain company);
}


[Mapper]
public partial class ApplicationMapper
{
    public partial CompanyDto CompanyToCompanyDto(CompanyDomain company);
}
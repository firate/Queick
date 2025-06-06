using Queick.Company.Application.DTOs;
using Queick.Company.Domain;

namespace Queick.Company.Application.Mapper;

public interface IApplicationMapper
{
    CompanyDto CompanyToCompanyDto(CompanyDomain company);
    
    List<CompanyDto> CompanyListToCompanyDtoList(List<CompanyDomain> companies);
    
    Branch BranchCreationDtoToBranch(BranchCreationDto branch);
    BranchDto BranchToBranchDto(Branch branch);
    
    List<BranchDto> BranchListToBranchDtoList(List<Branch> branches);
    
    Address BranchAddressCreationDtoToAddress(BranchAddressCreationDto address);
    BranchAddressCreationDto AddressToBranchAddressCreationDto(Address address);
    BranchAddressDto AddressToBranchAddressDto(Address address);
}
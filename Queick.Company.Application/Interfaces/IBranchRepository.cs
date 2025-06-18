using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IBranchRepository : IBaseRepository<Branch>
{
    Task<bool> IsBranchNameExistsInCompanyAsync(string name, Guid companyId,
        CancellationToken cancellationToken = default);

    Task<(List<Branch> Branches, int Count)> GetPagedAsync(
        Guid companyId,
        int skip,
        int take,
        string? name,
        string? description,
        bool onlyActiveRecords = true,
        bool includeDeletedRecords = false,
        CancellationToken cancellationToken = default);

    Task<Address> AddAddressAsync(Address address, CancellationToken cancellationToken = default);

    Task<Address?> GetAddressByBranchAndAddressIdAsync(Guid branchId, Guid addressId, CancellationToken cancellationToken = default);

    Task<Address?> GetPrimaryAddressByFunctionTypeAsync(Guid branchId, int addressFunctionType, CancellationToken cancellationToken = default);
    
    Task<(List<Address>, int totalCount)> GetAddressesByBranchIdAsync(Guid branchId,
        int page, int pageSize, CancellationToken cancellationToken = default);

    Task<Address> UpdateAddressAsync(Address address, CancellationToken cancellationToken = default);

    Task DeleteAddressAsync(Guid branchId, Guid addressId, CancellationToken cancellationToken = default);
}
using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IBranchRepository : IBaseRepository<Branch>
{
    Task<bool> IsBranchNameExistsInCompanyAsync(string name, long companyId,
        CancellationToken cancellationToken = default);

    Task<(List<Branch> Branches, int Count)> GetPagedAsync(
        long companyId,
        int skip,
        int take,
        string? name,
        string? description,
        bool onlyActiveRecords = true,
        bool includeDeletedRecords = false,
        CancellationToken cancellationToken = default);

    Task<Address> AddAddressAsync(Address address, CancellationToken cancellationToken = default);

    Task<Address?> GetAddressByBranchAndAddressIdAsync(long branchId, long addressId, CancellationToken cancellationToken = default);

    Task<Address?> GetPrimaryAddressByFunctionTypeAsync(long branchId, int addressFunctionType, CancellationToken cancellationToken = default);
    
    Task<(List<Address>, int totalCount)> GetAddressesByBranchIdAsync(int branchId,
        int page, int pageSize, CancellationToken cancellationToken = default);

    Task<Address> UpdateAddressAsync(Address address, CancellationToken cancellationToken = default);

    Task DeleteAddressAsync(long branchId, long addressId, CancellationToken cancellationToken = default);
}
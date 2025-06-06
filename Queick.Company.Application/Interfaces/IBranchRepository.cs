using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IBranchRepository : IBaseRepository<Branch>
{
    Task<List<Branch>> GetBranchesByCompanyIdAsync(long companyId, CancellationToken cancellationToken = default);

    Task<bool> IsBranchNameExistsInCompanyAsync(string name, long companyId,
        CancellationToken cancellationToken = default);

    Task<(List<Branch> Branches, int Count)> GetPagedAsync(
        long companyId, 
        string name, 
        string description, 
        bool onlyActiveRecords,
        bool includeDeletedRecords,
        int skip,
        int take,
        CancellationToken cancellationToken = default);

    Task<Address> AddAddressAsync(Address address, CancellationToken cancellationToken = default);

    Task<Address?> GetAddressByIdAsync(long addressId, CancellationToken cancellationToken = default);

    Task<(List<Address>, int totalCount)> GetAddressesByBranchIdAsync(int branchId,
        int page, int pageSize, CancellationToken cancellationToken = default);

    Task<Address> UpdateAddressAsync(Address address, CancellationToken cancellationToken = default);

    Task DeleteAddressAsync(int addressId, CancellationToken cancellationToken = default);
}
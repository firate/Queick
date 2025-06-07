using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Interfaces;
using Queick.Company.Domain;
using Queick.Company.Persistence.Repositories.Base;

namespace Queick.Company.Persistence.Repositories;

public class BranchRepository : BaseRepository<Branch>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<(List<Branch> Branches, int Count)> GetPagedAsync(
        long companyId,
        int skip,
        int take,
        string? name,
        string? description,
        bool onlyActiveRecords = true,
        bool includeDeletedRecords = false,
        CancellationToken cancellationToken = default)
    {
        List<Branch> branches = [];

        var query = _context.Branches.Include(b => b.Company).AsQueryable();

        if (companyId > 0)
        {
            query = query.Where(x => x.CompanyId == companyId);
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            query = query.Where(x => !string.IsNullOrWhiteSpace(x.Description) && x.Description.Contains(description));
        }

        if (onlyActiveRecords)
        {
            query = query.Where(x => x.IsActive);
        }

        if (!includeDeletedRecords)
        {
            query = query.Where(x => !x.IsDeleted);
        }

        var count = query.Count();

        if (count <= 0) return (branches, 0);


        branches = await query.Skip(skip).Take(take).ToListAsync();
        return (branches, count);
    }


    public async Task<bool> IsBranchNameExistsInCompanyAsync(string name, long companyId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (companyId <= 0)
        {
            return false;
        }

        var query = _context.Branches.AsQueryable();
        query = query.Where(b => b.CompanyId == companyId);

        return await query.AnyAsync(b => string.Equals(b.Name, name, StringComparison.InvariantCultureIgnoreCase),
            cancellationToken);
    }


    public async Task SetAddressAsPrimaryAsync(long branchId, long addressId, AddressFunctionType functionType,
        bool isPrimary)
    {
        var existingPrimaryAddresses = await _context.Addresses
            .Where(a => a.BranchId == branchId &&
                        a.AddressFunctionType == functionType &&
                        a.IsPrimary)
            .ToListAsync();

        foreach (var address in existingPrimaryAddresses)
        {
            address.IsPrimary = false;
        }

        var currentAddressToBePrimary =
            await _context.Addresses.FirstOrDefaultAsync(a => a.BranchId == branchId && a.Id == addressId);

        if (currentAddressToBePrimary == null)
        {
            throw new Exception("Address not found");
        }


        currentAddressToBePrimary.IsPrimary = true;


        _context.Addresses.Update(currentAddressToBePrimary);

        await _context.SaveChangesAsync();
    }

    // Address CRUD metodları
    public async Task<Address> AddAddressAsync(Address address, CancellationToken cancellationToken = default)
    {
        var result = await _context.Addresses.AddAsync(address, cancellationToken);
        return result.Entity;
    }

    public async Task<Address?> GetAddressByBranchAndAddressIdAsync(long branchId, long addressId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Addresses
            .Include(a => a.Branch)
            .FirstOrDefaultAsync(a => a.BranchId == branchId && a.Id == addressId && !a.IsDeleted, cancellationToken);
    }


    public async Task<Address?> GetPrimaryAddressByFunctionTypeAsync(long branchId, int addressFunctionType,
        CancellationToken cancellationToken = default)
    {
        return await _context.Addresses
            .Include(a => a.Branch)
            .FirstOrDefaultAsync(
                a => a.BranchId == branchId && (int)a.AddressFunctionType == addressFunctionType && !a.IsDeleted,
                cancellationToken);
    }

    public async Task<(List<Address>, int totalCount)> GetAddressesByBranchIdAsync(int branchId,
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Addresses
            .Where(a => a.BranchId == branchId && !a.IsDeleted);

        var totalCount = await query.CountAsync(cancellationToken);

        var addresses = await query.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (addresses, totalCount);
    }

    public async Task<Address> UpdateAddressAsync(Address address, CancellationToken cancellationToken = default)
    {
        _context.Addresses.Update(address);
        return await Task.FromResult(address);
    }


    public async Task DeleteAddressAsync(long branchId, long addressId, CancellationToken cancellationToken = default)
    {
        var address = await GetAddressByBranchAndAddressIdAsync(branchId, addressId, cancellationToken);
        if (address != null)
        {
            address.IsDeleted = true; // Soft delete
            address.Updated = DateTimeOffset.UtcNow;
        }
    }
}
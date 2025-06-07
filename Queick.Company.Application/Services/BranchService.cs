using Queick.Company.Application.Common;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Exceptions;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Mapper;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Application.Services;

public class BranchService : IBranchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationMapper _mapper;

    public BranchService(IUnitOfWork unitOfWork, IApplicationMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<BranchDto?> GetBranchByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var branch =
            await _unitOfWork.Branches.GetFirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

        if (branch is null)
        {
            throw new NotFoundException(nameof(Domain.Branch), id);
        }

        var dto = _mapper.BranchToBranchDto(branch);

        return dto;
    }

    public async Task<PaginatedList<BranchDto>> GetBranchsAsync(BranchSearchRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var branchAndCount = await _unitOfWork.Branches.GetPagedAsync(
            companyId: dto.CompanyId,
            name: dto?.Name,
            description: dto?.Description,
            onlyActiveRecords: dto.OnlyActives,
            includeDeletedRecords: dto.IncludeDeletedRecords,
            skip: ((dto.Page - 1) * dto.PageSize),
            take: dto.PageSize,
            cancellationToken: cancellationToken);

        var (branches, count) = branchAndCount;

        if (count == 0)
        {
            throw new NotFoundException(nameof(Domain.Branch));
        }

        var branchListDto = _mapper.BranchListToBranchDtoList(branches);

        return new PaginatedList<BranchDto>(branchListDto, count, dto.Page, dto.PageSize);
    }

    public async Task<BranchDto> CreateBranchAsync(BranchCreationDto dto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        if (dto.CompanyId <= 0)
        {
            throw new ArgumentException("Invalid CompanyId", nameof(dto.CompanyId));
        }

        var branch = _mapper.BranchCreationDtoToBranch(dto);
        branch.Created = DateTimeOffset.Now;
        branch.Updated = DateTimeOffset.Now;

        var createdBranch = await _unitOfWork.Branches.AddAsync(branch, cancellationToken);

        if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
        {
            // TODO: custom exception
            throw new Exception();
        }

        var resultDto = _mapper.BranchToBranchDto(createdBranch);

        return resultDto;
    }

    public async Task<BranchDto> UpdateBranchAsync(BranchUpdateDto dto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var branch = await _unitOfWork.Branches
            .GetFirstOrDefaultAsync(x => x.Name == dto.Name && !x.IsDeleted, cancellationToken);

        if (branch is null)
        {
            throw new NotFoundException(nameof(Domain.Branch));
        }

        branch.Name = dto.Name;
        branch.Description = dto.Description;
        branch.IsActive = dto.IsActive;

        await _unitOfWork.Branches.UpdateAsync(branch, cancellationToken);
        if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
        {
            throw new Exception();
        }

        var resultDto = _mapper.BranchToBranchDto(branch);

        return resultDto;
    }

    public async Task<bool> DeleteBranchAsync(long id, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.Branches.DeleteAsync(id, cancellationToken);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<BranchAddressDto> CreateAddressAsync(BranchAddressCreationDto dto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        var branch =
            await _unitOfWork.Branches.GetFirstOrDefaultAsync(x => x.Id == dto.BranchId && !x.IsDeleted,
                cancellationToken);

        if (branch is null)
        {
            throw new NotFoundException(nameof(Domain.Branch));
        }

        var address =  _mapper.BranchAddressCreationDtoToAddress(dto);
        
        address.Created = DateTimeOffset.UtcNow;
        address.Updated = DateTimeOffset.UtcNow;
        address.BranchId = branch.Id;

        var createdAddress = await _unitOfWork.Branches.AddAddressAsync(address, cancellationToken);

        return _mapper.AddressToBranchAddressDto(createdAddress);
    }

    public async Task<BranchAddressDto> GetBranchAddressByIdAsync(long branchId, long addressId,
        CancellationToken cancellationToken = default)
    {
        var address = await _unitOfWork.Branches.GetAddressByBranchAndAddressIdAsync(branchId, addressId, cancellationToken);

        if (address is null)
        {
            throw new NotFoundException(nameof(Domain.Branch));
        }
        
        return _mapper.AddressToBranchAddressDto(address);
    }

    public async Task<BranchAddressDto> GetBranchPrimaryAddressByAddressFunctionTypeAsync(long branchId,
        int addressFunctionTypeId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BranchAddressDto> UpdateAddressAsync(BranchAddressUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedList<BranchAddressDto>> GetAddressesPagedAsync(BranchAddressSearchDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BranchAddressDto> UpdateAddressAsPrimaryForBranchAsync(long branchId, long addressId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBranchAddressAsync(long branchId, long addressId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #region Message Queue Implementasyonunu Bekleyen Operasyonlar

    // TODO: çoklu işlemleri message queue üzerinden yapalım
    public async Task<object> CreateBranchsAsync(List<BranchCreationDto> dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<object> UpdateBranchsAsync(List<BranchDto> dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<object> DeleteBranchsAsync(List<long> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion
}
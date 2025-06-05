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

        var branch = new Branch()
        {
            Name = dto.Name,
            Description = dto.Description,
            CompanyId = dto.CompanyId,
            Created = DateTimeOffset.UtcNow,
            Updated = DateTimeOffset.UtcNow,
            IsActive = dto.IsActive
        };

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

        var branch =
            await _unitOfWork.Branches.GetFirstOrDefaultAsync(x => x.Name == dto.Name && !x.IsDeleted,
                cancellationToken);

        if (branch is null)
        {
            throw new NotFoundException(nameof(Domain.Branch));
        }

        branch.Name = dto.Name;
        branch.Description = dto.Description;
        branch.IsActive = dto.IsActive;
        branch.IsPrimary = branch.IsPrimary;


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
}
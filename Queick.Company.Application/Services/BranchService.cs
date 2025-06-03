using Queick.Company.Application.Common;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Exceptions;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Mapper;
using Queick.Company.Application.Services.Interfaces;

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


    public async Task<BranchDto>? GetBranchByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var branch = await _unitOfWork.Branches.GetFirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (branch is null)
        {
            throw new NotFoundException(nameof(Domain.Branch), id);
        }
        
        var dto = _mapper.BranchToBranchDto(branch);

        return dto;
    }

    public async Task<PaginatedList<BranchDto>> GetBranchsAsync(BranchSearchRequestDto dto, CancellationToken cancellationToken = default)
    {
        // var companies = await _unitOfWork.Branches.GetPagedAsync();
        //
        // if (companies.Count == 0)
        // {
        //     
        // }
        throw new NotImplementedException();
        
    }

    public async Task<BranchDto> CreateBranchAsync(BranchCreationDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BranchDto> UpdateBranchAsync(BranchUpdateDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBranchAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BranchDto>> CreateBranchsAsync(List<BranchCreationDto> dto, CancellationToken cancellationToken = default)
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
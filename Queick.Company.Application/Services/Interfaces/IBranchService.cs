using Queick.Company.Application.Common;
using Queick.Company.Application.DTOs;

namespace Queick.Company.Application.Services.Interfaces;

public interface IBranchService
{
    Task<BranchDto>? GetBranchByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PaginatedList<BranchDto>> GetBranchsAsync(BranchSearchRequestDto dto, CancellationToken cancellationToken = default);
    Task<BranchDto> CreateBranchAsync(BranchCreationDto dto, CancellationToken cancellationToken = default);
    Task<BranchDto> UpdateBranchAsync(BranchUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteBranchAsync(long id, CancellationToken cancellationToken = default);
    Task<List<BranchDto>> CreateBranchsAsync(List<BranchCreationDto> dto, CancellationToken cancellationToken = default);
    
    
    // TODO: çoklu işlemleri doğrudan rabbitmq'ya gönderelim, başka bir servis üzerinden işlensin
    Task<bool> UpdateBranchsAsync(List<BranchDto> dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteBranchsAsync(List<long> ids, CancellationToken cancellationToken = default);
         
    
}
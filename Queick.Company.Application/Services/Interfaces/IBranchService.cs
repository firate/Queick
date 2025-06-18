using Queick.Company.Application.Common;
using Queick.Company.Application.DTOs;

namespace Queick.Company.Application.Services.Interfaces;

public interface IBranchService
{
    Task<BranchDto?> GetBranchByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginatedList<BranchDto>> GetBranchsAsync(BranchSearchRequestDto dto,
        CancellationToken cancellationToken = default);

    Task<BranchDto> CreateBranchAsync(BranchCreationDto dto, CancellationToken cancellationToken = default);
    Task<BranchDto> UpdateBranchAsync(BranchUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteBranchAsync(Guid id, CancellationToken cancellationToken = default);

    Task<BranchAddressDto> CreateAddressAsync(BranchAddressCreationDto dto,
        CancellationToken cancellationToken = default);
    Task<BranchAddressDto> GetBranchAddressByIdAsync(Guid branchId, Guid addressId,
        CancellationToken cancellationToken = default);
    Task<BranchAddressDto> GetBranchPrimaryAddressByAddressFunctionTypeAsync(Guid branchId, int addressFunctionTypeId,
        CancellationToken cancellationToken = default);
    
    Task<BranchAddressDto> UpdateAddressAsync(BranchAddressUpdateDto dto,
        CancellationToken cancellationToken = default);
    Task<PaginatedList<BranchAddressDto>> GetAddressesPagedAsync(BranchAddressSearchDto dto,
        CancellationToken cancellationToken = default);
    Task<BranchAddressDto> UpdateAddressAsPrimaryForBranchAsync(Guid branchId, Guid addressId, CancellationToken cancellationToken = default);

    Task<bool> DeleteBranchAddressAsync(Guid branchId, Guid addressId, CancellationToken cancellationToken = default);

    // TODO: çoklu işlemleri doğrudan rabbitmq'ya gönderelim, başka bir servis üzerinden işlensin
    Task<object> CreateBranchsAsync(List<BranchCreationDto> dto, CancellationToken cancellationToken = default);
    Task<object> UpdateBranchsAsync(List<BranchDto> dto, CancellationToken cancellationToken = default);
    Task<object> DeleteBranchsAsync(List<Guid> ids, CancellationToken cancellationToken = default);
}
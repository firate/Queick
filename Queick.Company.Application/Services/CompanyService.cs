using Queick.Company.Application.Common;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Exceptions;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Mapper;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;
using Queick.Company.Domain.Exceptions;
using Queick.Company.Domain.ValueObjects;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CompanyService(IUnitOfWork unitOfWork, IApplicationMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }


    #region Temel CRUD İşlemleri

    public async Task<CompanyDto>? GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var company =
            await _unitOfWork.Companies.GetFirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

        if (company is null)
        {
            return null;
        }

        var cDto = _mapper.CompanyToCompanyDto(company);

        return cDto;
    }

    public async Task<PaginatedList<CompanyDto>> GetCompaniesAsync(CompanySearchRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var companyCountAndList = await _unitOfWork.Companies.GetPagedAsync(
            name: dto.Name,
            description: dto?.Description,
            skip: (dto.Page - 1) * dto.PageSize,
            take: dto.PageSize,
            createdFrom: dto.CreatedFrom,
            createdTo: dto.CreatedTo,
            onlyActives: dto.OnlyActives,
            onlyDeletedRecords: dto.OnlyDeleteds,
            cancellationToken);

        if (companyCountAndList.Count == 0)
        {
            return new PaginatedList<CompanyDto>([], 0, dto.Page, dto.PageSize);
        }

        var dtoList = _mapper.CompanyListToCompanyDtoList(companyCountAndList.Companies);

        return new PaginatedList<CompanyDto>(dtoList, dtoList.Count, dto.Page, dto.PageSize);
    }

    public async Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto dto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        CompanyName name = CompanyName.Create(dto.Name);

        var currentUser = _currentUserService.GetCurrentUserId();

        var company = new CompanyDomain(name, dto?.Description, currentUser, currentUser);

        var addedCompany = await _unitOfWork.Companies.AddAsync(company, cancellationToken);
        var isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!isSaved)
        {
            throw new Exception("Company not created.");
        }

        var companyResultDto = _mapper.CompanyToCompanyDto(addedCompany);

        return companyResultDto;
    }

    public async Task<CompanyDto> UpdateCompanyAsync(CompanyUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == dto.Id, cancellationToken);

        if (company is null)
        {
            throw new NotFoundException($"Company with Id {dto.Id} not found.");
        }

        //company.Name = dto.Name;

        // company.Description = dto!.Description ?? string.Empty;
        // company.IsActive = dto.IsActive;


        await _unitOfWork.Companies.UpdateAsync(company, cancellationToken);

        if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
        {
            throw new Exception("Company not updated.");
        }

        return _mapper.CompanyToCompanyDto(company);
    }

    public async Task<bool> DeleteCompanyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (company is null)
        {
            throw new CompanyNotFoundException();
        }
        
        var currentUser = _currentUserService.GetCurrentUserId();
        
        company.MarkAsDeleted(currentUser);
        
        await _unitOfWork.Companies.SoftDeleteAsync(id, cancellationToken);
        
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }

    #endregion

    #region Toplu İşlemler

    public async Task<List<CompanyDto>> CreateCompaniesAsync(List<CompanyCreationDto> companyCreationDtos,
        CancellationToken cancellationToken = default)
    {
        //TODO: create event for message queue(like RabbitMQ)
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateCompaniesAsync(List<CompanyDto> companiesDto,
        CancellationToken cancellationToken = default)
    {
        //TODO: create event for message queue(like RabbitMQ)
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCompaniesAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        //TODO: create event for message queue(like RabbitMQ)
        throw new NotImplementedException();
    }

    #endregion

    #region Kompleks İşlemler

    public async Task<object> TransferEmployeeBetweenCompaniesAsync(
        Guid employeeId, Guid sourceCompanyId, Guid targetCompanyId,
        CancellationToken cancellationToken = default)
    {
        // TODO: instant action, no message queue
        throw new NotImplementedException();
    }

    #endregion
}
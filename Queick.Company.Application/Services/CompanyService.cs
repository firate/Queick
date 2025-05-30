using Queick.Company.Application.Common;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Exceptions;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Mapper;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationMapper _mapper;

    public CompanyService(IUnitOfWork unitOfWork, IApplicationMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    #region Temel CRUD İşlemleri

    public async Task<CompanyDto>? GetCompanyByIdAsync(long id, CancellationToken cancellationToken = default)
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
        var companyList = await _unitOfWork.Companies.GetPagedAsync(
            (dto.Page * dto.PageSize) - 1,
            dto.PageSize,
            null,
            cancellationToken);

        if (companyList.Count == 0)
        {
            return new PaginatedList<CompanyDto>([], 0, dto.Page, dto.PageSize);
        }

        var dtoList = _mapper.CompanyListToCompanyDtoList(companyList);

        return new PaginatedList<CompanyDto>(dtoList, dtoList.Count, dto.Page, dto.PageSize);
    }

    public async Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto companyCreationDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(companyCreationDto);

        var company = new CompanyDomain
        {
            Name = companyCreationDto.Name,
            Description = companyCreationDto.Description ?? string.Empty
        };

        var addedCompany = await _unitOfWork.Companies.AddAsync(company, cancellationToken);

        var isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!isSaved)
        {
            throw new Exception("Company not updated.");
        }

        var companyResultDto = _mapper.CompanyToCompanyDto(addedCompany);

        return companyResultDto;
    }

    public async Task<CompanyDto> UpdateCompanyAsync(CompanyUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        if (dto is null)
        {
            throw new NotFoundException($"Company not found.");
        }

        var company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == dto.Id, cancellationToken);

        if (company is null)
        {
            throw new NotFoundException($"Company with ID {dto.Id} not found.");
        }

        company.Name = dto.Name;
        company.Description = dto!.Description ?? string.Empty;
        company.IsDeleted = dto!.IsDeleted;

        await _unitOfWork.Companies.UpdateAsync(company, cancellationToken);
        var isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!isSaved)
        {
            throw new Exception("Company not updated.");
        }

        return _mapper.CompanyToCompanyDto(company);
    }

    public async Task<bool> DeleteCompanyAsync(long id, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (company is null)
        {
            throw new NotFoundException($"Company with ID {id} not found.");
        }

        company.IsDeleted = true;
        await _unitOfWork.Companies.UpdateAsync(company, cancellationToken);
        var isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        return isSaved;
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

    public async Task<bool> DeleteCompaniesAsync(List<long> ids, CancellationToken cancellationToken = default)
    {
        //TODO: create event for message queue(like RabbitMQ)
        throw new NotImplementedException();
    }

    #endregion
    
    #region Kompleks İşlemler
    public async Task<object> TransferEmployeeBetweenCompaniesAsync(
        long employeeId, long sourceCompanyId, long targetCompanyId,
        CancellationToken cancellationToken = default)
    {
        // TODO: instant action, no message queue
        throw new NotImplementedException();
    }
    
    #endregion
}
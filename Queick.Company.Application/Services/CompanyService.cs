using Queick.Company.Application.Common;
using Queick.Company.Application.Common.Models;
using Queick.Company.Application.DTOs;
using Queick.Company.Application.Exceptions;
using Queick.Company.Application.Interfaces;
using Queick.Company.Application.Services.Interfaces;
using Queick.Company.Domain;

/// <summary>
/// Company servisi implementasyonu
/// </summary>
public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description
        };
    }

    public async Task<PaginatedList<CompanyDto>> GetCompaniesAsync(CompanySearchRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var companyList = await _unitOfWork.Companies.GetPagedAsync(
            (dto.Page * dto.PageSize) - 1,
            dto.PageSize,
            null,
            cancellationToken);

        var dtoList = companyList.Select(c => new CompanyDto()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }
        ).ToList();

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

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //var companyResultDto = 
        
        var companyResultDto = new CompanyDto()
        {
            Id = addedCompany.Id,
            Name = addedCompany.Name,
            Description = addedCompany.Description
        };

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

        return new CompanyDto()
        {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description
        };
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

    public async Task<List<CompanyDto>> CreateCompaniesAsync(List<CompanyCreationDto> companyCreationDtos,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateCompaniesAsync(List<CompanyDto> companiesDto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCompaniesAsync(List<long> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region Toplu İşlemler

    // public async Task<List<CompanyDto>> CreateCompaniesAsync(List<CompanyDto> companiesDto,
    //     CancellationToken cancellationToken = default)
    // {
    //     var companies = companiesDto.Select(dto => new CompanyDomain
    //     {
    //         Name = dto.Name,
    //         Description = dto.Description
    //     }).ToList();
    //
    //     // Değişiklikleri takip et
    //     var addedCompanies = await _dbContext.AddRangeAsync(companies, cancellationToken);
    //
    //     // Değişiklikleri kaydet
    //     await _dbContext.SaveChangesAsync(cancellationToken);
    //
    //     // ID'leri DTO'lara eşle
    //     for (int i = 0; i < addedCompanies.Count; i++)
    //     {
    //         companiesDto[i].Id = addedCompanies[i].Id;
    //     }
    //
    //     return companiesDto;
    // }


    // public async Task UpdateCompaniesAsync(List<CompanyDto> companiesDto, CancellationToken cancellationToken = default)
    // {
    //     // Tüm company'leri ID'leri ile birlikte getir
    //     var ids = companiesDto.Select(dto => dto.Id).ToList();
    //     var spec = new CompaniesByIdsSpecification(ids);
    //     var existingCompanies = await _dbContext.ListAsync(spec, cancellationToken);
    //
    //     // ID bazlı hızlı erişim için dictionary oluştur
    //     var companiesById = existingCompanies.ToDictionary(c => c.Id);
    //
    //     // Bulunamayan şirketleri kontrol et
    //     var missingIds = ids.Where(id => !companiesById.ContainsKey(id)).ToList();
    //     if (missingIds.Any())
    //     {
    //         throw new NotFoundException($"Companies with IDs {string.Join(", ", missingIds)} not found.");
    //     }
    //
    //     // Var olan şirketleri güncelle
    //     foreach (var dto in companiesDto)
    //     {
    //         var company = companiesById[dto.Id];
    //         company.Name = dto.Name;
    //         company.Description = dto.Description;
    //     }
    //
    //     // Değişiklikleri takip et
    //     await _dbContext.UpdateRangeAsync(existingCompanies, cancellationToken);
    //
    //     // Değişiklikleri kaydet
    //     await _dbContext.SaveChangesAsync(cancellationToken);
    // }

    // public async Task DeleteCompaniesAsync(List<long> ids, CancellationToken cancellationToken = default)
    // {
    //     // ExecuteInTransactionAsync kullanarak atomik bir işlem olarak gerçekleştir
    //     await _dbContext.ExecuteInTransactionAsync(async () =>
    //     {
    //         var spec = new CompaniesByIdsSpecification(ids);
    //         var companies = await _dbContext.ListAsync(spec, cancellationToken);
    //
    //         // Bulunamayan şirketleri kontrol et
    //         if (companies.Count != ids.Count)
    //         {
    //             var foundIds = companies.Select(c => c.Id).ToHashSet();
    //             var missingIds = ids.Where(id => !foundIds.Contains(id)).ToList();
    //             throw new NotFoundException($"Companies with IDs {string.Join(", ", missingIds)} not found.");
    //         }
    //
    //         // Şirketleri sil
    //         await _dbContext.RemoveRangeAsync(companies, cancellationToken);
    //
    //         // Değişiklikleri kaydet
    //         await _dbContext.SaveChangesAsync(cancellationToken);
    //
    //         return true;
    //     }, cancellationToken);
    // }
    //
    // #endregion
    //
    // #region Kompleks İşlemler
    //
    // public async Task<TransferResultDto> TransferEmployeeBetweenCompaniesAsync(
    //     long employeeId, long sourceCompanyId, long targetCompanyId,
    //     CancellationToken cancellationToken = default)
    // {
    //     // ExecuteInTransactionAsync kullanarak atomik bir işlem olarak gerçekleştir
    //     return await _dbContext.ExecuteInTransactionAsync(async () =>
    //     {
    //         // İlgili entity'leri getir
    //         var employeeSpec = new EmployeeByIdSpecification(employeeId);
    //         var employee = await _dbContext.FirstOrDefaultAsync(employeeSpec, cancellationToken);
    //
    //         var sourceCompanySpec = new CompanyByIdSpecification(sourceCompanyId);
    //         var sourceCompany = await _dbContext.FirstOrDefaultAsync(sourceCompanySpec, cancellationToken);
    //
    //         var targetCompanySpec = new CompanyByIdSpecification(targetCompanyId);
    //         var targetCompany = await _dbContext.FirstOrDefaultAsync(targetCompanySpec, cancellationToken);
    //
    //         // Validasyonlar
    //         if (employee == null)
    //             throw new NotFoundException($"Employee with ID {employeeId} not found.");
    //
    //         if (sourceCompany == null)
    //             throw new NotFoundException($"Source company with ID {sourceCompanyId} not found.");
    //
    //         if (targetCompany == null)
    //             throw new NotFoundException($"Target company with ID {targetCompanyId} not found.");
    //
    //         if (employee.CompanyId != sourceCompanyId)
    //             throw new InvalidOperationException("Employee does not belong to the source company.");
    //
    //         // Değişiklikler
    //         employee.CompanyId = targetCompanyId;
    //         employee.Company = targetCompany;
    //
    //         // Audit ve kayıt izleme için transfer kaydı oluştur
    //         var transferRecord = new EmployeeTransferRecord
    //         {
    //             EmployeeId = employeeId,
    //             SourceCompanyId = sourceCompanyId,
    //             TargetCompanyId = targetCompanyId,
    //             TransferDate = DateTime.UtcNow,
    //             Notes = "Employee transferred between companies"
    //         };
    //
    //         // Değişiklikleri takip et
    //         await _dbContext.UpdateAsync(employee, cancellationToken);
    //         await _dbContext.AddAsync(transferRecord, cancellationToken);
    //
    //         // Değişiklikleri kaydet
    //         await _dbContext.SaveChangesAsync(cancellationToken);
    //
    //         return new TransferResultDto
    //         {
    //             Success = true,
    //             EmployeeId = employeeId,
    //             SourceCompanyId = sourceCompanyId,
    //             TargetCompanyId = targetCompanyId,
    //             TransferRecordId = transferRecord.Id
    //         };
    //     }, cancellationToken);
    // }
    //
    // public async Task MergeCompaniesAsync(long targetCompanyId, List<long> companiesToMergeIds,
    //     CancellationToken cancellationToken = default)
    // {
    //     await _dbContext.ExecuteInTransactionAsync(async () =>
    //     {
    //         // Tüm gerekli şirketleri tek bir sorguda getir
    //         var allCompanyIds = new List<long>(companiesToMergeIds) { targetCompanyId };
    //         var companiesSpec = new CompaniesByIdsSpecification(allCompanyIds);
    //         var companies = await _dbContext.ListAsync(companiesSpec, cancellationToken);
    //
    //         // ID bazlı hızlı erişim için dictionary oluştur
    //         var companiesById = companies.ToDictionary(c => c.Id);
    //
    //         // Eksik olan şirketleri kontrol et
    //         var missingIds = allCompanyIds.Where(id => !companiesById.ContainsKey(id)).ToList();
    //         if (missingIds.Any())
    //         {
    //             throw new NotFoundException($"Companies with IDs {string.Join(", ", missingIds)} not found.");
    //         }
    //
    //         // Hedef şirketi al
    //         var targetCompany = companiesById[targetCompanyId];
    //
    //         // Birleştirilecek şirketlerdeki tüm çalışanları getir
    //         var employeesSpec = new EmployeesByCompanyIdsSpecification(companiesToMergeIds);
    //         var employees = await _dbContext.ListAsync(employeesSpec, cancellationToken);
    //
    //         // Çalışanları hedef şirkete taşı
    //         foreach (var employee in employees)
    //         {
    //             var oldCompanyId = employee.CompanyId;
    //             employee.CompanyId = targetCompanyId;
    //             employee.Company = targetCompany;
    //
    //             // Transfer kaydı oluştur
    //             var transferRecord = new EmployeeTransferRecord
    //             {
    //                 EmployeeId = employee.Id,
    //                 SourceCompanyId = oldCompanyId,
    //                 TargetCompanyId = targetCompanyId,
    //                 TransferDate = DateTime.UtcNow,
    //                 Notes = "Employee transferred due to company merger"
    //             };
    //
    //             await _dbContext.AddAsync(transferRecord, cancellationToken);
    //         }
    //
    //         // Çalışanları güncelle
    //         await _dbContext.UpdateRangeAsync(employees, cancellationToken);
    //
    //         // Birleştirilen şirketleri getir (targetCompany hariç)
    //         var companiesToMerge = companies.Where(c => c.Id != targetCompanyId).ToList();
    //
    //         // Şirketleri sil
    //         await _dbContext.RemoveRangeAsync(companiesToMerge, cancellationToken);
    //
    //         // Değişiklikleri kaydet
    //         await _dbContext.SaveChangesAsync(cancellationToken);
    //
    //         return true;
    //     }, cancellationToken);
    // }

    #endregion
}
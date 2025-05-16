using Queick.Company.Domain;

namespace Queick.Company.Application.Common.Specification;

/// <summary>
/// Şirket ID'sine göre specification
/// </summary>
public class CompanyByIdSpecification : BaseSpecification<CompanyDomain>
{
    public CompanyByIdSpecification(long id) : base(c => c.Id == id)
    {
    }
}
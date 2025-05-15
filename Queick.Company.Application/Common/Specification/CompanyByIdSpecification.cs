using Queick.Company.Domain;

namespace Queick.Company.Application.Common.Specification;

public class CompanyByIdSpecification : BaseSpecification<CompanyDomain>
{
    public CompanyByIdSpecification(long id) : base(c => c.Id == id)
    {
        // Gerekirse include'lar veya sıralama eklenebilir
        // AddInclude(c => c.Departments);
        // ApplyOrderBy(c => c.Name);
    }
}
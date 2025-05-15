using Queick.Company.Domain;

namespace Queick.Company.Application.Common.Specification;

public class CompanyPaginatedSpecification : BaseSpecification<CompanyDomain>
{
    public CompanyPaginatedSpecification(int skip, int take) : base()
    {
        ApplyPaging(skip, take);
        ApplyOrderBy(c => c.Name);
    }
}
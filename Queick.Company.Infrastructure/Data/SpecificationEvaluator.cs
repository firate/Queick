using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Repositories;
using Queick.Company.Domain;

namespace Queick.Company.Infrastructure.Data;

public class SpecificationEvaluator<T> where T : class, IEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;
            
        // Filtreleme
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }
            
        // Include'lar
        query = specification.Includes.Aggregate(query,
            (current, include) => current.Include(include));
            
        // String Include'lar
        query = specification.IncludeStrings.Aggregate(query,
            (current, include) => current.Include(include));
            
        // Sıralama
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }
            
        // Gruplama
        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }
            
        // Pagination
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
            
        return query;
    }
}
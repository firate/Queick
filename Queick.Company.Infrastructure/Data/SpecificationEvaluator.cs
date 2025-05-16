using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Common.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Infrastructure.Data;

/// <summary>
/// Specification'ları Entity Framework Core sorgularına dönüştüren sınıf
/// </summary>
public static class SpecificationEvaluator<T> where T : class, IEntity
{
    private const int DefaultPageSize = 25;
    private const int MaxPageSize = 100;

    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;

        // Filtering
        if (specification?.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Includes
        query = specification?.Includes.Aggregate(query,
            (current, include) => current.Include(include));

        // String includes
        query = specification?.IncludeStrings.Aggregate(query, (current, include) => current?.Include(include));

        // Sorting
        if (specification?.OrderBy != null)
        {
            query = query?.OrderBy(specification.OrderBy);
        }
        else if (specification?.OrderByDescending != null)
        {
            query = query?.OrderByDescending(specification.OrderByDescending);
        }

        // Grouping
        if (specification?.GroupBy != null)
        {
            query = query?.GroupBy(specification.GroupBy).SelectMany(x => x);
        }
        
        // Pagination
        if (specification?.IsPagingEnabled == true)
        {
            // page size check
            var safePageSize = specification?.Take switch
            {
                <= 0 => DefaultPageSize,
                > MaxPageSize => MaxPageSize,
                _ => specification?.Take ?? DefaultPageSize
            };

            var safeSkip = specification?.Skip switch
            {
                < 0 => 0,
                _ => specification?.Skip ?? 0
            };
            
            query = query?.Skip(safeSkip).Take(safePageSize);
        }
        else
        {
            query = query?.Take(MaxPageSize);
        }

        return query;
    }
}
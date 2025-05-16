using Microsoft.EntityFrameworkCore;
using Queick.Company.Application.Common.Interfaces;
using Queick.Company.Domain;

namespace Queick.Company.Infrastructure.Data;

public static class QueryCreator<T> where T : class, IEntity
{
    private const int DefaultPageSize = 25;
    private const int MaxPageSize = 100;

    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IQueryModel<T> queryModel)
    {
        var query = inputQuery;

        // Filtering
        if (queryModel?.Criteria != null)
        {
            query = query.Where(queryModel.Criteria);
        }

        // Includes
        query = queryModel?.Includes.Aggregate(query,
            (current, include) => current.Include(include));

        // String includes
        query = queryModel?.IncludeStrings.Aggregate(query, (current, include) => current?.Include(include));

        // Sorting
        if (queryModel?.OrderBy != null)
        {
            query = query?.OrderBy(queryModel.OrderBy);
        }
        else if (queryModel?.OrderByDescending != null)
        {
            query = query?.OrderByDescending(queryModel.OrderByDescending);
        }

        // Grouping
        if (queryModel?.GroupBy != null)
        {
            query = query?.GroupBy(queryModel.GroupBy).SelectMany(x => x);
        }
        
        // Pagination
        if (queryModel?.IsPagingEnabled == true)
        {
            // page size check
            var safePageSize = queryModel?.Take switch
            {
                <= 0 => DefaultPageSize,
                > MaxPageSize => MaxPageSize,
                _ => queryModel?.Take ?? DefaultPageSize
            };

            var safeSkip = queryModel?.Skip switch
            {
                < 0 => 0,
                _ => queryModel?.Skip ?? 0
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
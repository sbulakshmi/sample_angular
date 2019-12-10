using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace asp_dotnet_core_angular.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if(string.IsNullOrWhiteSpace(queryObj.SortBy)|| !columnsMap.ContainsKey(queryObj.SortBy))
            return query;

            if(queryObj.isSortAscen)
            return query.OrderBy(columnsMap[queryObj.SortBy]);
            else
            {
             return query.OrderByDescending(columnsMap[queryObj.SortBy]) ;   
            }
        }

        public static IQueryable<T> ApplyPaging<T> (this IQueryable<T> query, IQueryObject queryObj)
        {
            if(queryObj.PageNum<=0)
                queryObj.PageNum=1;
            if(queryObj.PageSize<=0)
                queryObj.PageSize=10;
            return query.Skip( (queryObj.PageNum-1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}
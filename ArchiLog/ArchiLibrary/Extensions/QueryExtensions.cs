using ArchiLibrary.Models;
using ArchiLibrary.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArchiLibrary.Extensions
{
    public static class QueryExtensions
    {

        private static Expression<Func<TModel, object>> SortingLambda<TModel> (string Champ)
        {
            string champ = Champ;

            //créer lambda
            var parameter = Expression.Parameter(typeof(TModel), "x");
            var property = Expression.Property(parameter, champ);

            var o = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<TModel, object>>(o, parameter);
            return lambda;
        } 

        public static IOrderedQueryable<TModel> Sort<TModel>(this IQueryable<TModel> query, BaseParams p)
        {
            if (!string.IsNullOrWhiteSpace(p.Asc) && !string.IsNullOrWhiteSpace(p.Dec))
            {
                var lambda = SortingLambda<TModel>(p.Asc);
                var lambda2 = SortingLambda<TModel>(p.Dec);

                return query.OrderBy(lambda).ThenByDescending(lambda2);
            }
            else if (!string.IsNullOrWhiteSpace(p.Asc))
            {
                var lambda = SortingLambda<TModel>(p.Asc);

                return query.OrderBy(lambda);
            }else if (!string.IsNullOrWhiteSpace(p.Dec))
            {
                var lambda = SortingLambda<TModel>(p.Dec);

                return query.OrderByDescending(lambda);
            }
            else
                return (IOrderedQueryable<TModel>)query;

        }
    }
}
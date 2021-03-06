using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if(spec.Criterias!=null)
            {
                query = query.Where(spec.Criterias);
            }
            query = spec.Includes.Aggregate(query,(current,include)=> current.Include(include));
            return query;
        }
    }
}
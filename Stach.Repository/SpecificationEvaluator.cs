using Microsoft.EntityFrameworkCore;
using Stach.Domain.Models;
using Stach.Domain.Specificaitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Repository
{
    public class SpecificationEvaluator<T> : BaseSpecifications<T> where T : Base
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec) 
        {
            var query = inputQuery; // _dbContext.Set<Product>()

            if (spec.Criteria is not null) // p => p.id == id
                query = query.Where(spec.Criteria);

            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if(spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            // _dbContext.Set<Product>().Where(p => p.id == id)
            // Includes
            // p => p.Brand
            // p => p.Category

            query = spec.Includes.Aggregate(query, (currentQuery, includeExperession) => currentQuery.Include(includeExperession));
            
            return query;
        }
    }
}

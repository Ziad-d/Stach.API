using Stach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Specificaitions
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : Base
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecifications()
        {
            
        }

        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}

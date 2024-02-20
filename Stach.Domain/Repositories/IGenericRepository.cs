using Stach.Domain.Models;
using Stach.Domain.Specificaitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Repositories
{
    public interface IGenericRepository<T> where T : Base
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
    }
}

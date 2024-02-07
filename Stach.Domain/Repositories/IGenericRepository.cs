using Stach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Repositories
{
    public interface IGenericRepository<T> where T : Base
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
    }
}

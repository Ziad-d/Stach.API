using Stach.Domain.Models;
using Stach.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> GetRepo<TEntity>() where TEntity : Base;

        Task<int> CompleteAsync();
    }
}

using Stach.Domain;
using Stach.Domain.Models;
using Stach.Domain.Repositories;
using Stach.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> GetRepo<TEntity>() where TEntity : Base
        {
            // 1. Getting key which is the name
            var key = typeof(TEntity).Name;

            // 2. Checking if this key already exists in the dictionary, if not
            if(!_repositories.ContainsKey(key))
            {
                // 2.1. Creating new GenericRepository of that entity
                var repository = new GenericRepository<TEntity>(_dbContext);
                // 2.2. Adding it to the dictionary with the key as its name
                _repositories.Add(key, repository);
            }

            // 3. Returning the key which is the name of the repository created
            return _repositories[key] as IGenericRepository<TEntity>;
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
    }
}

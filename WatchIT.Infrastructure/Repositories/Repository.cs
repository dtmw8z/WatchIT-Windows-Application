using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected WatchitDbContext _dbContext;

        public Repository(WatchitDbContext db)
        {
            _dbContext = db;
        }
        public async Task<T> CreateAsync(T e)
        {
            T r = _dbContext.Set<T>().Add(e).Entity;  // generic
            await _dbContext.SaveChangesAsync();

            return r;
        }


        public async Task<T> DeleteAsync(T e)
        {
            T res = _dbContext.Set<T>().Remove(e).Entity;
            await _dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public List<T> ReadAll(T e)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<T> UpdateAsync(T e)
        {
            T entity = _dbContext.Set<T>().Update(e).Entity;
            await _dbContext.SaveChangesAsync();
            return entity;
        }


        public async Task<T> UpsertAsync(T e)
        {
            T upsert = null;

            try
            {
                upsert = await UpdateAsync(e);
            }
            catch
            {
                upsert = await CreateAsync(e);
            }

            await _dbContext.SaveChangesAsync();
            return upsert;

        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WatchIT.Domain.SeedWork
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T e);
        List<T> ReadAll(T e);
        Task<List<T>> FindAllAsync();
        Task<T> UpsertAsync(T e);

        Task<T> FindByIdAsync(int id);
        Task<T> DeleteAsync(T e);
        Task<T> UpdateAsync(T e);



    }
}

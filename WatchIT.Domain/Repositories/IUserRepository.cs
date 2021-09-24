using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User> FindByEmailAsync(string email);
        Task RemoveUserAsync(User u);
    }
}

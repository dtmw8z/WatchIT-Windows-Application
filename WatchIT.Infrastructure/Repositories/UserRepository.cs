using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.Repositories;

namespace WatchIT.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
      
        public UserRepository(WatchitDbContext db) : base(db)
        {
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            User user = await _dbContext.Users.SingleOrDefaultAsync(em=>em.Email==email);
            return user;


        }

        public async Task RemoveUserAsync(User u)
        {
            _dbContext.Channel.Remove(u.Channel);
            _dbContext.Users.Remove(u);
            await _dbContext.SaveChangesAsync();

        }



    }
}

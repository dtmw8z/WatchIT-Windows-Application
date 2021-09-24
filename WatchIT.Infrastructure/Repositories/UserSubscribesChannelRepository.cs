using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.Repositories;

namespace WatchIT.Infrastructure.Repositories
{
    public class UserSubscribesChannelRepository : Repository<UserSubscribesChannel>, IUserSubscribesChannelRepository
    {

        public UserSubscribesChannelRepository(WatchitDbContext db) : base(db)
        {
        }

        public async Task<bool> HasSubscribe(int userid, int channelid)
        {
            bool hassubscribe = await _dbContext.UserSubscribesChannels.AnyAsync(em => em.userId == userid && em.channelId == channelid);

            return hassubscribe;
        }

        public async Task<UserSubscribesChannel> FindByUserChannelIdAsync(int userid, int channelid)
        {
            UserSubscribesChannel userSubscribesChannel = await _dbContext.UserSubscribesChannels.SingleOrDefaultAsync(em => em.userId == userid && em.channelId == channelid);

            return userSubscribesChannel;
        }

        public async Task<List<UserSubscribesChannel>> FindSubscribersByChannelAsync(int channelID)
        {
            List<UserSubscribesChannel> userSubscribesChannels = await _dbContext.UserSubscribesChannels.Where(x => x.channelId == channelID).ToListAsync();

            return userSubscribesChannels;
        }

    }
}

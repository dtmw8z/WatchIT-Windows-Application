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
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {

        public ChannelRepository(WatchitDbContext db):base(db)
        {
         
        }

        public async Task<bool> HasChannel(int userid)
        {
            bool hasChannel = await _dbContext.Channel.AnyAsync(em => em.userId == userid);

            return hasChannel;
        }

        public async Task<Channel> FindByUserIdAsync(int v)
        {
            Channel channel = await _dbContext.Channel.SingleAsync(c => c.userId == v);
            return channel;

        }

        public async Task RemoveChannelAsync(Channel channelID)
        {

            List<UserSubscribesChannel> userSubscribesChannels = await _dbContext.UserSubscribesChannels.Where(x => x.channelId == channelID.Id).ToListAsync();
            _dbContext.UserSubscribesChannels.RemoveRange(userSubscribesChannels);
            

            _dbContext.channels.Remove(channelID);
            await _dbContext.SaveChangesAsync();
        }



    }
}

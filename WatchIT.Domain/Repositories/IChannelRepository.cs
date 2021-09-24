using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IChannelRepository : IRepository<Channel>
    {
        public Task<bool> HasChannel(int userid);
        Task<Channel> FindByUserIdAsync(int v);
        Task RemoveChannelAsync(Channel channelID);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IUserSubscribesChannelRepository:IRepository<UserSubscribesChannel>
    {
        public Task<bool> HasSubscribe(int userid, int channelid);
        public Task<UserSubscribesChannel> FindByUserChannelIdAsync(int userid, int channelid);
        Task<List<UserSubscribesChannel>> FindSubscribersByChannelAsync(int channelID);
    }
}

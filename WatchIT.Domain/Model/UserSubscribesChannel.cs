using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class UserSubscribesChannel
    {
        public int userId { get; set; }
        public User User { get; set; }
        public int channelId { get; set; }
        public Channel Channel { get; set; }
        public DateTime subscribedOn { get; set; }
        public UserSubscribesChannel()
        {
            this.subscribedOn = DateTime.UtcNow;
           
        }
    }
}

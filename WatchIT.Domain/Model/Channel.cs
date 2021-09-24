using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class Channel:Entity
    {
        public string channelName { get; set; }
        public string channelDescription { get; set; }        
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public List<UserSubscribesChannel> UserSubscribesChannels { get; set; }
        public List<Video> Videos { get; set; }
        public List<Music> Musics { get; set; }
        public int userId { get; set; }
        public User User { get; set; }

        public Channel()
        {
            this.created_at = DateTime.UtcNow;
            this.updated_at = DateTime.UtcNow;
        }
    }
}

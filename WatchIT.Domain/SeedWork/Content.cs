using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.Model;

namespace WatchIT.Domain.SeedWork
{
    public abstract class Content : Entity
    {
        public byte[] content { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Thumbnail { get; set; }
        public string Type { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int channelId { get; set; }
        public Channel Channel { get; set; }

        public Content()
        {
            this.created_at = DateTime.UtcNow;
            this.updated_at = DateTime.UtcNow;
        }

    }
}

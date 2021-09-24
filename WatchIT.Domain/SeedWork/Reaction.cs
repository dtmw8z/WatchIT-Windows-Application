using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.Model;

namespace WatchIT.Domain.SeedWork
{
    public abstract class Reaction:Entity
    {
        public DateTime created_at { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
        public Reaction()
        {
            this.created_at = DateTime.UtcNow;
        }

    }
}

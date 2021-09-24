using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class MusicComment: Reaction
    {
        public string commentText { get; set; }
        public DateTime updated_at { get; set; }


        public int musicId { get; set; }
        public Music Music { get; set; }


        public MusicComment()
        {
            this.created_at = DateTime.UtcNow;
            this.updated_at = DateTime.UtcNow;
        }

    
    }
}

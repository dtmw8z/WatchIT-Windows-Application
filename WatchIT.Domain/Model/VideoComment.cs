using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class VideoComment : Reaction
    {
        public string commentText { get; set; }
        public DateTime updated_at { get; set; }
        public int videoId { get; set; }
        public Video Video { get; set; }

        public VideoComment()
        {
            this.created_at = DateTime.UtcNow;
            this.updated_at = DateTime.UtcNow;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class VideoLike : Reaction
    {

        
        public int videoId { get; set; }
        public Video Video { get; set; }
    }
}

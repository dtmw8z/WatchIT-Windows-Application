using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class Video:Content
    {        
        public List<VideoPlaylist> VideoPlaylists { get; set; }
        public List<VideoLike> VideoLikes { get; set; }
        public List<VideoComment> VideoComments { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class Music:Content
    {
        public List<MusicPlaylist> MusicPlaylists { get; set; }
        public List<MusicLike> MusicLikes { get; set; }
        public List<MusicComment> MusicComments { get; set; }
    }
}

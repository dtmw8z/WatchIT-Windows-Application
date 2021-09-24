using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class VideoPlaylist
    {
        public int videoId { get; set; }
        public Video Video { get; set; }
        public int playlistId { get; set; }
        public Playlist Playlist { get; set; }
        public DateTime updated_at { get; set; }
        public VideoPlaylist()
        {
            this.updated_at = DateTime.UtcNow;
        }
    }
}

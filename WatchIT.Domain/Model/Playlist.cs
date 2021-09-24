using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class Playlist:Entity
    {
        public int userId { get; set; }
        public string playlistName { get; set; }
        public DateTime created_on { get; set; }
        public User User { get; set; }
        public List<MusicPlaylist> MusicPlaylists { get; set; }
        public List<VideoPlaylist> VideoPlaylists { get; set; }

        public Playlist()
        {
            this.created_on = DateTime.UtcNow;
        }
    }
}

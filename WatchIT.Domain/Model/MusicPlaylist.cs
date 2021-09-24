using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class MusicPlaylist
    {
        public int musicId { get; set; }
        public Music Music { get; set; }

        public int playlistId { get; set; }
        public Playlist Playlist { get; set; }
        public DateTime updated_at { get; set; }
        public MusicPlaylist()
        {
            this.updated_at = DateTime.UtcNow;

        }
    }
}

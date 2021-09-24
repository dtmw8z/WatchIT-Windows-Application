using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class User: Entity
    {
        public string fullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public List<UserSubscribesChannel> UserSubscribesChannels { get; set; }
        public List<MusicLike> MusicLikes { get; set; }
        public List<VideoLike> VideoLikes { get; set; }
        public List<MusicComment> MusicComments { get; set; }
        public List<VideoComment> VideoComments { get; set; }
        public List<Playlist> Playlists { get; set; }
        public Channel Channel { get; set; }
    }
}

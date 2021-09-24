using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IMusicRepository : IRepository<Music>
    {
        Task<List<Music>> FindMusicsInPlaylistAsync(int playlistID);
        Task<List<Music>> FindMusicByChannelAsync(int channelID);
        Task RemoveMusicAsync(Music m);
    }
}

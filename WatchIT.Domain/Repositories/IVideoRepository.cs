using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IVideoRepository : IRepository<Video>
    {
        Task<List<Video>> FindVideosInPlaylistAsync(int playlistID);
        Task<List<Video>> FindVideoByChannelAsync(int channelID);
        Task RemoveVideoAsync(Video m);
    }
}

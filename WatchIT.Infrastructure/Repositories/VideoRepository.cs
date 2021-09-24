using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.Repositories;

namespace WatchIT.Infrastructure.Repositories
{
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        public VideoRepository(WatchitDbContext db) : base(db)
        {

        }

        public async Task<List<Video>> FindVideosInPlaylistAsync(int playlistID)
        {
            List<VideoPlaylist> VideoPlaylists = await _dbContext.VideoPlaylists.Where(x => x.playlistId == playlistID).ToListAsync();
            List<Video> Videos = new List<Video>();
            foreach (VideoPlaylist f in VideoPlaylists)
            {
                Video Video = await _dbContext.Videos.SingleOrDefaultAsync(x => x.Id == f.videoId);
                Videos.Add(Video);
            }

            return Videos;

        }

        public async Task<List<Video>> FindVideoByChannelAsync(int channelID)
        {
            List<Video> Videos = await _dbContext.Videos.Where(x => x.channelId == channelID).ToListAsync();

            return Videos;
        }

        public async Task RemoveVideoAsync(Video m)
        {
            List<VideoLike> ml = await _dbContext.VideoLikes.Where(x => x.videoId == m.Id).ToListAsync();
            List<VideoComment> mc = await _dbContext.VideoComments.Where(x => x.videoId == m.Id).ToListAsync();
            //List<VideoPlaylist> mp = await _dbContext.VideoPlaylists.Where(x => x.videoId == m.Id).ToListAsync();

            _dbContext.VideoLikes.RemoveRange(ml);
            _dbContext.VideoComments.RemoveRange(mc);
            //_dbContext.VideoPlaylists.RemoveRange(mp);
            _dbContext.Videos.Remove(m);
            await _dbContext.SaveChangesAsync();




        }
    }
}

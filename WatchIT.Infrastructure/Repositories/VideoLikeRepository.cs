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
   
    public class VideoLikeRepository : Repository<VideoLike>, IVideoLikeRepository
    {
        public VideoLikeRepository(WatchitDbContext db) : base(db)
        {

        }
         public async Task<bool> HasLike(int userid, int videoid)
        {
            bool haslike = await _dbContext.VideoLikes.AnyAsync(em => em.userId == userid && em.videoId == videoid);

            return haslike;
        }

        public async Task<VideoLike> FindByVideoIdAsync(int userid, int videoid)
        {
            VideoLike videoLike = await _dbContext.VideoLikes.SingleOrDefaultAsync(em => em.userId == userid && em.videoId == videoid);

            return videoLike;
        }

        public async Task<List<VideoLike>> FindVideoLikeAsync(int videoID)
        {
            List<VideoLike> videoLikes = await _dbContext.VideoLikes.Where(x => x.videoId == videoID).ToListAsync();
            return videoLikes;
        }
    }
}

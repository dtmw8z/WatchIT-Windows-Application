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
    public class VideoCommentRepository : Repository<VideoComment>, IVideoCommentRepository
    {
        public VideoCommentRepository(WatchitDbContext db) : base(db)
        {

        }

        public async Task<List<VideoComment>> FindVideoCommentAsync(int videoID)
        {
            List<VideoComment> videocomments = await _dbContext.VideoComments.Where(x => x.videoId == videoID).ToListAsync();

            return videocomments;
        }

        public async Task<bool> HasCommented(int userid, int videoid)
        {
            bool hascomment = await _dbContext.VideoComments.AnyAsync(em => em.userId == userid && em.videoId == videoid);

            return hascomment;
        }

    }
}

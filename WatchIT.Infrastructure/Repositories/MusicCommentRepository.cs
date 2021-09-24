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
    public class MusicCommentRepository : Repository<MusicComment>, IMusicCommentRepository
    {
        public MusicCommentRepository(WatchitDbContext db) : base(db)
        {

        }
        public async Task<List<MusicComment>> FindMusicCommentAsync(int musicaID)
        {
            List<MusicComment> musiccomments = await _dbContext.MusicComments.Where(x => x.musicId == musicaID).ToListAsync();

            return musiccomments;
        }

        public async Task<bool> HasCommented(int userid, int musicid)
        {
            bool hascomment = await _dbContext.MusicComments.AnyAsync(em => em.userId == userid && em.musicId == musicid);

            return hascomment;
        }

    }

}

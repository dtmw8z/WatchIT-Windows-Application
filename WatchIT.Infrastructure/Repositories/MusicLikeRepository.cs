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
   
    public class MusicLikeRepository : Repository<MusicLike>, IMusicLikeRepository
    {
        public MusicLikeRepository(WatchitDbContext db) : base(db)
        {
           
        }

        public async Task<bool> HasLike(int userid, int musicid)
        {
            bool haslike = await _dbContext.MusicLikes.AnyAsync(em => em.userId == userid && em.musicId == musicid);

            return haslike;
        }
        public async Task<MusicLike> FindByMusicIdAsync(int userid, int musicid)
        {
            MusicLike musicLike = await _dbContext.MusicLikes.SingleOrDefaultAsync(em => em.userId == userid && em.musicId == musicid);

            return musicLike;
        }

        public async Task<List<MusicLike>> FindMusicLikeAsync(int musicaID)
        {
            List<MusicLike> musicLikes = await _dbContext.MusicLikes.Where(x => x.musicId == musicaID).ToListAsync();
            return musicLikes;
        }
       
    }
}

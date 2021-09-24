using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IMusicLikeRepository : IRepository<MusicLike>
    {
        public Task<bool> HasLike(int userid, int musicid);
        public Task<MusicLike> FindByMusicIdAsync(int userid, int musicid);
        Task<List<MusicLike>> FindMusicLikeAsync( int musicaID);
    }

}

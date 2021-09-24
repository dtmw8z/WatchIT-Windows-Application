using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IVideoLikeRepository : IRepository<VideoLike>
    {
        public Task<bool> HasLike(int userid, int videoid);
        public Task<VideoLike> FindByVideoIdAsync(int userid, int videoid);
        Task<List<VideoLike>> FindVideoLikeAsync(int  videoID);
    }
}

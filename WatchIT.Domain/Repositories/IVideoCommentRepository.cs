using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IVideoCommentRepository : IRepository<VideoComment>
    {
        Task<List<VideoComment>> FindVideoCommentAsync(int videoID);
        Task<bool> HasCommented(int userid, int videoid);
    }
    
}

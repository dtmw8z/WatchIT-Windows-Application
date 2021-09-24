using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IMusicCommentRepository : IRepository<MusicComment>
    {
        Task<List<MusicComment>> FindMusicCommentAsync(int musicaID);
        Task<bool> HasCommented(int userid, int musicid);
    }

}

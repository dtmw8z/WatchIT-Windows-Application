using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Repositories
{
    public interface IPlaylistRepository:IRepository<Playlist>
    {
        Task<List<Playlist>> FindPlaylistAsync(int usrID);
    }
}

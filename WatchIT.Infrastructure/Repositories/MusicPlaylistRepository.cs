using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.Model;
using WatchIT.Domain.Repositories;

namespace WatchIT.Infrastructure.Repositories
{
    public class MusicPlaylistRepository : Repository<MusicPlaylist>, IMusicPlaylistRepository
    {
        public MusicPlaylistRepository(WatchitDbContext db) : base(db)
        {

        }
    }
}

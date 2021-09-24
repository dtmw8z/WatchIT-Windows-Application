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
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(WatchitDbContext db) : base(db)
        {
            

        }
        public async Task<List<Playlist>> FindPlaylistAsync(int usrID)
        {
            List<Playlist> playlists = await _dbContext.Playlists.Where(x => x.userId == usrID).ToListAsync();

            return playlists;
        }
    }
}

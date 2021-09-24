using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.Model;
using WatchIT.Domain.Repositories;

namespace WatchIT.Infrastructure.Repositories
{
    public class VideoPlaylistRepository : Repository<VideoPlaylist>, IVideoPlaylistRepository
    {

       public VideoPlaylistRepository(WatchitDbContext db) : base(db)
       {

       }
    }
}

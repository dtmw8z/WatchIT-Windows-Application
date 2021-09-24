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
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        public MusicRepository(WatchitDbContext db) : base(db)
        {

        }

        public async Task<List<Music>> FindMusicsInPlaylistAsync(int playlistID)
        {
            List<MusicPlaylist> musicPlaylists = await _dbContext.MusicPlaylists.Where(x => x.playlistId == playlistID).ToListAsync();
            List<Music> musics = new List<Music>();
            foreach (MusicPlaylist f in musicPlaylists)
            {
                Music music = await _dbContext.Musics.SingleOrDefaultAsync(x => x.Id == f.musicId);
                musics.Add(music);
            }

            return musics;

        }

        public async Task<List<Music>> FindMusicByChannelAsync(int channelID)
        {
            List<Music> musics = await _dbContext.Musics.Where(x => x.channelId == channelID).ToListAsync();

            return musics;
        }

        public async Task RemoveMusicAsync(Music m)
        {
            List<MusicLike> ml = await _dbContext.MusicLikes.Where(x => x.musicId == m.Id).ToListAsync();
            List<MusicComment> mc = await _dbContext.MusicComments.Where(x => x.musicId == m.Id).ToListAsync();
            List<MusicPlaylist> mp = await _dbContext.MusicPlaylists.Where(x => x.musicId == m.Id).ToListAsync();
       
            _dbContext.MusicLikes.RemoveRange(ml);
            _dbContext.MusicComments.RemoveRange(mc);
            _dbContext.MusicPlaylists.RemoveRange(mp);


            _dbContext.Musics.Remove(m);
            await _dbContext.SaveChangesAsync();




        }
    }
}

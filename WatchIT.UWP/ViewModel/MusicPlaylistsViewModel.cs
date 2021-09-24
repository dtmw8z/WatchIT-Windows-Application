using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel
{
    public class MusicPlaylistsViewModel
    {
        public MusicPlaylist musicPlaylist { get; set; }
        public ObservableCollection<MusicPlaylist> musicPlaylists { get; set; }

        public MusicPlaylistsViewModel() {
            musicPlaylist = new MusicPlaylist();

        }

        internal async Task UpsertAsync(MusicPlaylist mp)
        {
            mp.Music = await App.UnitOfWork.MusicRepository.FindByIdAsync(mp.musicId);
            mp.Playlist = await App.UnitOfWork.PlaylistRepository.FindByIdAsync(mp.playlistId);

            await App.UnitOfWork.MusicPlaylistRepository.UpsertAsync(mp);
        }


        internal async Task DeleteAsync()
        {
            await App.UnitOfWork.MusicPlaylistRepository.DeleteAsync(musicPlaylist);

        }
    }
}

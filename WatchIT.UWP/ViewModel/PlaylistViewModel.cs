using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel
{
    public class PlaylistViewModel
    {
        public ObservableCollection<Playlist> Playlists { get; set; }

        public Playlist Playlist;

        public PlaylistViewModel()
        {
            Playlists = new ObservableCollection<Playlist>();
            Playlist = new Playlist();
        }

        public async Task LoadAllAsync()
        {

            List<Playlist> list = await App.UnitOfWork.PlaylistRepository.FindAllAsync();
            // Playlists = new ObservableCollection<Playlist>(list);
            Playlists.Clear();
            foreach (Playlist e in list)
            {
                Playlists.Add(e);
            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.PlaylistRepository.CreateAsync(Playlist);
        }

        public async Task LoadCurrentUserPlaylistAsync()
        {
            List<Playlist> list = await App.UnitOfWork.PlaylistRepository.FindPlaylistAsync(App.userViewModel.GetCurrentUserID());
            Playlists.Clear();
            foreach (Playlist e in list)
            {
                e.User = await App.UnitOfWork.UserRepository.FindByIdAsync(e.userId);
                Playlists.Add(e);

            }


        }

        internal async Task DeleteAsync(Playlist Playlist)
        {
            await App.UnitOfWork.PlaylistRepository.DeleteAsync(Playlist);
            Playlists.Remove(Playlist);
        }

        internal async Task UpsertAsync(Playlist pl)
        {            
          await App.UnitOfWork.PlaylistRepository.UpsertAsync(pl);
        }
    }
}

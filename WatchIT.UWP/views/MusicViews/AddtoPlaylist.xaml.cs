using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.MusicViews
{
    public sealed partial class AddtoPlaylist : ContentDialog
    {
        public PlaylistViewModel PlaylistViewModel { get; set; }
        public MusicViewModel musicViewModel { get; set; }
        public UserViewModel userViewModel { get; set; }
        public MusicPlaylistsViewModel musicPlaylistsViewModel { get; set; }
        public AddtoPlaylist(MusicViewModel musicVM, User usr)
        {
            this.InitializeComponent();
            PlaylistViewModel = new PlaylistViewModel();
            userViewModel = new UserViewModel();
            musicViewModel = musicVM;

            musicPlaylistsViewModel = new MusicPlaylistsViewModel();

            
            userViewModel.User = usr;

        }   



        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int playlistID = (loadPlaylist.SelectedItem as Playlist).Id;
            int musicID = musicViewModel.Music.Id;

            musicPlaylistsViewModel.musicPlaylist.musicId = musicID;
            musicPlaylistsViewModel.musicPlaylist.playlistId = playlistID;

            await musicPlaylistsViewModel.UpsertAsync(musicPlaylistsViewModel.musicPlaylist);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }
    }
}

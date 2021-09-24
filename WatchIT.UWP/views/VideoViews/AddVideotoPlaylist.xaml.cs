using System;
using System.Collections.Generic;
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

namespace WatchIT.UWP.views.VideoViews
{
    public sealed partial class AddVideotoPlaylist : ContentDialog
    {
        public PlaylistViewModel PlaylistViewModel { get; set; }
        public VideoViewModel videoViewModel { get; set; }
        public UserViewModel userViewModel { get; set; }
        public VideoPlaylistsViewModel videoPlaylistsViewModel { get; set; }
        public AddVideotoPlaylist(VideoViewModel videoVM, User usr)
        {
            this.InitializeComponent();
            PlaylistViewModel = new PlaylistViewModel();
            userViewModel = new UserViewModel();
            
            videoViewModel = videoVM;

            videoPlaylistsViewModel = new VideoPlaylistsViewModel();

            userViewModel.User = usr;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int playlistID = (loadPlaylist.SelectedItem as Playlist).Id;
            int videoID = videoViewModel.Video.Id;

            videoPlaylistsViewModel.videoPlaylist.videoId = videoID;
            videoPlaylistsViewModel.videoPlaylist.playlistId = playlistID;

            await videoPlaylistsViewModel.UpsertAsync(videoPlaylistsViewModel.videoPlaylist);

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}

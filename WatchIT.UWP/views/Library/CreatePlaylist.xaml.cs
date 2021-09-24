using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.Home;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.Library
{
    
    public sealed partial class CreatePlaylist : ContentDialog
    {
        PlaylistViewModel PlaylistViewModel { get; set; }
        public CreatePlaylist(Playlist playlist)
        {
            this.InitializeComponent();
            PlaylistViewModel = new PlaylistViewModel();
            PlaylistViewModel.Playlist = playlist;
            if (playlist.playlistName != null)
            {
                createPlaylist.Text = playlist.playlistName;
            }
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            PlaylistViewModel.Playlist.playlistName = createPlaylist.Text;
            PlaylistViewModel.Playlist.userId = App.userViewModel.loggedinUser.Id;
            await PlaylistViewModel.UpsertAsync(PlaylistViewModel.Playlist);

            var checkboxDialog = new MessageDialog("Playlist " + createPlaylist.Text + " Created/Updated Successfully");
            await checkboxDialog.ShowAsync();
            

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }
    }
}

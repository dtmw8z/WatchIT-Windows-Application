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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.Library
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlaylistPage : Page
    {  
       public PlaylistViewModel PlaylistViewModel { get; set; }
        public PlaylistPage()
        {
            this.InitializeComponent();
            PlaylistViewModel = new PlaylistViewModel();
        }

        private async void createplaylist_Click(object sender, RoutedEventArgs e)
        {
            CreatePlaylist dlg = new CreatePlaylist(new Playlist());
            var res = await dlg.ShowAsync();

            this.Frame.Navigate(typeof(PlaylistPage));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await PlaylistViewModel.LoadCurrentUserPlaylistAsync();
            base.OnNavigatedTo(e);
        }

        private void Playlist_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Playlist playlist)
            {

                PlaylistViewModel.Playlist = playlist;
                this.Frame.Navigate(typeof(PlaylistSinglePage), PlaylistViewModel);

            }

        }
    }
}

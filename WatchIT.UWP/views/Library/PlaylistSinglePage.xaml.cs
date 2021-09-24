using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.Domain.SeedWork;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.MusicViews;
using WatchIT.UWP.views.VideoViews;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.Library
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlaylistSinglePage : Page
    {
        public PlaylistViewModel PlaylistViewModel { get; set; }
        public UserViewModel User { get; set; }
        public VideoViewModel VideoViewModel { get; set; }
        public MusicViewModel MusicViewModel { get; set; }
       public MusicPlaylistsViewModel musicPlaylistsViewModel { get; set; }
        public VideoPlaylistsViewModel videoPlaylistsViewModel { get; set; }

        public ObservableCollection<Content> contents { get; set; }

        public PlaylistSinglePage()
        {
            this.InitializeComponent();
            PlaylistViewModel = new PlaylistViewModel();
            User = new UserViewModel();
            VideoViewModel = new VideoViewModel();
            MusicViewModel = new MusicViewModel();
            musicPlaylistsViewModel = new MusicPlaylistsViewModel();
            videoPlaylistsViewModel = new VideoPlaylistsViewModel();
            contents = new ObservableCollection<Content>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                PlaylistViewModel = e.Parameter as PlaylistViewModel;
            }
            await VideoViewModel.LoadVideoinPlaylistAsync(PlaylistViewModel.Playlist.Id);
            await MusicViewModel.LoadMusicinPlaylistAsync(PlaylistViewModel.Playlist.Id);

            contents.Clear();
            foreach(Music m in MusicViewModel.Musics)
            {
                contents.Add(m as Content);
            }
            foreach (Video v in VideoViewModel.Videos)
            {
                contents.Add(v as Content);
            }
            base.OnNavigatedTo(e);
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            CreatePlaylist cp = new CreatePlaylist(PlaylistViewModel.Playlist);
            await cp.ShowAsync();
            this.Frame.Navigate(typeof(PlaylistPage));

        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog
            {
                Title = "Delete this Playlist?",
                Content = "Are you sure you want to delete the playlist ?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await cd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {                   
                    await PlaylistViewModel.DeleteAsync(PlaylistViewModel.Playlist);
                    this.Frame.Navigate(typeof(PlaylistPage), PlaylistViewModel);
            }
        }

       

        public static ImageSource LoadThumbnailAsync(byte[] thumb)
        {
            using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(thumb);
                    writer.StoreAsync().GetResults();
                }

                var image = new BitmapImage();
                image.SetSource(ms);
                ms.Dispose();
                return image;
            }

        }

        private async void removeContent_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement b && b.DataContext is Content content)
            {
                if (content.Type == "video")
                {
                    videoPlaylistsViewModel.videoPlaylist.playlistId = PlaylistViewModel.Playlist.Id;
                    videoPlaylistsViewModel.videoPlaylist.videoId = (content as Video).Id;
                    await videoPlaylistsViewModel.DeleteAsync();

                }
                else
                {
                    musicPlaylistsViewModel.musicPlaylist.playlistId = PlaylistViewModel.Playlist.Id;            
                    musicPlaylistsViewModel.musicPlaylist.musicId = (content as Music).Id ;
                    await musicPlaylistsViewModel.DeleteAsync();

                }
               
                this.Frame.Navigate(typeof(PlaylistSinglePage), PlaylistViewModel);
            }

        }

        public static Symbol videoORmusic(string type)
        {
            if (type == "video")
            {
                return Symbol.Video;
            }
            else
            {
                return Symbol.MusicInfo;
            }
        }

        private void playlistList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem is Content content)
            {
                if(content.Type == "video")
                {
                    this.Frame.Navigate(typeof(VideoSingle), content as Video);
                }
                else
                {
                    this.Frame.Navigate(typeof(MusicSinglePage), content as Music);

                }
            }
        }
    }
}

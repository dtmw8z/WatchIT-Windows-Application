using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.MusicViews;
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

namespace WatchIT.UWP.views.MyChannel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MyMusic : Page
    {
        public MusicViewModel musicViewModel { get; set; }
        public ChannelViewModel channelViewModel { get; set; }
        public MyMusic()
        {
            this.InitializeComponent();
            musicViewModel = new MusicViewModel();
            channelViewModel = new ChannelViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter != null)
            {
                channelViewModel = e.Parameter as ChannelViewModel;
            }

            base.OnNavigatedTo(e);
        }
       
        private void navigate_to_singlepage(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Music music)
            {
                musicViewModel.Music = music;
                this.Frame.Navigate(typeof(MusicSinglePage), musicViewModel.Music);
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

        private void editMusic_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement b && b.DataContext is Music music)
            {
                this.Frame.Navigate(typeof(UploadMusicPage), music);
            }

        }

        private async void deleteMusic_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog
            {
                Title = "Delete Your Music?",
                Content = "Are you sure you want to remove the music from your channel ?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await cd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement b && b.DataContext is Music music)
                {
                    musicViewModel.Music = music;
                    await musicViewModel.DeleteAsync(music);
                    this.Frame.Navigate(typeof(MyChannelView),channelViewModel.Channel);
                }
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.MusicViews;
using WatchIT.UWP.views.VideoViews;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.Home
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public VideoViewModel VideoViewModel { get; set; }
        public MusicViewModel MusicViewModel { get; set; }
        public HomePage()
        {
            this.InitializeComponent();


            VideoViewModel = new VideoViewModel();
            MusicViewModel = new MusicViewModel();
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;

            await MusicViewModel.LoadAllAsync();
            await VideoViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;

        }
        private void navigate_to_Musicsinglepage(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Music music)
            {
                MusicViewModel.Music = music;
                this.Frame.Navigate(typeof(MusicSinglePage), MusicViewModel.Music);
            }




        }
        //private async void navigate_to_singlepage(object sender, TappedRoutedEventArgs e)
        //{
        //    if ( e.Tapped is Music music)
        //    {
        //       MusicViewModel.Music = music;
        //        this.Frame.Navigate(typeof(MusicSinglePage), MusicViewModel.Music);
        //    }

        //}
        private void navigate_to_Videosinglepage(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Video video)
            {
                VideoViewModel.Video = video;
                this.Frame.Navigate(typeof(VideoSingle), VideoViewModel.Video);
            }




        }



    }
        
}

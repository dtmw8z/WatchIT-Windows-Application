using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
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

namespace WatchIT.UWP.views.MusicViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicPage : Page
    {


        public MusicViewModel MusicViewModel { get; set; }
        

        public MusicPage()
        {
            this.InitializeComponent();
            MusicViewModel = new MusicViewModel();
            
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;
            await MusicViewModel.LoadAllAsync();           
            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;
        }
        //private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(MusicSinglePage), MusicViewModel);
        //}
        private void navigate_to_singlepage(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Music music)
            {
                MusicViewModel.Music = music;
                this.Frame.Navigate(typeof(MusicSinglePage), MusicViewModel.Music);
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
    }
}

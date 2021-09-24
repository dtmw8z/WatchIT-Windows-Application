using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.MyChannel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicUploadPage : Page
    {
        public MusicViewModel musica { get; set; }
        public ChannelViewModel ChannelViewModel { get; set; }
        public ContentDialog result { get; set; }


        public MusicUploadPage()
        {
            this.InitializeComponent();
            musica = new MusicViewModel();
            ChannelViewModel = new ChannelViewModel();
            result = new ContentDialog();
            result.Title = "Music Uploaded Successfully";
            result.Content = "New Music Uploaded Successfully. You can edit, delete your music in your channel page.";
            result.PrimaryButtonText = "Ok";

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                musica.Music = e.Parameter as Music;
                title.Text = musica.Music.Title;
                description.Text = musica.Music.Description;
                uploadThumbButton.Visibility = Visibility.Collapsed;
                uploadButton.Visibility = Visibility.Collapsed;
                clickTextThumb.Visibility = Visibility.Collapsed;
                clickTextVideo.Visibility = Visibility.Collapsed;

                result.Title = "Updated Successfully";
                result.Content = "Music Updated Successfully. Thank you.";

            }

            base.OnNavigatedTo(e);
        }



        private async void uploadThumbButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {

                text.Text = "Picked photo: " + file.Name;
            }
            musica.Music.Thumbnail = await musica.ConvertImageToByte(file);

        }

        private async void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();

            picker.FileTypeFilter.Add(".mp3");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                this.textBlock.Text = "Picked music: " + file.Name;
            }
            musica.Music.content = await musica.GetBytesAsync(file);
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            musica.Music.Title = title.Text;
            musica.Music.Description = description.Text;
            ChannelViewModel.Channel = await App.UnitOfWork.ChannelRepository.FindByUserIdAsync(App.userViewModel.GetCurrentUserID());
            musica.Music.Type = "music";
            musica.Music.channelId = ChannelViewModel.Channel.Id;
            await musica.UpsertAsync(musica.Music);
            await result.ShowAsync();

            this.Frame.Navigate(typeof(MyMusic));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();

        }
    }
}

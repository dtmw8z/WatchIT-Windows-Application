using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.UWP.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Storage.Provider;
using WatchIT.Domain.Model;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.MyChannel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UploadMusicPage : Page
    {
        public MusicViewModel musica{ get; set; }
        public ChannelViewModel ChannelViewModel { get; set; }
        public ContentDialog result { get; set; }


        public UploadMusicPage()
        {
            this.InitializeComponent();
            musica = new MusicViewModel();
            ChannelViewModel = new ChannelViewModel();
            result = new ContentDialog();
            result.Title = "Music Uploaded Successfully";
            result.Content = "New Music Uploaded Successfully. You can edit, delete your music in your channel page.";
            result.PrimaryButtonText = "Ok";
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                musica.Music = e.Parameter as Music;
                title.Text = musica.Music.Title;
                description.Text = musica.Music.Description;
                uploadThumbButton.Visibility = Visibility.Collapsed;
                uploadButton.Visibility = Visibility.Collapsed;
                clickTextThumb.Visibility = Visibility.Collapsed;
                clickTextMusic.Visibility = Visibility.Collapsed;

                result.Title = "Updated Successfully";
                result.Content = "Music Updated Successfully. Thank you.";

            }

            ChannelViewModel.Channel = await App.UnitOfWork.ChannelRepository.FindByUserIdAsync(App.userViewModel.GetCurrentUserID());
            ChannelViewModel.Channel.UserSubscribesChannels = await App.UnitOfWork.UserSubscribesChannelRepository.FindSubscribersByChannelAsync(ChannelViewModel.Channel.Id);

            ChannelViewModel.LoadMyContentsAsync(ChannelViewModel.Channel.Id);
            ChannelViewModel.LoadMyVideoContentsAsync(ChannelViewModel.Channel.Id);

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
            else
            {
                MessageDialog dialog = new MessageDialog("Thumbnail cannot be empty");
                await dialog.ShowAsync();
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
                var prop = await file.GetBasicPropertiesAsync();
                if (prop.Size > 5000000)
                {
                    MessageDialog dialog = new MessageDialog("Music Size cannot be greater than 5 MB");
                    await dialog.ShowAsync();
                }
                else
                {
                    this.textBlock.Text = "Picked music: " + file.Name;
                }
                
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Music cannot be empty");
                await dialog.ShowAsync();
            }
            musica.Music.content = await musica.GetBytesAsync(file);
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            bool iscorrect = true;

            if (string.IsNullOrWhiteSpace(title.Text))
                {
                iscorrect = false;
                _TitleCheckError.Text = "Title is required*";
            }
            if (string.IsNullOrWhiteSpace(description.Text))
            {
                iscorrect = false;
                _DescriptionCheckError.Text = "Description is required*";
            }
           
            if(iscorrect)
            {
                musica.Music.Title = title.Text;
                musica.Music.Description = description.Text;
                ChannelViewModel.Channel = await App.UnitOfWork.ChannelRepository.FindByUserIdAsync(App.userViewModel.GetCurrentUserID());
                musica.Music.Type = "music";
                musica.Music.channelId = ChannelViewModel.Channel.Id;

                await musica.UpsertAsync(musica.Music);
                await result.ShowAsync();

                this.Frame.Navigate(typeof(MyChannelView), ChannelViewModel);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();

        }
    }
}

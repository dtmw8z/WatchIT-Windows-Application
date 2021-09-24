using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.Profile;
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

namespace WatchIT.UWP.views.MyChannel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyChannelView : Page
    {
        public ChannelViewModel channelViewModel { get; set; }
        
        public MyChannelView()
        {
            this.InitializeComponent();
            channelViewModel = new ChannelViewModel();
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;

            channelViewModel.Channel = await App.UnitOfWork.ChannelRepository.FindByUserIdAsync(App.userViewModel.GetCurrentUserID());
            channelViewModel.Channel.UserSubscribesChannels = await App.UnitOfWork.UserSubscribesChannelRepository.FindSubscribersByChannelAsync(channelViewModel.Channel.Id);

            channelViewModel.LoadMyContentsAsync(channelViewModel.Channel.Id);
            channelViewModel.LoadMyVideoContentsAsync(channelViewModel.Channel.Id);

            channelName.Text = channelViewModel.Channel.channelName;
            channelSubCount.Text = channelViewModel.Channel.UserSubscribesChannels.Count.ToString();

            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;



        }


        private void editChannel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateChannelPage), channelViewModel.Channel);

        }

        private void manageChannel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageChannelPage), channelViewModel);
        }

        private void uploadVideo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UploadVideoPage));

        }

        private void uploadMusic_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UploadMusicPage));
        }

        private void myMusics_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyMusic),channelViewModel);

        }

        private void myVideos_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyVideos),channelViewModel);
        }

        //private void manageChannel_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(ManageChannelPage));
        //}
    }
}

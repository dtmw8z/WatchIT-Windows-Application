using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.MyChannel;
using WatchIT.UWP.views.SignUp;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.Profile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        public ChannelViewModel ChannelViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }
        public ProfilePage()
        {
            this.InitializeComponent();
            ChannelViewModel = new ChannelViewModel();
            UserViewModel = new UserViewModel();
        }

        private void createChannel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateChannelPage));
            Channel_visibility();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //LoadingControl.IsLoading = true;
            UserViewModel.User = App.userViewModel.loggedinUser;
            base.OnNavigatedTo(e);
            //LoadingControl.IsLoading = false;
            Channel_visibility();
        }
        private async void Channel_visibility()
        {
            bool hasChannel = await App.UnitOfWork.ChannelRepository.HasChannel(App.userViewModel.loggedinUser.Id);
            if (hasChannel)
            {

                createChannel.Visibility = Visibility.Collapsed;
                viewChannel.Visibility = Visibility.Visible;

            }


            else
            {
                createChannel.Visibility = Visibility.Visible;
                viewChannel.Visibility = Visibility.Collapsed;

            }

        }
      
        private async void viewChannel_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(MyChannelView), ChannelViewModel.Channel);



        }


    }
}

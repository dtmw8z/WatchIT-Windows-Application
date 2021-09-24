using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.AdminMangement.ManageChannels;
using WatchIT.UWP.views.AdminMangement.ManageMusics;
using WatchIT.UWP.views.AdminMangement.ManageUsers;
using WatchIT.UWP.views.AdminMangement.ManageVideos;
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

namespace WatchIT.UWP.views.AdminMangement.AdminPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminMainPage : Page
    {
        UserViewModel userViewModel { get; set; }
        public AdminMainPage()
        {
            this.InitializeComponent();
            userViewModel = new UserViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;

            await userViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;

        }



        private void manageUser_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageUsersPage));

        }

      

        private void manageChannel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageChannelsPage));
        }

        private void manageVideo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageVideosPage));

        }

        private void manageMusic_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageMusicsPage));

        }
    }
}

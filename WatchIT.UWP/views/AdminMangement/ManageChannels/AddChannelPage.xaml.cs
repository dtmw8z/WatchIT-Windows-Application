using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
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

namespace WatchIT.UWP.views.AdminMangement.ManageChannels
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddChannelPage : Page
    {
        ChannelViewModel channelViewModel { get; set; }
        public UserViewModel userViewModel { get; set; }
        public AddChannelPage()
        {
            this.InitializeComponent();
            channelViewModel = new ChannelViewModel();
            userViewModel = new UserViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await userViewModel.LoadAllAsync();
            if (e.Parameter != null)
            {
                userViewModel.User = e.Parameter as User;
            }
            base.OnNavigatedTo(e);
        }


        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool iscorrect = true;

                if (string.IsNullOrWhiteSpace(channelName.Text))
                {
                    iscorrect = false;
                    _ChannelNameCheckError.Text = "Channel name is required*";
                }
                if (string.IsNullOrWhiteSpace(channelDescription.Text))
                {
                    iscorrect = false;
                    _ChannelDescriptionCheckError.Text = "Channel description is required*";
                }
                //if (string.IsNullOrEmpty(userViewModel.User.Email)){

                //    iscorrect = false;
                //    _UserCheckError.Text = "Selection of user is required*";

                //}
               
                if (iscorrect) { 
                if (channelName.Text!=null|| channelDescription.Text != null)
                {
                    channelViewModel.Channel.channelName = channelName.Text;
                    channelViewModel.Channel.channelDescription = channelDescription.Text;
                    channelViewModel.Channel.userId = (userID.SelectedItem as User).Id;
                    await App.UnitOfWork.ChannelRepository.CreateAsync(channelViewModel.Channel);
                    this.Frame.Navigate(typeof(ManageChannelsPage));
                }
                else
                {
                    var checkboxDialog = new MessageDialog("Cannot Add Channel. Please check the data carefully. Note: Duplicate entry not allowed");
                    await checkboxDialog.ShowAsync();
                }
              }
                
            }
            catch
            {
                var checkboxDialog = new MessageDialog("Cannot Add Channel. Please check the data carefully. Note: Duplicate entry not allowed");
                await checkboxDialog.ShowAsync();
            }
           

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}

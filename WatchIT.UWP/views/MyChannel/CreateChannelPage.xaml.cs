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
using Windows.System;
using Windows.UI.Popups;
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
    public sealed partial class CreateChannelPage : Page
    {
        ChannelViewModel ChannelViewModel { get; set; }
        public CreateChannelPage()
        {
            this.InitializeComponent();
            ChannelViewModel = new ChannelViewModel();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ChannelViewModel.Channel = e.Parameter as Channel;
            }
            base.OnNavigatedTo(e);
        }



        private async void createChannel_Click(object sender, RoutedEventArgs e)
        {
            bool iscorrect = true;

            if (string.IsNullOrWhiteSpace(channelname.Text))
            {
                iscorrect = false;
                _ChannelNameCheckError.Text = "Channel name is required*";
            }
            if (string.IsNullOrWhiteSpace(channeldescription.Text))
            {
                iscorrect = false;
                _ChannelDescriptionCheckError.Text = "Channel description is required*";
            }
            if (iscorrect)
            {
                ContentDialog subscribeDialog = new ContentDialog
                {
                    Title = "Are you sure you want to create your channel?",
                    Content = "After you create channel, you can upload your own videos and upload your own music. You can also delete your channel anytime.",
                    CloseButtonText = "Not Now",
                    PrimaryButtonText = "Yes",
                    DefaultButton = ContentDialogButton.Primary
                };

                ContentDialogResult result = await subscribeDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {

                    ChannelViewModel.Channel.channelName = channelname.Text;
                    ChannelViewModel.Channel.channelDescription = channeldescription.Text;
                    ChannelViewModel.Channel.userId = App.userViewModel.loggedinUser.Id;
                    await ChannelViewModel.UpsertAsync(ChannelViewModel.Channel);

                    var checkboxDialog = new MessageDialog("Channel " + channelname.Text + " Created / Updated Successfully");
                    await checkboxDialog.ShowAsync();
                    this.Frame.Navigate(typeof(MyChannelView));

                }
                else
                {
                    if (this.Frame.CanGoBack)
                        this.Frame.GoBack();
                }
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProfilePage));

        }
    }
}

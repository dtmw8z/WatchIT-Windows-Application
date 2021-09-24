using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ManageChannelsPage : Page
    {
        public ChannelViewModel channelViewModel { get; set; }
        public ManageChannelsPage()
        {
            this.InitializeComponent();
            channelViewModel = new ChannelViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;

            await channelViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;

        }

        private async void manageChannel_RowEditEnded(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridRowEditEndedEventArgs e)
        {
            bool iscorrect = true;

            if (string.IsNullOrWhiteSpace(channelViewModel.Channel.channelName))
            {
                iscorrect = false;
                var checkboxDialog = new MessageDialog("Not a valid name or field is null");
                await checkboxDialog.ShowAsync();
            }
            if (string.IsNullOrWhiteSpace(channelViewModel.Channel.channelDescription))
            {
                iscorrect = false;
                var checkboxDialog = new MessageDialog("Not a valid Description or field is null");
                await checkboxDialog.ShowAsync();
            }
            if (iscorrect)
            {

                await App.UnitOfWork.ChannelRepository.UpsertAsync(channelViewModel.Channel);
                this.Frame.Navigate(typeof(ManageChannelsPage));
            }

        }

        private void addChannel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddChannelPage));

        }

        private async void deleteChannel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (channelViewModel.Channel != null)
                {
                    await channelViewModel.DeleteAsync(channelViewModel.Channel);
                    this.Frame.Navigate(typeof(ManageChannelsPage));
                }
                else
                {
                    var checkboxDialog = new MessageDialog("Please select channel");
                    await checkboxDialog.ShowAsync();
                }

            }
            catch
            {
                var checkboxDialog = new MessageDialog("Please select channel");
                await checkboxDialog.ShowAsync();
            }
            
        }
    }
}

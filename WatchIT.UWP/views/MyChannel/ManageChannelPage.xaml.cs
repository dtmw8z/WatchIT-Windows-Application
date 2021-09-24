using System;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.Home;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.MyChannel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageChannelPage : Page
    {
        public ChannelViewModel ChannelViewModel { get; set; }

        public ManageChannelPage()
        {
            this.InitializeComponent();
            ChannelViewModel = new ChannelViewModel();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ChannelViewModel = e.Parameter as ChannelViewModel;
            }

            base.OnNavigatedTo(e);
        }

        private async void deleteChannel_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog
            {
                Title = "Delete Channel ?",
                Content = "Are you sure you want to delete Your Channel?",
                PrimaryButtonText = "Go On",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await cd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {

               await ChannelViewModel.DeleteAsync(ChannelViewModel.Channel);
                var checkboxDialog = new MessageDialog("Channel deleted Successfully");
                await checkboxDialog.ShowAsync();
                this.Frame.Navigate(typeof(HomePage));
            }
        }
    }
}

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

namespace WatchIT.UWP.views.AdminMangement.ManageVideos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageVideosPage : Page
    {
        public VideoViewModel videoViewModel { get; set; }
        public ManageVideosPage()
        {
            this.InitializeComponent();
            videoViewModel = new VideoViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;

            await videoViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;

        }

        private async void manageVideos_RowEditEnded(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridRowEditEndedEventArgs e)
        {
            await App.UnitOfWork.VideoRepository.UpsertAsync(videoViewModel.Video);
            this.Frame.Navigate(typeof(ManageVideosPage));


        }

        private async void deleteVideo_Click(object sender, RoutedEventArgs e)
        {
            if (videoViewModel.Video != null)
            {
                await videoViewModel.DeleteAsync(videoViewModel.Video);
                this.Frame.Navigate(typeof(ManageVideosPage));
            }
            else
            {
                var checkboxDialog = new MessageDialog("Please select video");
                await checkboxDialog.ShowAsync();
            }
            

        }
    }
}

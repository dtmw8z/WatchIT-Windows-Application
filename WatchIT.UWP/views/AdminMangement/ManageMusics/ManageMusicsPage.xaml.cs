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

namespace WatchIT.UWP.views.AdminMangement.ManageMusics
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageMusicsPage : Page
    {
        public MusicViewModel musicViewModel { get; set; }
        public ManageMusicsPage()
        {
            this.InitializeComponent();
            musicViewModel = new MusicViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;
            await musicViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;
        }

        private async void manageMusics_RowEditEnded(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridRowEditEndedEventArgs e)
        {
            await App.UnitOfWork.MusicRepository.UpsertAsync(musicViewModel.Music);
            this.Frame.Navigate(typeof(ManageMusicsPage));

        }      

        private async void deleteMusic_Click(object sender, RoutedEventArgs e)
        {
            if (musicViewModel.Music != null)
            {
                await musicViewModel.DeleteAsync(musicViewModel.Music);
                this.Frame.Navigate(typeof(ManageMusicsPage));
            }
            else
            {
                var checkboxDialog = new MessageDialog("Please select music");
                await checkboxDialog.ShowAsync();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

namespace WatchIT.UWP.views.AdminMangement.ManageUsers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageUsersPage : Page
    {
        public UserViewModel userViewModel { get; set; }
        public ManageUsersPage()
        {
            this.InitializeComponent();
            userViewModel = new UserViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
           
            await userViewModel.LoadAllAsync();
            

            base.OnNavigatedTo(e);
        }

        private async void manageUser_RowEditEnded(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridRowEditEndedEventArgs e)
        {
            bool isCorrect = true;
            //verfy email algorithm

            if (string.IsNullOrWhiteSpace(userViewModel.User.Email) || !Regex.IsMatch(userViewModel.User.Email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                isCorrect = false;
                var checkboxDialog = new MessageDialog("Not a valid email or field is null");
                await checkboxDialog.ShowAsync();

            }
            if (string.IsNullOrWhiteSpace(userViewModel.User.fullName))
            {
                isCorrect = false;
                var checkboxDialog = new MessageDialog("Not a valid User Name or field is null");
                await checkboxDialog.ShowAsync();

            }

            if (isCorrect) {
                try
                {
                    await App.UnitOfWork.UserRepository.UpsertAsync(userViewModel.User);
                }
                catch
                {
                    var checkboxDialog = new MessageDialog("Incorrect data. Please Check your data correctly");
                    await checkboxDialog.ShowAsync();
                }
                finally
                {
                    this.Frame.Navigate(typeof(ManageUsersPage));
                }
                 
            }
            

        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddUsersPage),userViewModel);

        }

        //private async void deleteUser_Click(object sender, RoutedEventArgs e)
        //{


        //    try
        //    {
        //        if (userViewModel.User != null)
        //        {
        //            await userViewModel.DeleteAsync(userViewModel.User);
        //            this.Frame.Navigate(typeof(ManageUsersPage));
        //        }
        //        else
        //        {
        //            var checkboxDialog = new MessageDialog("Please select user");
        //            await checkboxDialog.ShowAsync();
        //        }

        //    }
        //    catch
        //    {
        //        var checkboxDialog = new MessageDialog("Please select user");
        //        await checkboxDialog.ShowAsync();
        //    }


        //}
    }
}

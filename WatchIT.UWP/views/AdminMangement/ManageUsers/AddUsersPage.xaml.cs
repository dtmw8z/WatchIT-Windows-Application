using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class AddUsersPage : Page
    {
        public UserViewModel userViewModel { get; set; }
        public AddUsersPage()
        {
            this.InitializeComponent();
            userViewModel = new UserViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await userViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            try {

                bool iscorrect = true;

                if (string.IsNullOrWhiteSpace(userName.Text))
                {
                    iscorrect = false;
                    _UserNameCheckError.Text = "User name is required*";
                }
                if (string.IsNullOrWhiteSpace(userEmail.Text))
                {
                    iscorrect = false;
                    _UserEmailCheckError.Text = "User email is required*";
                }
                if (userPassword.Password.Length < 7)
                {
                    iscorrect = false;
                    _PassCheckError.Text = "Password Require more than 7 character*";
                }

              

                if (iscorrect)
                {



                    if (userName != null || userEmail.Text != null || userPassword.Password != null)
                    {
                        userViewModel.User.fullName = userName.Text;
                        userViewModel.User.Email = userEmail.Text;
                        if (string.IsNullOrWhiteSpace(userEmail.Text) || !Regex.IsMatch(userEmail.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                        {
                            var checkboxDialog = new MessageDialog("Not a valid email or field cannot be empty");
                            await checkboxDialog.ShowAsync();

                        }
                        userViewModel.User.Password = App.userViewModel.Hash(userPassword.Password); ;
                        userViewModel.User.IsAdmin = (bool)isAdmin.IsChecked;
                        await App.UnitOfWork.UserRepository.CreateAsync(userViewModel.User);
                        this.Frame.Navigate(typeof(ManageUsersPage));
                    }
                    else
                    {
                        var checkboxDialog = new MessageDialog("Cannot Add User. Please check the data carefully. Note: Duplicate Entry not allowed or Field cannot be empty");
                        await checkboxDialog.ShowAsync();
                    }
                }
            }
            catch
            {
                var checkboxDialog = new MessageDialog("Cannot Add User. Please check the data carefully. Note: Duplicate Entry not allowed or Field cannot be empty");
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

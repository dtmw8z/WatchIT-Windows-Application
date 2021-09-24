using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.Home;
using WatchIT.UWP.views.Login;
using WatchIT.UWP.views.VideoViews;
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

namespace WatchIT.UWP.views.SignUp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
      
        public UserViewModel UserViewModel{ get; set; }
        public SignUpPage()
        {
            this.InitializeComponent();
            UserViewModel = new UserViewModel();
        }
        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.Navigate(typeof(HomePage));


        }

        private async void signupbtn_Click(object sender, RoutedEventArgs e)
        {

            bool isCorrect = true;
            //verfy email algorithm

            if (string.IsNullOrWhiteSpace(emailtext.Text) || !Regex.IsMatch(emailtext.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                isCorrect = false;
                _EmailCheckError.Text = "Please enter valid email";

            }
            else
            {
                Task<User> userTask = App.UnitOfWork.UserRepository.FindByEmailAsync(emailtext.Text);
                User user = await userTask;

                if (user != null)
                {
                    isCorrect = false;
                    var checkboxDialog = new MessageDialog("Email already exists");
                    await checkboxDialog.ShowAsync();
                }
            }

            //check with confirm password and use password box
            if (password.Password.Length < 7)
            {
                isCorrect = false;
                _PassCheckError.Text = "Password Require more than 7 character";
            }

            if (password.Password != confirmpassword.Password)
            {
                isCorrect = false;
                _PassCheckError.Text = "Please enter the same password";
            }

           



            if (isCorrect)
            {
                UserViewModel.User.fullName = fullnametext.Text;
                UserViewModel.User.Email = emailtext.Text;

               

                UserViewModel.User.Password = App.userViewModel.Hash(password.Password);

                User usr = await App.UnitOfWork.UserRepository.CreateAsync(UserViewModel.User);

                App.userViewModel.loggedinUser = usr;
                App.userViewModel.LogIn(usr.Id);

                ContentDialog cd = new ContentDialog
                {
                    Title = "Welcome",
                    Content = "Hello "+fullnametext.Text+". You are succesfully Registered",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await cd.ShowAsync();
                Frame.Navigate(typeof(HomePage));
            }

            
        }
      


        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));

        }
    }
}

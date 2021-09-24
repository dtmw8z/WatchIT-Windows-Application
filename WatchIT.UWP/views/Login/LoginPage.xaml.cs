using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.UWP.views.Home;
using WatchIT.UWP.views.SignUp;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void loginbtn_Click(object sender, RoutedEventArgs e)
        {
          
            string email = emailtext.Text;
            string password = passwd.Password;


            Task<User> task_user = App.UnitOfWork.UserRepository.FindByEmailAsync(email);
            User user = await task_user;

            if ((email.Length == 0) && (password.Length == 0))
            {              

                ContentDialog cd = new ContentDialog
                {
                    Title = "Empty data",
                    Content = "Please enter you email and password",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await cd.ShowAsync();
            }

            else if (user == null)
            {
                ContentDialog cd = new ContentDialog
                {
                    Title = "Incorrect data",
                    Content = "Please check Email and Password and try again",                   
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await cd.ShowAsync();

            }

            else if ((user.Email == email) && App.userViewModel.verifyHash(user.Password,password))
            {
                App.userViewModel.LogIn(user.Id);
                App.userViewModel.loggedinUser = user;
                this.Frame.Navigate(typeof(HomePage));
                // Construct the content
                var content = new ToastContentBuilder()
                    .AddText("Hello "+ user.fullName)
                    .AddText("You are succesfully logged in")
                    .GetToastContent();

                // Create the notification
                var notif = new ToastNotification(content.GetXml());

                // And show it!
                ToastNotificationManager.CreateToastNotifier().Show(notif);

            }
           
            else
            {
                ContentDialog cd = new ContentDialog
                {
                    Title = "Incorrect data",
                    Content = "Please check Email and Password and try again",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await cd.ShowAsync();


            }
            emailtext.Text = "";
            passwd.Password = "";
            

        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();


        }

        private void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage));

        }

        
    }
}

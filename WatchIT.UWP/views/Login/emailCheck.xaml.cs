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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WatchIT.UWP.views.Login
{
    public sealed partial class emailCheck : ContentDialog
    {
        public UserViewModel userViewModel { get; set; }
        public emailCheck()
        {
            this.InitializeComponent();
            userViewModel = new UserViewModel();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(sendEmail.Text) || !Regex.IsMatch(sendEmail.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                Task<User> usrtask = App.UnitOfWork.UserRepository.FindByEmailAsync(sendEmail.Text);
                User user = await usrtask;
                App.userViewModel.loggedinUser = user;

                Random rnd = new Random();
                int resetVerifyCode = rnd.Next(100000, 999999);

                App.userViewModel.code = resetVerifyCode;
                try
                {
                    App.userViewModel.sendEmail(resetVerifyCode, user.Email, user.fullName, "reset");
                }
                catch
                {
                    var checkboxDialog = new MessageDialog("Please check email. Either email is not registered or field is empty");
                    await checkboxDialog.ShowAsync();
                }

               
            }
            else
            {
                var checkboxDialog = new MessageDialog("Please check email. Either email is not registered or field is empty");
                await checkboxDialog.ShowAsync();
            }




            }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using Windows.Storage;
using Windows.UI.Popups;

namespace WatchIT.UWP.ViewModel
{
    public class UserViewModel
    {
        public ObservableCollection<User> Users { get; set; }

        public User User { get; set; }
        public User loggedinUser { get; set; }
        public int code { get; set; }

        public UserViewModel()
        {
            Users = new ObservableCollection<User>();
            User = new User();
            loggedinUser = new User();
            



        }

        public async Task LoadAllAsync()
        {

            List<User> list = await App.UnitOfWork.UserRepository.FindAllAsync();
            // Users = new ObservableCollection<User>(list);
            Users.Clear();
            foreach (User e in list)
            {
                
                Users.Add(e);
            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.UserRepository.CreateAsync(User);
        }

        public bool IsLoggedIn()
        {
            // get user auth value from local settings
            object authObject = ApplicationData.Current.LocalSettings.Values["isLoggedIn"];



            if (authObject != null)
            {
                return (Boolean)authObject;
            }
            else
            {
                return false;
            }
        }


        public void LogIn(int userid)
        {
            ApplicationData.Current.LocalSettings.Values["LoggedInuserID"] = userid;
            ApplicationData.Current.LocalSettings.Values["isLoggedIn"] = true;
           
        }

        public void LogOut()
        {
            ApplicationData.Current.LocalSettings.Values["isLoggedIn"] = false;
            ApplicationData.Current.LocalSettings.Values["LoggedInuserID"] = 0;

        }

        public int GetCurrentUserID()
        {
            object curID = ApplicationData.Current.LocalSettings.Values["LoggedInuserID"];
            int LoggedInuserID = (int)curID;

            return LoggedInuserID;
        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }

        public bool verifyHash(string hashedPassword, string plainPassword)
        {
            return Hash(plainPassword) == hashedPassword;
        }

        public async void sendEmail(int code, string email, string name, string type)
        {
            try
            {
                var fromAddress = new MailAddress("watchithere55@gmail.com");
                var toAddress = new MailAddress(email, name);
                const string fromPassword = "Watchit123.";
                string subject = "Password Reset - WatchIT";
                string body = $" Please use the following code to reset your password <br> <br> <b><h2> {code.ToString()} </h2></b><br><br> Ignore, if you didn't request this. Thank you, <br><br><hr><h1> WatchIT</h3>";




                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {

                    Subject = subject,
                    Body = body
                })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch
            {
                var checkboxDialog = new MessageDialog("Please check email. Either email is not registered or field is empty");
                await checkboxDialog.ShowAsync();
            }

            
        }


        internal async Task DeleteAsync(User User)
        {
            await App.UnitOfWork.UserRepository.RemoveUserAsync(User);
            Users.Remove(User);
        }


    }
}

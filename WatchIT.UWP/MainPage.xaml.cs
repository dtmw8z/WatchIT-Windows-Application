using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WatchIT.UWP.views;
using WatchIT.UWP.views.Home;
using WatchIT.UWP.views.VideoViews;
using WatchIT.UWP.views.MusicViews;
using WatchIT.UWP.views.Profile;
using WatchIT.UWP.views.Library;
using WatchIT.UWP.views.Setting;
using WatchIT.UWP.views.MyChannel;
using WatchIT.UWP.views.Login;
using Windows.Storage;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.UWP.views.AdminMangement.AdminPage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WatchIT.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void manage_visibility()
        {
            if (App.userViewModel.IsLoggedIn())
            {
                login.Visibility = Visibility.Collapsed;
                logout.Visibility = Visibility.Visible;
                Profile.Visibility = Visibility.Visible;
                Library.Visibility = Visibility.Visible;
                if (App.userViewModel.loggedinUser.IsAdmin==true) {
                    Admin.Visibility = Visibility.Visible;
                }
                else
                {
                    Admin.Visibility = Visibility.Collapsed;
                }
                

            }
            else
            {
                login.Visibility = Visibility.Visible;
                logout.Visibility = Visibility.Collapsed;
                Profile.Visibility = Visibility.Collapsed;
                Library.Visibility = Visibility.Collapsed;
                Admin.Visibility = Visibility.Collapsed;


            }
        }

        private async void channel_visibility()
        {
            bool hasChannel = await App.UnitOfWork.ChannelRepository.HasChannel(App.userViewModel.loggedinUser.Id);
            if (App.userViewModel.IsLoggedIn() && hasChannel)
            {

                MyChannel.Visibility = Visibility.Visible;
                Content.Visibility = Visibility.Visible;
                UploadMusic.Visibility = Visibility.Visible;
                UploadVideo.Visibility = Visibility.Visible;
                

            }
            else
            {
                MyChannel.Visibility = Visibility.Collapsed;
                Content.Visibility = Visibility.Collapsed;
                UploadMusic.Visibility = Visibility.Collapsed;
                UploadVideo.Visibility = Visibility.Collapsed;
            }

        }

        private async void menu_LoadedAsync(object sender, RoutedEventArgs e)
        {
            manage_visibility();
            channel_visibility();

            if (App.userViewModel.IsLoggedIn())
            {
                int userID = (int)ApplicationData.Current.LocalSettings.Values["LoggedInuserID"];
                if (userID != 0)
                {
                    User current = await App.UnitOfWork.UserRepository.FindByIdAsync(userID);
                    current.Playlists = await App.UnitOfWork.PlaylistRepository.FindPlaylistAsync(current.Id);
                    App.userViewModel.loggedinUser = current;
                }
            }
            contentFrame.Navigate(typeof(HomePage));
            menu.SelectedItem = menu.MenuItems[0];
            


        }


        private void menu_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {

            manage_visibility();
            channel_visibility();
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {

                var item = args.InvokedItemContainer as Microsoft.UI.Xaml.Controls.NavigationViewItem;

                switch (item.Tag)
                {
                    case "home":
                        contentFrame.Navigate(typeof(HomePage));
                        break;

                    case "video":
                        contentFrame.Navigate(typeof(VideoPage));
                        break;

                    case "music":
                        contentFrame.Navigate(typeof(MusicPage));
                        break;

                    case "profile":
                        contentFrame.Navigate(typeof(ProfilePage));
                        break;

                    case "playlist":
                        contentFrame.Navigate(typeof(PlaylistPage));
                        break;

                    case "library":
                        contentFrame.Navigate(typeof(LibraryPage));
                        break;

                    case "mychannel":
                        contentFrame.Navigate(typeof(MyChannelView));
                        break;

                    case "uploadvideo":
                        contentFrame.Navigate(typeof(UploadVideoPage));
                        break;

                    case "uploadmusic":
                        contentFrame.Navigate(typeof(UploadMusicPage));
                        break;

                    case "admin":
                        contentFrame.Navigate(typeof(AdminMainPage));
                        break;

                }
            }
        }

        private void login_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            contentFrame.Navigate(typeof(LoginPage));
            menu.SelectedItem = menu.MenuItems[0];

        }

        private void logout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.userViewModel.LogOut();
            contentFrame.Navigate(typeof(HomePage));
            menu.SelectedItem = menu.MenuItems[0];
        }

        private void contentFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            manage_visibility();
            channel_visibility();

            if (e.SourcePageType == typeof(HomePage))
            {
                menu.SelectedItem = Home;
            }
            else if (e.SourcePageType == typeof(VideoPage))
            {
                menu.SelectedItem = Video;
            }
            else if (e.SourcePageType == typeof(MusicPage))
            {
                menu.SelectedItem = Music;
            }
            else if (e.SourcePageType == typeof(PlaylistPage))
            {
                menu.SelectedItem = Playlist;
            }
          
            else if (e.SourcePageType == typeof(MyChannelView))
            {
                menu.SelectedItem = MyChannel;
            }
            else if (e.SourcePageType == typeof(LoginPage))
            {
                menu.SelectedItem = login;
            }
            else if (e.SourcePageType == typeof(ProfilePage))
            {
                menu.SelectedItem = Profile;
            }
            else if (e.SourcePageType == typeof(SettingsPage))
            {
                menu.SelectedItem = menu.SettingsItem;
            }
            else if (e.SourcePageType == typeof(UploadVideoPage))
            {
                menu.SelectedItem = UploadVideo;
            }
            else if (e.SourcePageType == typeof(UploadMusicPage))
            {
                menu.SelectedItem = UploadMusic;
            }
            else if (e.SourcePageType == typeof(AdminMainPage))
            {
                menu.SelectedItem = Admin;
            }

        }

        private void menu_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            if (contentFrame.CanGoBack)
            {
                contentFrame.GoBack();
            }

        }

        private List<string> Cats = new List<string>(){};




        private void Search_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Search.Text = args.SelectedItem.ToString();

        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            foreach (var cat in Cats)
            {
                var found = splitText.All((key) =>
                {
                    return cat.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(cat);
                }
            }
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No results found");
            }
            sender.ItemsSource = suitableItems;


        }
    }
}

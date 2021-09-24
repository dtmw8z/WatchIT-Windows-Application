using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
using WatchIT.UWP.views.Home;
using WatchIT.UWP.views.Library;
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

namespace WatchIT.UWP.views.MusicViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicSinglePage : Page
    {
        public MusicCommentViewModel MusicCommentViewModel { get; set; }
        public MusicViewModel MusicViewModel { get; set; }
        public MusicLikeViewModel MusicLikeViewModel { get; set; }

        public MusicViewModel SuggestedMusicViewModel { get; set; }
        public UserSubscribesChannelViewModel UserSubscribesChannelViewModel { get; set; }

        public MusicPlaylist MusicPlaylist { get; set; }
        public PlaylistViewModel PlaylistviewModel { get; set; }
        public ObservableCollection<Music> SuggestedMusics { get; set; }
        public UserViewModel UserViewModel { get; set; }



        public MusicSinglePage()
        {

            this.InitializeComponent();
            MusicCommentViewModel = new MusicCommentViewModel();
            UserSubscribesChannelViewModel = new UserSubscribesChannelViewModel();
            MusicLikeViewModel = new MusicLikeViewModel();
            MusicViewModel = new MusicViewModel();

            SuggestedMusics = new ObservableCollection<Music>();


            SuggestedMusicViewModel = new MusicViewModel();
            UserViewModel = new UserViewModel();

        }
      
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;
            if (e.Parameter != null)
            {
                MusicViewModel.Music = e.Parameter as Music;
                MusicViewModel.Music.MusicComments = await App.UnitOfWork.MusicCommentRepository.FindMusicCommentAsync(MusicViewModel.Music.Id);
                MusicViewModel.Music.Channel = await App.UnitOfWork.ChannelRepository.FindByIdAsync(MusicViewModel.Music.channelId);
                MusicViewModel.Music.Channel.UserSubscribesChannels = await App.UnitOfWork.UserSubscribesChannelRepository.FindSubscribersByChannelAsync(MusicViewModel.Music.Channel.Id);
                MusicViewModel.Music.Channel.Videos = await App.UnitOfWork.VideoRepository.FindVideoByChannelAsync(MusicViewModel.Music.Channel.Id);
                MusicViewModel.Music.Channel.Musics = await App.UnitOfWork.MusicRepository.FindMusicByChannelAsync(MusicViewModel.Music.Channel.Id);

            }
            bool hasLike = await App.UnitOfWork.MusicLikeRepository.HasLike(App.userViewModel.loggedinUser.Id, MusicViewModel.Music.Id);
            if (hasLike)
            {
                MusicLikeViewModel.MusicLike = await App.UnitOfWork.MusicLikeRepository.FindByMusicIdAsync(App.userViewModel.loggedinUser.Id, MusicViewModel.Music.Id);
            }
            bool hasSubscribe = await App.UnitOfWork.UserSubscribesChannelRepository.HasSubscribe(App.userViewModel.loggedinUser.Id, MusicViewModel.Music.channelId);
            if (hasSubscribe)
            {
                UserSubscribesChannelViewModel.UserSubscribesChannel = await App.UnitOfWork.UserSubscribesChannelRepository.FindByUserChannelIdAsync(App.userViewModel.loggedinUser.Id, MusicViewModel.Music.channelId);
            }

            string path = Path.GetTempPath();


            await File.WriteAllBytesAsync(@path + "\\music.mp3", MusicViewModel.Music.content);

            myPlayer.Source = new Uri(@path + "\\music.mp3");


           


            //await MusicCommentViewModel.LoadCurrentMusicCommentAsync(MusicViewModel.Music.Id);
            await MusicViewModel.LoadAllAsync();
            App.userViewModel.loggedinUser.Playlists = await App.UnitOfWork.PlaylistRepository.FindPlaylistAsync(App.userViewModel.GetCurrentUserID());
            await UserViewModel.LoadAllAsync();
            //  await PlaylistviewModel.LoadAllAsync();
            List<Music> suggested = MusicViewModel.Musics.Where(x => x.Id != MusicViewModel.Music.Id).ToList();
            foreach (Music v in suggested)
            {
                SuggestedMusics.Add(v);
            }
            subscribe_visibility();
            Like_visibility();
            comment_visiblity();
            base.OnNavigatedTo(e);

            LoadingControl.IsLoading = false;

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            string path = Path.GetTempPath();
           File.Delete(@path + "\\music.mp3");

            base.OnNavigatedFrom(e);
        }




        private async void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {


                UserSubscribesChannelViewModel.UserSubscribesChannel.userId = App.userViewModel.loggedinUser.Id;
                UserSubscribesChannelViewModel.UserSubscribesChannel.channelId = MusicViewModel.Music.channelId;
                await App.UnitOfWork.UserSubscribesChannelRepository.CreateAsync(UserSubscribesChannelViewModel.UserSubscribesChannel);
                this.Frame.Navigate(typeof(MusicSinglePage), MusicViewModel.Music);

           

            subscribe_visibility();
           

        }
        private async void like_click(object sender, RoutedEventArgs e)
        {


            MusicLikeViewModel.MusicLike.userId = App.userViewModel.loggedinUser.Id;
            MusicLikeViewModel.MusicLike.musicId = MusicViewModel.Music.Id;
            await App.UnitOfWork.MusicLikeRepository.CreateAsync(MusicLikeViewModel.MusicLike);
            this.Frame.Navigate(typeof(MusicSinglePage), MusicViewModel.Music);
            Like_visibility();
        }
        private async void Post_Click(object sender, RoutedEventArgs e)
        {
            bool iscorrect = true;
            if (string.IsNullOrWhiteSpace(commentText.Text))
            {
                iscorrect = false;
                _CommentError.Text = "Sorry! Comment can't be blank";
            }
            if (iscorrect)
            {
                MusicCommentViewModel.MusicComment.commentText = commentText.Text;
                MusicCommentViewModel.MusicComment.musicId = MusicViewModel.Music.Id;
                MusicCommentViewModel.MusicComment.userId = App.userViewModel.loggedinUser.Id;

                await App.UnitOfWork.MusicCommentRepository.CreateAsync(MusicCommentViewModel.MusicComment);
                var checkboxDialog = new MessageDialog("Thanks for your comment");
                await checkboxDialog.ShowAsync();
                this.Frame.Navigate(typeof(MusicPage), MusicViewModel);               
                subscribe_visibility();// for hiding post button if loggedout

            }
        }

        private async void subscribe_visibility()
        {
            bool hasSubscribe = await App.UnitOfWork.UserSubscribesChannelRepository.HasSubscribe(App.userViewModel.loggedinUser.Id, MusicViewModel.Music.channelId);
            if (hasSubscribe)
            {

                subscribe.Visibility = Visibility.Collapsed;
                unsubscribe.Visibility = Visibility.Visible;

            }
            else if ((App.userViewModel.loggedinUser.Id == MusicViewModel.Music.Channel.userId))
            {
                subscribe.IsEnabled = false;
                unsubscribe.Visibility = Visibility.Collapsed;
                cannotSub.Visibility = Visibility.Visible;
            

            }
            else if (!App.userViewModel.IsLoggedIn())
            {
                subscribe.Visibility = Visibility.Collapsed;
                unsubscribe.Visibility = Visibility.Collapsed;
                addtoPlaylist.Visibility = Visibility.Collapsed;
                post.Visibility = Visibility.Collapsed;
                commentText.Visibility = Visibility.Collapsed;
                cmt.Visibility = Visibility.Collapsed;
            }

            else
            {
                subscribe.Visibility = Visibility.Visible;
                unsubscribe.Visibility = Visibility.Collapsed;

            }

        }

        private async void comment_visiblity()
        {
            bool hasComment = await App.UnitOfWork.MusicCommentRepository.HasCommented(App.userViewModel.loggedinUser.Id, MusicViewModel.Music.Id);
            if (hasComment)
            {
                post.Visibility = Visibility.Collapsed;
                commentText.Visibility = Visibility.Collapsed;
                cmt.Visibility = Visibility.Collapsed;
                _CommentError.Text = "Sorry! You can only comment once to this music";
            }
            else
            {
                post.Visibility = Visibility.Visible;
                commentText.Visibility = Visibility.Visible;
                cmt.Visibility = Visibility.Visible;
            }

        }


        private async void Like_visibility()
        {
            bool hasLike = await App.UnitOfWork.MusicLikeRepository.HasLike(App.userViewModel.loggedinUser.Id, MusicViewModel.Music.Id);
            if (hasLike)
            {

                like.Visibility = Visibility.Collapsed;
                dislike.Visibility = Visibility.Visible;


            }
            else if ((App.userViewModel.loggedinUser.Id == MusicViewModel.Music.Channel.userId) || (!App.userViewModel.IsLoggedIn()))
            {
                like.Visibility = Visibility.Collapsed;
                dislike.Visibility = Visibility.Collapsed;
               

            }

            else
            {
                like.Visibility = Visibility.Visible;
                dislike.Visibility = Visibility.Collapsed;


            }

        }




        private void MusicSuggestion_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Music music)
            {
                SuggestedMusicViewModel.Music = music;
                this.Frame.Navigate(typeof(MusicSinglePage), SuggestedMusicViewModel.Music);
            }
        }

        private async void UnSubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog
            { 
                Title = "Unsubscribe",
                Content = "Are you sure you want to unsubscribe",
                PrimaryButtonText = "Unsubscribe",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await cd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await UserSubscribesChannelViewModel.DeleteAsync(UserSubscribesChannelViewModel.UserSubscribesChannel);
                var checkboxDialog = new MessageDialog("Unsubscribed Successfully");
                await checkboxDialog.ShowAsync();

                this.Frame.Navigate(typeof(MusicSinglePage), MusicViewModel.Music);

            }

        }
        private async void dislike_click(object sender, RoutedEventArgs e)
        {
            await MusicLikeViewModel.DeleteAsync(MusicLikeViewModel.MusicLike);
            this.Frame.Navigate(typeof(MusicSinglePage), MusicViewModel.Music);
        }

        private async void addtoPlaylist_Click(object sender, RoutedEventArgs e)
        {
            AddtoPlaylist dlg = new AddtoPlaylist(MusicViewModel, App.userViewModel.loggedinUser);
            var res = await dlg.ShowAsync();

            this.Frame.Navigate(typeof(PlaylistPage));
        }
    }




}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using WatchIT.UWP.ViewModel;
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

namespace WatchIT.UWP.views.VideoViews
{
    public sealed partial class VideoSingle : Page
    {
        public VideoCommentViewModel VideoCommentViewModel { get; set; }
        public VideoViewModel VideoViewModel { get; set; }
        public VideoLikeViewModel VideoLikeViewModel { get; set; }

        public VideoViewModel SuggestedVideoViewModel { get; set; }
        public UserSubscribesChannelViewModel UserSubscribesChannelViewModel { get; set; }

        public VideoPlaylist VideoPlaylist { get; set; }
        public PlaylistViewModel PlaylistviewModel { get; set; }

        public ObservableCollection<Video> SuggestedVideos { get; set; }
        public UserViewModel UserViewModel { get; set; }





        public VideoSingle()
        {

            this.InitializeComponent();
            VideoCommentViewModel = new VideoCommentViewModel();
            UserSubscribesChannelViewModel = new UserSubscribesChannelViewModel();
            VideoLikeViewModel = new VideoLikeViewModel();
            VideoViewModel = new VideoViewModel();

            SuggestedVideos = new ObservableCollection<Video>();

            SuggestedVideoViewModel = new VideoViewModel();
            UserViewModel = new UserViewModel();

        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingControl.IsLoading = true;

            if (e.Parameter != null)
            {
                VideoViewModel.Video = e.Parameter as Video;
                VideoViewModel.Video.Channel = await App.UnitOfWork.ChannelRepository.FindByIdAsync(VideoViewModel.Video.channelId);
                VideoViewModel.Video.Channel.UserSubscribesChannels = await App.UnitOfWork.UserSubscribesChannelRepository.FindSubscribersByChannelAsync(VideoViewModel.Video.Channel.Id);
                VideoViewModel.Video.Channel.Videos = await App.UnitOfWork.VideoRepository.FindVideoByChannelAsync(VideoViewModel.Video.Channel.Id);
                VideoViewModel.Video.Channel.Musics = await App.UnitOfWork.MusicRepository.FindMusicByChannelAsync(VideoViewModel.Video.Channel.Id);
                VideoViewModel.Video.VideoComments = await App.UnitOfWork.VideoCommentRepository.FindVideoCommentAsync(VideoViewModel.Video.Id);

            }
            bool hasLike = await App.UnitOfWork.VideoLikeRepository.HasLike(App.userViewModel.loggedinUser.Id, VideoViewModel.Video.Id);
            if (hasLike)
            {
                VideoLikeViewModel.VideoLike = await App.UnitOfWork.VideoLikeRepository.FindByVideoIdAsync(App.userViewModel.loggedinUser.Id, VideoViewModel.Video.Id);
            }
            bool hasSubscribe = await App.UnitOfWork.UserSubscribesChannelRepository.HasSubscribe(App.userViewModel.loggedinUser.Id, VideoViewModel.Video.channelId);
            if (hasSubscribe)
            {
                UserSubscribesChannelViewModel.UserSubscribesChannel = await App.UnitOfWork.UserSubscribesChannelRepository.FindByUserChannelIdAsync(App.userViewModel.loggedinUser.Id, VideoViewModel.Video.channelId);
            }
            string path = Path.GetTempPath();
            await File.WriteAllBytesAsync(@path + "\\Video.mp4", VideoViewModel.Video.content);

            myVideoPlayer.Source = new Uri(@path + "\\Video.mp4");

            App.userViewModel.loggedinUser.Playlists = await App.UnitOfWork.PlaylistRepository.FindPlaylistAsync(App.userViewModel.GetCurrentUserID());
            await UserViewModel.LoadAllAsync();
            await VideoViewModel.LoadAllAsync();

            List<Video> suggested = VideoViewModel.Videos.Where(x => x.Id != VideoViewModel.Video.Id).ToList();
            foreach (Video v in suggested)
            {
                SuggestedVideos.Add(v);
            }
            Like_visibility();
            subscribe_visibility();
            comment_visibilithy();
            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;



        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            string path = Path.GetTempPath();
            File.Delete(@path + "\\Video.mp4");

            base.OnNavigatedFrom(e);
        }




        private async void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {


            UserSubscribesChannelViewModel.UserSubscribesChannel.userId = App.userViewModel.loggedinUser.Id;
            UserSubscribesChannelViewModel.UserSubscribesChannel.channelId = VideoViewModel.Video.channelId;
            await App.UnitOfWork.UserSubscribesChannelRepository.CreateAsync(UserSubscribesChannelViewModel.UserSubscribesChannel);
            this.Frame.Navigate(typeof(VideoSingle), VideoViewModel.Video);



            subscribe_visibility();


        }
        private async void like_click(object sender, RoutedEventArgs e)
        {


            VideoLikeViewModel.VideoLike.userId = App.userViewModel.loggedinUser.Id;
            VideoLikeViewModel.VideoLike.videoId = VideoViewModel.Video.Id;
            await App.UnitOfWork.VideoLikeRepository.CreateAsync(VideoLikeViewModel.VideoLike);
            this.Frame.Navigate(typeof(VideoSingle), VideoViewModel.Video);
            Like_visibility();
        }
        private async void Post_Click(object sender, RoutedEventArgs e)
        {
            bool iscorrect = true;
            if (string.IsNullOrWhiteSpace(commentText.Text))
            {
                iscorrect = false;
              
            }
            if (iscorrect)
            {
                comment_visibilithy();
                VideoCommentViewModel.VideoComment.commentText = commentText.Text;
                VideoCommentViewModel.VideoComment.videoId = VideoViewModel.Video.Id;
                VideoCommentViewModel.VideoComment.userId = App.userViewModel.loggedinUser.Id;

                await App.UnitOfWork.VideoCommentRepository.CreateAsync(VideoCommentViewModel.VideoComment);

                var checkboxDialog = new MessageDialog("Thanks for your comment");
                await checkboxDialog.ShowAsync();



                this.Frame.Navigate(typeof(VideoPage));

            }
        }

        private async void comment_visibilithy()
        {
            bool hasCommented = await App.UnitOfWork.VideoCommentRepository.HasCommented(App.userViewModel.loggedinUser.Id, VideoViewModel.Video.Id);
            {
                if (hasCommented)
                {
                    post.Visibility = Visibility.Collapsed;
                    commentText.Visibility = Visibility.Collapsed;
                    cmt.Visibility = Visibility.Collapsed;
                    _CommentError.Text = "You can only comment once to this video";
                }
                else
                {
                    post.Visibility = Visibility.Visible;
                    commentText.Visibility = Visibility.Visible;
                    cmt.Visibility = Visibility.Visible;
                }
            }
        }


            private async void subscribe_visibility()
            {
                bool hasSubscribe = await App.UnitOfWork.UserSubscribesChannelRepository.HasSubscribe(App.userViewModel.loggedinUser.Id, VideoViewModel.Video.channelId);
                if (hasSubscribe)
                {

                    subscribe.Visibility = Visibility.Collapsed;
                    unsubscribe.Visibility = Visibility.Visible;

                }
                else if ((App.userViewModel.loggedinUser.Id == VideoViewModel.Video.Channel.userId))
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


            private async void Like_visibility()
            {
                bool hasLike = await App.UnitOfWork.VideoLikeRepository.HasLike(App.userViewModel.loggedinUser.Id, VideoViewModel.Video.Id);
                if (hasLike)
                {

                    like.Visibility = Visibility.Collapsed;
                    dislike.Visibility = Visibility.Visible;


                }
                else if ((App.userViewModel.loggedinUser.Id == VideoViewModel.Video.Channel.userId) || (!App.userViewModel.IsLoggedIn()))
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



            private void VideoSuggestion_ItemClick(object sender, ItemClickEventArgs e)
            {
                if (e.ClickedItem is Video Video)
                {
                    SuggestedVideoViewModel.Video = Video;
                    this.Frame.Navigate(typeof(VideoSingle), SuggestedVideoViewModel.Video);
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

                    this.Frame.Navigate(typeof(VideoSingle), VideoViewModel.Video);

                }
            subscribe_visibility();

            }
            private async void dislike_click(object sender, RoutedEventArgs e)
            {
                await VideoLikeViewModel.DeleteAsync(VideoLikeViewModel.VideoLike);
                this.Frame.Navigate(typeof(VideoSingle), VideoViewModel.Video);
            }

            private async void addtoPlaylist_Click(object sender, RoutedEventArgs e)
            {
                AddVideotoPlaylist dlg = new AddVideotoPlaylist(VideoViewModel, App.userViewModel.loggedinUser);
                var res = await dlg.ShowAsync();

                this.Frame.Navigate(typeof(PlaylistPage));
            }
        }
    }

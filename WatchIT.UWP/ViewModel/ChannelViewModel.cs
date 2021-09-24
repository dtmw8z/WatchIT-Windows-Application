using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel
{
    public class ChannelViewModel
    {
        public ObservableCollection<Channel> Channels { get; set; }

        public Channel Channel { get; set; }
        public ObservableCollection<Music> Musics { get; set; }
        public ObservableCollection<Video> Videos { get; set; }
        public MusicViewModel musicViewModel { get; set; }
        public VideoViewModel videoViewModel { get; set; }


        public ChannelViewModel()
        {
            Channels = new ObservableCollection<Channel>();
            Channel = new Channel();
            Musics = new ObservableCollection<Music>();
            Videos = new ObservableCollection<Video>();
            musicViewModel = new MusicViewModel();
            videoViewModel = new VideoViewModel();
        }

        public async Task LoadAllAsync()
        {

            List<Channel> list = await App.UnitOfWork.ChannelRepository.FindAllAsync();
            Channels.Clear();
            foreach (Channel e in list)
            {
                e.User = await App.UnitOfWork.UserRepository.FindByIdAsync(e.userId);
                e.Videos = await App.UnitOfWork.VideoRepository.FindVideoByChannelAsync(e.Id);
                e.Musics = await App.UnitOfWork.MusicRepository.FindMusicByChannelAsync(e.Id);
                e.UserSubscribesChannels = await App.UnitOfWork.UserSubscribesChannelRepository.FindSubscribersByChannelAsync(e.Id);
                Channels.Add(e);
            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.ChannelRepository.CreateAsync(Channel);
        }

        internal async Task DeleteAsync(Channel channel)
        {
            if (Musics != null)
            {
                foreach (Music m in Musics)
                {
                    await musicViewModel.DeleteAsync(m);
                }
            }


            if (Videos != null)
            {
                foreach (Video v in Videos)
                {
                    await videoViewModel.DeleteAsync(v);
                }
            }

            await App.UnitOfWork.ChannelRepository.RemoveChannelAsync(channel);
            
        }

        internal async void LoadMyContentsAsync(int channelId)
        {

            List<Music> mu = await App.UnitOfWork.MusicRepository.FindMusicByChannelAsync(channelId);

            Musics.Clear();
            foreach(Music m in mu)
            {
                m.MusicLikes = await App.UnitOfWork.MusicLikeRepository.FindMusicLikeAsync(m.Id);
                m.MusicComments = await App.UnitOfWork.MusicCommentRepository.FindMusicCommentAsync(m.Id);

                Musics.Add(m);
            }
        }

        internal async void LoadMyVideoContentsAsync(int channelId)
        {
            List<Video> vi = await App.UnitOfWork.VideoRepository.FindVideoByChannelAsync(channelId);
            Videos.Clear();
            foreach (Video v in vi)
            {
                v.VideoLikes = await App.UnitOfWork.VideoLikeRepository.FindVideoLikeAsync(v.Id);
                v.VideoComments = await App.UnitOfWork.VideoCommentRepository.FindVideoCommentAsync(v.Id);
                Videos.Add(v);
            }
        }

        internal async Task UpsertAsync(Channel cn)
        {
            await App.UnitOfWork.ChannelRepository.UpsertAsync(cn);
        }
    }

}

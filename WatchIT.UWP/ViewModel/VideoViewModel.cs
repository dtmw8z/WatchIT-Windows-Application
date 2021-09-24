using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace WatchIT.UWP.ViewModel
{
   public class VideoViewModel
    {
        public ObservableCollection<Video> Videos { get; set; }
        public ObservableCollection<VideoComment> Comments { get; set; }

        public Video Video { get; set; }


        public VideoViewModel()
        {
            Videos = new ObservableCollection<Video>();
            Comments = new ObservableCollection<VideoComment>();
            Video = new Video();
        }

        public async Task LoadAllAsync()
        {

            List<Video> list = await App.UnitOfWork.VideoRepository.FindAllAsync();
            // Videos = new ObservableCollection<Video>(list);
            Videos.Clear();
            foreach (Video e in list)
            {
                e.Channel = await App.UnitOfWork.ChannelRepository.FindByIdAsync(e.channelId);
                e.VideoLikes = await App.UnitOfWork.VideoLikeRepository.FindVideoLikeAsync(e.Id);
                e.VideoComments = await App.UnitOfWork.VideoCommentRepository.FindVideoCommentAsync(e.Id);
                Videos.Add(e);
            }

            foreach(Video m in Videos)
            {
                m.Channel = await App.UnitOfWork.ChannelRepository.FindByIdAsync(m.channelId);
                m.VideoComments = (await App.UnitOfWork.VideoCommentRepository.FindVideoCommentAsync(m.Id)).ToList();
                m.VideoLikes = (await App.UnitOfWork.VideoLikeRepository.FindVideoLikeAsync(m.Id)).ToList();
                foreach (VideoLike mc in m.VideoLikes)
                {
                    mc.Video = await App.UnitOfWork.VideoRepository.FindByIdAsync(mc.videoId);
                    mc.User = await App.UnitOfWork.UserRepository.FindByIdAsync(mc.userId);
                }

                foreach (VideoComment mc in m.VideoComments)
                {
                    mc.Video = await App.UnitOfWork.VideoRepository.FindByIdAsync(mc.videoId);
                    mc.User = await App.UnitOfWork.UserRepository.FindByIdAsync(mc.userId);
                }
            }
        }

        public async Task LoadVideoinPlaylistAsync(int playlistID)
        {
            List<Video> Video = await App.UnitOfWork.VideoRepository.FindVideosInPlaylistAsync(playlistID);
            Videos.Clear();
            foreach (Video e in Video)
            {
                Videos.Add(e);

            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.VideoRepository.CreateAsync(Video);
        }
        public async Task<byte[]> GetBytesAsync(StorageFile file)
        {
            byte[] fileBytes = null;

            if (file is null)
            {
                return null;
            }

            using (var stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];

                using (var reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }
            return fileBytes;
        }
        public async Task<byte[]> ConvertImageToByte(StorageFile file)
        {
            if (file == null)
            {
                return null;
            }

                using (var inputStream = await file.OpenSequentialReadAsync())
                {
                    var readStream = inputStream.AsStreamForRead();

                    var byteArray = new byte[readStream.Length];
                    await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                    return byteArray;
                }

            
            
        }

        internal async Task DeleteAsync(Video Video)
        {
            await App.UnitOfWork.VideoRepository.RemoveVideoAsync(Video);
            Videos.Remove(Video);
        }

        public int GetCurrentVideoID()
        {
            object currVideoID = ApplicationData.Current.LocalSettings.Values["CurrentVideoID"];
            int CurrentVideoID = (int)currVideoID;

            return CurrentVideoID;
        }

        internal async Task UpsertAsync(Video v)
        {
            await App.UnitOfWork.VideoRepository.UpsertAsync(v);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel


{
    public class VideoLikeViewModel
    {
        public ObservableCollection<VideoLike> VideoLikes { get; set; }

        public VideoLike VideoLike { get; set; }

        public VideoLikeViewModel()
        {
            VideoLikes = new ObservableCollection<VideoLike>();
            VideoLike = new VideoLike();
        }

        public async Task LoadAllAsync()
        {

            List<VideoLike> list = await App.UnitOfWork.VideoLikeRepository.FindAllAsync();
            // Likes = new ObservableCollection<Like>(list);
            VideoLikes.Clear();
            foreach (VideoLike e in list)
            {
                VideoLikes.Add(e);
            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.VideoLikeRepository.CreateAsync(VideoLike);
        }


        internal async Task DeleteAsync(VideoLike VideoLike)
        {
            await App.UnitOfWork.VideoLikeRepository.DeleteAsync(VideoLike);
            VideoLikes.Remove(VideoLike);

        }
    }
}


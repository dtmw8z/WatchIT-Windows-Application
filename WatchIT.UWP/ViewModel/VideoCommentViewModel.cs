using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel
{
    public class VideoCommentViewModel
    {
        public ObservableCollection<VideoComment> VideoComments { get; set; }
        public VideoComment VideoComment { get; set; }
        public User User { get; set; }
        public VideoCommentViewModel()
        {
            VideoComments = new ObservableCollection<VideoComment>();
            VideoComment = new VideoComment();
        }

        public async Task LoadCurrentVideoCommentAsync(int VideoID)
        {

            List<VideoComment> list = await App.UnitOfWork.VideoCommentRepository.FindVideoCommentAsync(VideoID);
            // Comments = new ObservableCollection<Comment>(list);
            VideoComments.Clear();
            foreach (VideoComment e in list)
            {
                VideoComments.Add(e);
            }


        }
        public async Task LoadAllAsync()
        {

            List<VideoComment> list = await App.UnitOfWork.VideoCommentRepository.FindAllAsync();

            VideoComments.Clear();
            foreach (VideoComment e in list)
            {
                VideoComments.Add(e);
            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.VideoCommentRepository.CreateAsync(VideoComment);
        }

    }





}

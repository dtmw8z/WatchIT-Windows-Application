using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel
{
   public class MusicCommentViewModel
    {
        public ObservableCollection<MusicComment> MusicComments { get; set; }
        public MusicComment MusicComment { get; set; }
        public User User { get; set; }
        public MusicCommentViewModel()
        {
            MusicComments = new ObservableCollection<MusicComment>();
            MusicComment = new MusicComment();
        }

        public async Task LoadCurrentMusicCommentAsync(int musicID)
        {

            List<MusicComment> list = await App.UnitOfWork.MusicCommentRepository.FindMusicCommentAsync(musicID);
            // Comments = new ObservableCollection<Comment>(list);
            MusicComments.Clear();
            foreach (MusicComment e in list)
            {
                MusicComments.Add(e);
            }


        }
        public async Task LoadAllAsync()
        {

            List<MusicComment> list = await App.UnitOfWork.MusicCommentRepository.FindAllAsync();

            MusicComments.Clear();
            foreach (MusicComment e in list)
            {
                MusicComments.Add(e);
            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.MusicCommentRepository.CreateAsync(MusicComment);
        }

    }








   

        
    
}

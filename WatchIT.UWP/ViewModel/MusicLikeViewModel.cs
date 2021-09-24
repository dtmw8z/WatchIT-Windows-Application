using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel
{
    public class MusicLikeViewModel
    {
        public ObservableCollection<MusicLike> MusicLikes { get; set; }

        public MusicLike MusicLike { get; set; }

        public MusicLikeViewModel()
        {
            MusicLikes = new ObservableCollection<MusicLike>();
            MusicLike = new MusicLike();
        }

        public async Task LoadAllAsync()
        {

            List<MusicLike> list = await App.UnitOfWork.MusicLikeRepository.FindAllAsync();
            // Likes = new ObservableCollection<Like>(list);
            MusicLikes.Clear();
            foreach (MusicLike e in list)
            {
                MusicLikes.Add(e);
            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.MusicLikeRepository.CreateAsync(MusicLike);
        }

      
        internal async Task DeleteAsync(MusicLike musicLike)
        {
            await App.UnitOfWork.MusicLikeRepository.DeleteAsync(musicLike);
            MusicLikes.Remove(musicLike);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel
{
    public class VideoPlaylistsViewModel
    {
        public VideoPlaylist videoPlaylist { get; set; }
        public ObservableCollection<VideoPlaylist> videoPlaylists { get; set; }

        public VideoPlaylistsViewModel()
        {
            videoPlaylist = new VideoPlaylist();

        }

        internal async Task UpsertAsync(VideoPlaylist mp)
        {
            mp.Video = await App.UnitOfWork.VideoRepository.FindByIdAsync(mp.videoId);
            mp.Playlist = await App.UnitOfWork.PlaylistRepository.FindByIdAsync(mp.playlistId);

            await App.UnitOfWork.VideoPlaylistRepository.UpsertAsync(mp);
        }


        internal async Task DeleteAsync()
        {
            await App.UnitOfWork.VideoPlaylistRepository.DeleteAsync(videoPlaylist);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIT.Domain.Model;
using Windows.Storage;
using Windows.Storage.Streams;

namespace WatchIT.UWP.ViewModel
{
   public class MusicViewModel
    {
        public ObservableCollection<Music> Musics { get; set; }
        public ObservableCollection<MusicComment> Comments { get; set; }

        public Music Music { get; set; }


        public MusicViewModel()
        {
            Musics = new ObservableCollection<Music>();
            Comments = new ObservableCollection<MusicComment>();
            Music = new Music();
        }

        public async Task LoadAllAsync()
        {

            List<Music> list = await App.UnitOfWork.MusicRepository.FindAllAsync();
            // Musics = new ObservableCollection<Music>(list);
            Musics.Clear();
            foreach (Music e in list)
            {
                e.Channel = await App.UnitOfWork.ChannelRepository.FindByIdAsync(e.channelId);
                e.MusicLikes = await App.UnitOfWork.MusicLikeRepository.FindMusicLikeAsync(e.Id);
                e.MusicComments = await App.UnitOfWork.MusicCommentRepository.FindMusicCommentAsync(e.Id);


                Musics.Add(e);
            }

            foreach(Music m in Musics)
            {
                m.Channel = await App.UnitOfWork.ChannelRepository.FindByIdAsync(m.channelId);
                m.MusicComments = (await App.UnitOfWork.MusicCommentRepository.FindMusicCommentAsync(m.Id)).ToList();
                m.MusicLikes = (await App.UnitOfWork.MusicLikeRepository.FindMusicLikeAsync(m.Id)).ToList();
                foreach (MusicLike mc in m.MusicLikes)
                {
                    mc.Music = await App.UnitOfWork.MusicRepository.FindByIdAsync(mc.musicId);
                    mc.User = await App.UnitOfWork.UserRepository.FindByIdAsync(mc.userId);
                }

                foreach (MusicComment mc in m.MusicComments)
                {
                    mc.Music = await App.UnitOfWork.MusicRepository.FindByIdAsync(mc.musicId);
                    mc.User = await App.UnitOfWork.UserRepository.FindByIdAsync(mc.userId);
                }
            }
        }

        public async Task LoadMusicinPlaylistAsync(int playlistID)
        {
            List<Music> music = await App.UnitOfWork.MusicRepository.FindMusicsInPlaylistAsync(playlistID);
            Musics.Clear();
            foreach (Music e in music)
            {
                Musics.Add(e);

            }
        }

        internal async Task InsertAsync()
        {
            await App.UnitOfWork.MusicRepository.CreateAsync(Music);
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

        internal async Task DeleteAsync(Music music)
        {
            await App.UnitOfWork.MusicRepository.RemoveMusicAsync(music);
            Musics.Remove(music);
        }

        public int GetCurrentMusicID()
        {
            object currmusicID = ApplicationData.Current.LocalSettings.Values["CurrentMusicID"];
            int CurrentMusicID = (int)currmusicID;

            return CurrentMusicID;
        }

        internal async Task UpsertAsync(Music m)
        {
            await App.UnitOfWork.MusicRepository.UpsertAsync(m);
        }
    }
}

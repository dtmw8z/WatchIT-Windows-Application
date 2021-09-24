using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.Repositories;

namespace WatchIT.Domain
{
    public interface IUnitOfWork
    {
        IChannelRepository ChannelRepository { get; }

        IUserRepository UserRepository { get; }

        IMusicCommentRepository MusicCommentRepository { get; }
        IVideoCommentRepository VideoCommentRepository { get; }

       
        IMusicLikeRepository MusicLikeRepository { get; }
        IVideoLikeRepository VideoLikeRepository { get; }

        IMusicPlaylistRepository MusicPlaylistRepository { get; }

        IMusicRepository MusicRepository { get; }

        IPlaylistRepository PlaylistRepository { get; }

        IUserSubscribesChannelRepository UserSubscribesChannelRepository { get; }

        IVideoPlaylistRepository VideoPlaylistRepository { get; }

        IVideoRepository VideoRepository { get; }

    }
}

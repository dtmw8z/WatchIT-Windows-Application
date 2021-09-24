using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain;
using WatchIT.Domain.Repositories;
using WatchIT.Infrastructure.Repositories;

namespace WatchIT.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContextOptions Options { get; set; }

        public IUserRepository UserRepository => new UserRepository(new WatchitDbContext(Options));

        public IChannelRepository ChannelRepository => new ChannelRepository(new WatchitDbContext(Options));

        public IMusicCommentRepository MusicCommentRepository => new MusicCommentRepository(new WatchitDbContext(Options));
        public IVideoCommentRepository VideoCommentRepository => new VideoCommentRepository(new WatchitDbContext(Options));

       
        public IMusicLikeRepository MusicLikeRepository => new MusicLikeRepository(new WatchitDbContext(Options));
        public IVideoLikeRepository VideoLikeRepository => new VideoLikeRepository(new WatchitDbContext(Options));
        public IMusicPlaylistRepository MusicPlaylistRepository 
                              => new MusicPlaylistRepository(new WatchitDbContext(Options));

        public IMusicRepository MusicRepository => new MusicRepository(new WatchitDbContext(Options));

        public IPlaylistRepository PlaylistRepository => new PlaylistRepository(new WatchitDbContext(Options));
        public IUserSubscribesChannelRepository UserSubscribesChannelRepository
                               => new UserSubscribesChannelRepository(new WatchitDbContext(Options));

        public IVideoPlaylistRepository VideoPlaylistRepository 
                              => new VideoPlaylistRepository(new WatchitDbContext(Options));

        public IVideoRepository VideoRepository => new VideoRepository(new WatchitDbContext(Options));

        public UnitOfWork(DbContextOptions<WatchitDbContext> options)
        {
            Options = options;

            WatchitDbContext db = new WatchitDbContext(options);
            db.Database.Migrate();
        }

    }
}

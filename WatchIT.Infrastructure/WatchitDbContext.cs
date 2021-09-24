using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.Model;

namespace WatchIT.Infrastructure
{
    public class WatchitDbContext : DbContext
    {
        public WatchitDbContext(DbContextOptions options) : base(options)
        {
        }

        public WatchitDbContext()
        {

        }

        public DbSet<Channel> channels {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=watch-it; Integrated Security = True; Connect Timeout = 30;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MusicPlaylist>().HasKey(mp => new { mp.musicId, mp.playlistId });
            modelBuilder.Entity<VideoPlaylist>().HasKey(vp => new { vp.videoId, vp.playlistId });
            modelBuilder.Entity<UserSubscribesChannel>().HasKey(usc => new { usc.userId, usc.channelId });

            modelBuilder.Entity<Video>().HasOne<Channel>(c => c.Channel).WithMany(v => v.Videos).HasForeignKey(fk => fk.channelId).OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<Music>().HasOne<Channel>(c => c.Channel).WithMany(m => m.Musics).HasForeignKey(fk => fk.channelId).OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<VideoLike>().HasOne<Video>(v => v.Video).WithMany(l => l.VideoLikes).HasForeignKey(fk => fk.videoId).OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<VideoComment>().HasOne<Video>(v => v.Video).WithMany(c => c.VideoComments).HasForeignKey(fk => fk.videoId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MusicLike>().HasOne<Music>(m => m.Music).WithMany(l => l.MusicLikes).HasForeignKey(fk => fk.musicId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MusicComment>().HasOne<Music>(m => m.Music).WithMany(c => c.MusicComments).HasForeignKey(fk => fk.musicId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<VideoLike>().HasOne<User>(u => u.User).WithMany(l => l.VideoLikes).HasForeignKey(fk => fk.userId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MusicLike>().HasOne<User>(u => u.User).WithMany(l => l.MusicLikes).HasForeignKey(fk => fk.userId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<VideoComment>().HasOne<User>(u => u.User).WithMany(c => c.VideoComments).HasForeignKey(fk => fk.userId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MusicComment>().HasOne<User>(u => u.User).WithMany(c => c.MusicComments).HasForeignKey(fk => fk.userId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Playlist>().HasOne<User>(u => u.User).WithMany(p => p.Playlists).HasForeignKey(fk => fk.userId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasOne(c => c.Channel).WithOne(u => u.User).HasForeignKey<Channel>(fk => fk.userId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>().HasIndex(p =>  p.Email ).IsUnique();

            //User Table

            var u = new User { Id = 1, fullName = "Susan Babu Pandey", Password = "E/XH1MpKAjCSH7uWc+iXM7MvyQVmDDqjdQs8Db+OWW89lX16OrRH8FkIcl1Lr/4UzztdfdRgeKTjl4MlKVgPNg==", IsAdmin = true, Email="dtmw8z.sp@gmail.com"};
            var u1 = new User { Id = 2, fullName = "Shailab Chapagain", Password = "E/XH1MpKAjCSH7uWc+iXM7MvyQVmDDqjdQs8Db+OWW89lX16OrRH8FkIcl1Lr/4UzztdfdRgeKTjl4MlKVgPNg==", IsAdmin = false, Email = "shailabchapagain34@gmail.com@gmail.com" };
            modelBuilder.Entity<User>().HasData(u);
            modelBuilder.Entity<User>().HasData(u1);


            //Channel Table
            var c = new Channel { Id = 1, channelName = "Satyamev Jayate", channelDescription = "Satyamev Jayate 1.1M subscribers Description Satyamev Jayate - a TV show hosted by Aamir Khan that discussed social issues in India through three seasons that went on air between 2012 and 2014.Starting early 2016,the shows core team runs Paani Foundation.",userId=2 };
            var c1 = new Channel { Id = 2, channelName = "Bollywood Spy", channelDescription = "Wouldn’t it be just great to keep pace with what your stars are doing, never leaving you in a dull moment? Bollywood Spy.in is here to be your entertainment and B Town partner. Look nowhere, go nowhere, simply catch up with us for fresh feeds.",userId=1 };
            modelBuilder.Entity<Channel>().HasData(c);
            modelBuilder.Entity<Channel>().HasData(c1);  








        }

        public DbSet<Music> Musics { get; set; }        
        public DbSet<MusicPlaylist> MusicPlaylists { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoPlaylist> VideoPlaylists { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<UserSubscribesChannel> UserSubscribesChannels { get; set; }
        public DbSet<MusicLike> MusicLikes { get; set; }
        public DbSet<VideoLike> VideoLikes { get; set; }
        public DbSet<MusicComment> MusicComments { get; set; }
        public DbSet<VideoComment> VideoComments { get; set; }








    }
}

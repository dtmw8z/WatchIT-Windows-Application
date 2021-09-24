using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchIT.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    channelName = table.Column<string>(nullable: true),
                    channelDescription = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channel_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    playlistName = table.Column<string>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<byte[]>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<byte[]>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    channelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musics_Channel_channelId",
                        column: x => x.channelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscribesChannels",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false),
                    channelId = table.Column<int>(nullable: false),
                    subscribedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscribesChannels", x => new { x.userId, x.channelId });
                    table.ForeignKey(
                        name: "FK_UserSubscribesChannels_Channel_channelId",
                        column: x => x.channelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscribesChannels_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<byte[]>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<byte[]>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    channelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Channel_channelId",
                        column: x => x.channelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    commentText = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    musicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicComments_Musics_musicId",
                        column: x => x.musicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicComments_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MusicLikes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    musicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicLikes_Musics_musicId",
                        column: x => x.musicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicLikes_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MusicPlaylists",
                columns: table => new
                {
                    musicId = table.Column<int>(nullable: false),
                    playlistId = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPlaylists", x => new { x.musicId, x.playlistId });
                    table.ForeignKey(
                        name: "FK_MusicPlaylists_Musics_musicId",
                        column: x => x.musicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicPlaylists_Playlists_playlistId",
                        column: x => x.playlistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    commentText = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    videoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoComments_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoComments_Videos_videoId",
                        column: x => x.videoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoLikes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    videoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoLikes_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoLikes_Videos_videoId",
                        column: x => x.videoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoPlaylists",
                columns: table => new
                {
                    videoId = table.Column<int>(nullable: false),
                    playlistId = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoPlaylists", x => new { x.videoId, x.playlistId });
                    table.ForeignKey(
                        name: "FK_VideoPlaylists_Playlists_playlistId",
                        column: x => x.playlistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoPlaylists_Videos_videoId",
                        column: x => x.videoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Password", "fullName" },
                values: new object[] { 1, "dtmw8z.sp@gmail.com", true, "E/XH1MpKAjCSH7uWc+iXM7MvyQVmDDqjdQs8Db+OWW89lX16OrRH8FkIcl1Lr/4UzztdfdRgeKTjl4MlKVgPNg==", "Susan Babu Pandey" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Password", "fullName" },
                values: new object[] { 2, "shailabchapagain34@gmail.com@gmail.com", false, "E/XH1MpKAjCSH7uWc+iXM7MvyQVmDDqjdQs8Db+OWW89lX16OrRH8FkIcl1Lr/4UzztdfdRgeKTjl4MlKVgPNg==", "Shailab Chapagain" });

            migrationBuilder.InsertData(
                table: "Channel",
                columns: new[] { "Id", "channelDescription", "channelName", "created_at", "updated_at", "userId" },
                values: new object[] { 2, "Wouldn’t it be just great to keep pace with what your stars are doing, never leaving you in a dull moment? Bollywood Spy.in is here to be your entertainment and B Town partner. Look nowhere, go nowhere, simply catch up with us for fresh feeds.", "Bollywood Spy", new DateTime(2021, 1, 29, 23, 35, 6, 7, DateTimeKind.Utc).AddTicks(1375), new DateTime(2021, 1, 29, 23, 35, 6, 7, DateTimeKind.Utc).AddTicks(1412), 1 });

            migrationBuilder.InsertData(
                table: "Channel",
                columns: new[] { "Id", "channelDescription", "channelName", "created_at", "updated_at", "userId" },
                values: new object[] { 1, "Satyamev Jayate 1.1M subscribers Description Satyamev Jayate - a TV show hosted by Aamir Khan that discussed social issues in India through three seasons that went on air between 2012 and 2014.Starting early 2016,the shows core team runs Paani Foundation.", "Satyamev Jayate", new DateTime(2021, 1, 29, 23, 35, 6, 6, DateTimeKind.Utc).AddTicks(6473), new DateTime(2021, 1, 29, 23, 35, 6, 6, DateTimeKind.Utc).AddTicks(7612), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Channel_userId",
                table: "Channel",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusicComments_musicId",
                table: "MusicComments",
                column: "musicId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicComments_userId",
                table: "MusicComments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicLikes_musicId",
                table: "MusicLikes",
                column: "musicId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicLikes_userId",
                table: "MusicLikes",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlaylists_playlistId",
                table: "MusicPlaylists",
                column: "playlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Musics_channelId",
                table: "Musics",
                column: "channelId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_userId",
                table: "Playlists",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscribesChannels_channelId",
                table: "UserSubscribesChannels",
                column: "channelId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoComments_userId",
                table: "VideoComments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoComments_videoId",
                table: "VideoComments",
                column: "videoId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoLikes_userId",
                table: "VideoLikes",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoLikes_videoId",
                table: "VideoLikes",
                column: "videoId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoPlaylists_playlistId",
                table: "VideoPlaylists",
                column: "playlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_channelId",
                table: "Videos",
                column: "channelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicComments");

            migrationBuilder.DropTable(
                name: "MusicLikes");

            migrationBuilder.DropTable(
                name: "MusicPlaylists");

            migrationBuilder.DropTable(
                name: "UserSubscribesChannels");

            migrationBuilder.DropTable(
                name: "VideoComments");

            migrationBuilder.DropTable(
                name: "VideoLikes");

            migrationBuilder.DropTable(
                name: "VideoPlaylists");

            migrationBuilder.DropTable(
                name: "Musics");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

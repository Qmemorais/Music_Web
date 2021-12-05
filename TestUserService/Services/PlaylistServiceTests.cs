using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Moq;
using System.Collections.Generic;
using DataLayer.Models;
using AutoFixture;
using System;
using BusinessLayer.Models;

namespace BusinessLayer.Services.Tests
{
    [TestClass()]
    public class PlaylistServiceTests
    {
        private Mock<MusicContext> context;

        private Mock<IMapper> mapper;
        private PlaylistService service;
        private Fixture fixture;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            context = new Mock<MusicContext>();
            mapper = new Mock<IMapper>();

            service = new PlaylistService(context.Object, mapper.Object);
        }

        [TestMethod()]
        public void AddSongToPlaylistTest_WithExistSongAndPlaylist()
        {
            //arange
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var DbSetSong = CreateDbSetMock(song);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            service.AddSongToPlaylist(playlistId,songId);
            //assert
            context.Verify(x => x.SaveChanges());
        }
        [TestMethod()]
        public void AddSongToPlaylistTest_WithUnexistSong()
        {
            // arange
            var songId = fixture.Create<int>();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            //act
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.AddSongToPlaylist(playlistId,songId));
        }
        [TestMethod()]
        public void AddSongToPlaylistTest_WithUnexistPlaylist()
        {
            // arange
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();

            var DbSetSong = CreateDbSetMock(song);

            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.AddSongToPlaylist(playlistId, songId));
        }
        [TestMethod()]
        public void AddSongToPlaylistTest_WichAlreadyExist()
        {
            //arange
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();

            var playlistId = song[0].Playlists.First().Id;
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .With(x => x.Songs, song)
                .CreateMany(1)
                .ToList();

            var DbSetSong = CreateDbSetMock(song);
            var DbSetPlaylist = CreateDbSetMock(playlist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //assert
            service.AddSongToPlaylist(playlistId,songId);
            context.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod()]
        public void CreatePlaylistTest_WithExistName_ReturnNotVerify()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();
            var userId = playlist[0].Users[0].Id;
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistToCreate = playlist.Select(playlistCreateDTO => fixture.Build<PlaylistCreateDto>()
                            .With(x => x.Name, playlistCreateDTO.Name)
                            .With(x => x.UserId, playlistCreateDTO.Users[0].Id)
                            .Create()).First();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var DbSetUser = CreateDbSetMock(user);

            mapper.Setup(mapper => mapper.Map<Playlist>(playlistToCreate)).Returns(playlist.First());
            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //assert 
            service.CreatePlaylist(playlistToCreate);
            context.Verify(x => x.SaveChanges(), Times.Never());
        }
        [TestMethod()]
        public void CreatePlaylistTest_WithUnexistName_ReturnCreated()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistToCreateNew = fixture.Create<PlaylistCreateDto>();
            playlistToCreateNew.UserId = userId;
            var playlistModel = fixture.Build<Playlist>()
                    .With(x => x.Name, playlistToCreateNew.Name).Create();

            var DbSetUser = CreateDbSetMock(user);
            var DbSetPlaylist = CreateDbSetMock(playlist);

            mapper.Setup(mapper => mapper.Map<Playlist>(playlistToCreateNew)).Returns(playlistModel);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            //act
            service.CreatePlaylist(playlistToCreateNew);
            //assert 
            context.Verify(x => x.SaveChanges());
        }
        [TestMethod()]
        public void CreatePlaylistTest_WithUnexistUser_ReturnException()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var playlistToCreate = playlist.Select(playlistCreateDTO => fixture.Build<PlaylistCreateDto>()
                            .With(x => x.Name, playlistCreateDTO.Name)
                            .With(x => x.UserId, playlistCreateDTO.Users[0].Id)
                            .Create()).First();

            var DbSetPlaylist = CreateDbSetMock(playlist);

            mapper.Setup(mapper => mapper.Map<Playlist>(playlistToCreate)).Returns(playlist.First());
            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);

            Assert.ThrowsException<ArgumentNullException>(() => service.CreatePlaylist(playlistToCreate));
        }

        [TestMethod()]
        public void DeletePlaylistTest_WithExistId_ReturnTrue()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            //act
            service.DeletePlaylist(playlistId);
            //assert 
            context.Verify(x => x.SaveChanges());
        }
        [TestMethod()]
        public void DeletePlaylistTest_WithUnexistId_ReturnException()
        {
            //arange
            var playlistId = fixture.Create<int>();

            context.Setup(x => x.Playlists).Returns((DbSet<Playlist>)null);
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.DeletePlaylist(playlistId));
        }

        [TestMethod()]
        public void GetAllPlaylistsTest_WithExistModels_ReturnAllPlaylists()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);

            var mappedPlaylist = playlist.Select(playlistDTO => fixture.Build<PlaylistDto>()
                            .With(x => x.Name, playlistDTO.Name)
                            .Create());

            mapper.Setup(mapper => mapper.Map<IEnumerable<PlaylistDto>>(playlist)).Returns(mappedPlaylist);
            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            //act
            var playlists = service.GetAllPlaylists();
            //assert
            Assert.IsInstanceOfType(playlists, typeof(IEnumerable<PlaylistDto>));
            Assert.IsNotNull(playlists);
            Assert.AreEqual(mappedPlaylist.Count(), playlists.Count());
            Assert.AreEqual(mappedPlaylist.ElementAt(0).Name, playlists.ElementAt(0).Name);
        }
        [TestMethod()]
        public void GetAllPlaylistsTest_WithunexistModels_ReturnException()
        {
            context.Setup(x => x.Playlists).Returns((DbSet<Playlist>)null);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllPlaylists());
        }

        [TestMethod()]
        public void GetAllPlaylistsBySongTest_WithExistSong_ReturnAllPlaylists()
        {
            //arange
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .With(x => x.Songs, song)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var DbSetSong = CreateDbSetMock(song);

            var mappedPlaylist = playlist.Select(playlistDTO => fixture.Build<PlaylistDto>()
                            .With(x => x.Name, playlistDTO.Name)
                            .Create());

            mapper.Setup(mapper => mapper.Map<IEnumerable<PlaylistDto>>(playlist)).Returns(mappedPlaylist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            var result = service.GetAllPlaylistsBySong(songId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<PlaylistDto>));
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedPlaylist.Count(), result.Count());
            Assert.AreEqual(mappedPlaylist.ElementAt(0).Name, result.ElementAt(0).Name);
        }
        [TestMethod()]
        public void GetAllPlaylistsBySongTest_WithUnexistSong_ReturnException()
        {
            var songId = fixture.Create<int>();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var mappedPlaylist = playlist.Select(playlistDTO => fixture.Build<PlaylistDto>()
                            .With(x => x.Name, playlistDTO.Name)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<PlaylistDto>>(playlist)).Returns(mappedPlaylist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllPlaylistsBySong(songId));
        }
        [TestMethod()]
        public void GetAllPlaylistsBySongTest_WithUnexistPlaylist_ReturnException()
        {
            //arange
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();

            var DbSetSong = CreateDbSetMock(song);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllPlaylistsBySong(songId));
        }

        [TestMethod()]
        public void GetAllPlaylistsByUserTest_WithExistUser_ReturnAllPlaylists()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .With(x => x.Users, user)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var DbSetUser = CreateDbSetMock(user);

            var mappedPlaylist = playlist.Select(playlistDTO => fixture.Build<PlaylistDto>()
                            .With(x => x.Name, playlistDTO.Name)
                            .Create());

            mapper.Setup(mapper => mapper.Map<IEnumerable<PlaylistDto>>(playlist)).Returns(mappedPlaylist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            var result = service.GetAllPlaylistsByUser(userId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<PlaylistDto>));
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedPlaylist.Count(), result.Count());
            Assert.AreEqual(mappedPlaylist.ElementAt(0).Name, result.ElementAt(0).Name);
        }
        [TestMethod()]
        public void GetAllPlaylistsByUserTest_WithUnexistUser_ReturnException()
        {
            var userId = fixture.Create<int>();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var mappedPlaylist = playlist.Select(playlistDTO => fixture.Build<PlaylistDto>()
                            .With(x => x.Name, playlistDTO.Name)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<PlaylistDto>>(playlist)).Returns(mappedPlaylist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllPlaylistsByUser(userId));
        }
        [TestMethod()]
        public void GetAllPlaylistsByUserTest_WithUnexistPlaylist_ReturnException()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var DbSetUser = CreateDbSetMock(user);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllPlaylistsByUser(userId));
        }

        [TestMethod()]
        public void GetPlaylistTest_WithUnexistId_ReturnUnexistModel()
        {
            //act
            var playlistId = fixture.Create<int>();

            context.Setup(x => x.Playlists).Returns((DbSet<Playlist>)null);
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.GetPlaylist(playlistId));
        }
        [TestMethod()]
        public void GetPlaylistTest_WithExistId_ReturnModel()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var mappedPlaylist = playlist.Select(userDTO => fixture.Build<PlaylistDto>()
                            .With(x => x.Name, userDTO.Name)
                            .Create()).First();

            var DbSetUser = CreateDbSetMock(playlist);

            mapper.Setup(mapper => mapper.Map<PlaylistDto>(playlist.First())).Returns(mappedPlaylist);

            context.Setup(x => x.Playlists).Returns(DbSetUser.Object);
            //act
            var result = service.GetPlaylist(playlistId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(PlaylistDto));
            Assert.AreEqual(mappedPlaylist.Name, result.Name);
        }

        [TestMethod()]
        public void UpdatePlaylistTest_WithExistId_ReturnTrue()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();
            var mappedPlaylistUpdate = playlist.Select(userDTO => fixture.Build<PlaylistUpdateDto>()
                            .With(x => x.Name, userDTO.Name)
                            .Create()).First();

            var DbSetPlaylist = CreateDbSetMock(playlist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            //act
            service.UpdatePlaylist(playlistId, mappedPlaylistUpdate);
            //assert 
            context.Verify(x => x.SaveChanges());
        }
        [TestMethod()]
        public void UpdatePlaylistTest_WithUnexistId_ReturnFalse()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();
            var mappedPlaylistUpdate = playlist.Select(userDTO => fixture.Build<PlaylistUpdateDto>()
                            .With(x => x.Name, userDTO.Name)
                            .Create()).First();
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.UpdatePlaylist(playlistId, mappedPlaylistUpdate));
        }

        private Mock<DbSet<T>> CreateDbSetMock<T>(List<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(s => elements.Add(s));

            return dbSetMock;
        }
    }
}
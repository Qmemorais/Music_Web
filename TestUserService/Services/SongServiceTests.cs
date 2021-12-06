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
    public class SongServiceTests
    {
        private Mock<MusicContext> context;

        private Mock<IMapper> mapper;
        private SongService service;
        private Fixture fixture;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            context = new Mock<MusicContext>();
            mapper = new Mock<IMapper>();

            service = new SongService(context.Object, mapper.Object);
        }

        [TestMethod()]
        public void CreateSongTest_WithExistSong_ReturnNotVerify()
        {
            //arange
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();
            var songToCreate = song.Select(songCreateDTO => fixture.Build<SongCreateDto>()
                            .With(x => x.Name, songCreateDTO.Name)
                            .With(x => x.ArtistId, songCreateDTO.ArtistId)
                            .With(x => x.AlbumId, songCreateDTO.AlbumId)
                            .With(x => x.Time, songCreateDTO.Time)
                            .Create()).First();

            var DbSetSong = CreateDbSetMock(song);

            mapper.Setup(mapper => mapper.Map<Song>(songToCreate)).Returns(song.First());
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //assert 
            service.CreateSong(songToCreate);
            context.Verify(x => x.SaveChanges(), Times.Never());
        }
        [TestMethod()]
        public void CreateSongTest_WithUnexistSong_ReturnCreated()
        {
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();
            var songToCreate = fixture.Create<SongCreateDto>();
            var songModel = fixture.Build<Song>()
                            .With(x => x.Name, songToCreate.Name)
                            .With(x => x.ArtistId, songToCreate.ArtistId)
                            .With(x => x.AlbumId, songToCreate.AlbumId)
                            .With(x => x.Time, songToCreate.Time)
                            .Create();

            var DbSetSong = CreateDbSetMock(song);

            mapper.Setup(mapper => mapper.Map<Song>(songToCreate)).Returns(songModel);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            service.CreateSong(songToCreate);
            //assert 
            context.Verify(x => x.SaveChanges());
        }

        [TestMethod()]
        public void DeleteSongTest_WithUnexistId_ReturnException()
        {
            //arange
            var songId = fixture.Create<int>();

            context.Setup(x => x.Songs).Returns((DbSet<Song>)null);
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.DeleteSong(songId));
        }
        [TestMethod()]
        public void DeleteSongTest_WithExistId_ReturnTrue()
        {
            //arange
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();

            var DbSetSong = CreateDbSetMock(song);

            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            service.DeleteSong(songId);
            //assert 
            context.Verify(x => x.SaveChanges());
        }

        [TestMethod()]
        public void GetAllSongsByPlaylistTest_WithExistPlaylist_ReturnAllSongs()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();

            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .With(x => x.Playlists, playlist)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var DbSetSong = CreateDbSetMock(song);

            var mappedSong = song.Select(songDTO => fixture.Build<SongDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create());

            mapper.Setup(mapper => mapper.Map<IEnumerable<SongDto>>(song)).Returns(mappedSong);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            var result = service.GetAllSongsByPlaylist(playlistId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<SongDto>));
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedSong.Count(), result.Count());
            Assert.AreEqual(mappedSong.ElementAt(0).Name, result.ElementAt(0).Name);
        }
        [TestMethod()]
        public void GetAllSongsByPlaylistTest_WithUnexistPlaylist_ReturnException()
        {
            var playlistId = fixture.Create<int>();
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();

            var DbSetSong = CreateDbSetMock(song);

            var mappedSong = song.Select(songDTO => fixture.Build<SongDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create());
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllSongsByPlaylist(playlistId));
        }
        [TestMethod()]
        public void GetAllSongsByPlaylistTest_WithUnexistSong_ReturnException()
        {
            //arange
            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .CreateMany(1)
                .ToList();
            var DbSetPlaylist = CreateDbSetMock(playlist);
            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllSongsByPlaylist(playlistId));
        }

        [TestMethod()]
        public void GetAllSongsTest_WithunexistModels_ReturnException()
        {
            context.Setup(x => x.Songs).Returns((DbSet<Song>)null);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllSongs());
        }
        [TestMethod()]
        public void GetAllSongsTest_WithExistModels_ReturnList()
        {
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();

            var DbSetSong = CreateDbSetMock(song);

            var mappedSong = song.Select(songDTO => fixture.Build<SongDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<SongDto>>(song)).Returns(mappedSong);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            var result = service.GetAllSongs();
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<SongDto>));
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedSong.Count(), result.Count());
            Assert.AreEqual(mappedSong.ElementAt(0).Name, result.ElementAt(0).Name);
        }

        [TestMethod()]
        public void GetSongByIdTest_WithUnexistId_ReturnUnexistModel()
        {
            var songId = fixture.Create<int>();
            context.Setup(x => x.Songs).Returns((DbSet<Song>)null);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetSongById(songId));
        }
        [TestMethod()]
        public void GetSongByIdTest_WithExistId_ReturnModel()
        {
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();
            var DbSetSong = CreateDbSetMock(song);

            var mappedSong = song.Select(songDTO => fixture.Build<SongDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create()).First();
            mapper.Setup(mapper => mapper.Map<SongDto>(song.First())).Returns(mappedSong);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            var result = service.GetSongById(songId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(SongDto));
            Assert.AreEqual(mappedSong.Name, result.Name);
        }

        [TestMethod()]
        public void UpdateSongTest_WithExistId_ReturnTrue()
        {
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();
            var mappedSong = song.Select(songDTO => fixture.Build<SongUpdateDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create()).First();
            var DbSetSong = CreateDbSetMock(song);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            service.UpdateSong(songId, mappedSong);
            //assert 
            context.Verify(x => x.SaveChanges());
        }
        [TestMethod()]
        public void UpdateSongTest_WithUnexistId_ReturnFalse()
        {
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();
            var mappedSong = song.Select(songDTO => fixture.Build<SongUpdateDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create()).First();
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.UpdateSong(songId, mappedSong));
        }

        [TestMethod()]
        public void GetAllSongsByArtistTest_WithExistArtist_ReturnAllSongs()
        {
            var artistId = fixture.Create<int>();
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .With(x => x.ArtistId, artistId)
                .CreateMany(1)
                .ToList();
            var DbSetSong = CreateDbSetMock(song);
            var mappedSong = song.Select(songDTO => fixture.Build<SongDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<SongDto>>(song)).Returns(mappedSong);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            var result = service.GetAllSongsByArtist(artistId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<SongDto>));
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedSong.Count(), result.Count());
            Assert.AreEqual(mappedSong.ElementAt(0).Name, result.ElementAt(0).Name);
        }
        [TestMethod()]
        public void GetAllSongsByArtistTest_WithUnexistArtist_ReturnNull()
        {
            var artistId = fixture.Create<int>();
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();
            var DbSetSong = CreateDbSetMock(song);
            var mappedSong = song.Select(songDTO => fixture.Build<SongDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<SongDto>>(song)).Returns(mappedSong);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            var result = service.GetAllSongsByArtist(artistId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<SongDto>));
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public void GetAllSongsByAlbumTest_WithExistAlbum_ReturnList()
        {
            var albumId = fixture.Create<int>();
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .With(x => x.AlbumId, albumId)
                .CreateMany(1)
                .ToList();
            var DbSetSong = CreateDbSetMock(song);
            var mappedSong = song.Select(songDTO => fixture.Build<SongDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<SongDto>>(song)).Returns(mappedSong);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            var result = service.GetAllSongsByAlbum(albumId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<SongDto>));
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedSong.Count(), result.Count());
            Assert.AreEqual(mappedSong.ElementAt(0).Name, result.ElementAt(0).Name);
        }
        [TestMethod()]
        public void GetAllSongsByAlbumTest_WithUnexistAlbum_ReturnNull()
        {
            var albumId = fixture.Create<int>();
            var songId = fixture.Create<int>();
            var song = fixture.Build<Song>()
                .With(x => x.Id, songId)
                .CreateMany(1)
                .ToList();
            var DbSetSong = CreateDbSetMock(song);
            var mappedSong = song.Select(songDTO => fixture.Build<SongDto>()
                            .With(x => x.Name, songDTO.Name)
                            .With(x => x.ArtistId, songDTO.ArtistId)
                            .With(x => x.AlbumId, songDTO.AlbumId)
                            .With(x => x.Time, songDTO.Time)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<SongDto>>(song)).Returns(mappedSong);
            context.Setup(x => x.Songs).Returns(DbSetSong.Object);
            //act
            var result = service.GetAllSongsByAlbum(albumId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<SongDto>));
            Assert.AreEqual(0, result.Count());
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
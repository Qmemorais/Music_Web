using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoFixture;
using Moq;
using AutoMapper;
using BusinessLayer.Services.Interface;
using System.Linq;
using BusinessLayer.Models;
using Web_Music.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Web_Music.Controllers.Tests
{
    [TestClass()]
    public class PlaylistSongControllerTests
    {
        private Mock<ISongService> mockSongService;
        private Mock<IPlaylistService> mockPlaylistService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private PlaylistSongController controller;

        private readonly int have = 1, no = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockPlaylistService = new Mock<IPlaylistService>();
            mockSongService = new Mock<ISongService>();
            mapper = new Mock<IMapper>();

            controller = new PlaylistSongController(mockSongService.Object, mockPlaylistService.Object, mapper.Object);
        }

        [TestMethod()]
        public void GetAllSongsByPlaylistTest_WithExistPlaylistAndSongs_ReturnList()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x => x.Name, songDto.Name)
                .With(x => x.Time, songDto.Time)
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .Create());
            var playlist = fixture.Create<PlaylistDto>();

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockPlaylistService.Setup(service => service.GetPlaylist(have)).Returns(playlist);
            mockSongService.Setup(service => service.GetAllSongsByPlaylist(have)).Returns(songs);

            //act
            var result = controller.GetAllSongsByPlaylist(have) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(songsResponse,responseModel);
        }
        [TestMethod()]
        public void GetAllSongsByPlaylistTest_WithUnexistPlaylist_ReturnNotFound()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x => x.Name, songDto.Name)
                .With(x => x.Time, songDto.Time)
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .Create());

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockPlaylistService.Setup(service => service.GetPlaylist(no)).Returns((PlaylistDto)null);
            mockSongService.Setup(service => service.GetAllSongsByPlaylist(no)).Returns(songs);
            //act
            var result = controller.GetAllSongsByPlaylist(no);
            //assert
            Assert.IsInstanceOfType(result,typeof(NotFoundResult));
        }
        [TestMethod()]
        public void GetAllSongsByPlaylistTest_WithUnexistSongs_ReturnNotFound()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x => x.Name, songDto.Name)
                .With(x => x.Time, songDto.Time)
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .Create());
            var playlist = fixture.Create<PlaylistDto>();

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockPlaylistService.Setup(service => service.GetPlaylist(have)).Returns(playlist);
            mockSongService.Setup(service => service.GetAllSongsByPlaylist(have)).Returns((IEnumerable<SongDto>)null);
            //act
            var result = controller.GetAllSongsByPlaylist(have);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllPlaylistsBySongTest_WithExistSongAndPlaylists_ReturnList()
        {
            var playlists = fixture.CreateMany<PlaylistDto>();
            var playlistsResponse = playlists.Select(playlistDto => fixture.Build<PlaylistResponseModel>()
                .With(x => x.Name, playlistDto.Name)
                .Create());
            var song = fixture.Create<SongDto>();

            mapper.Setup(m => m.Map<IEnumerable<PlaylistResponseModel>>(playlists)).Returns(playlistsResponse);

            mockSongService.Setup(service => service.GetSongById(have)).Returns(song);
            mockPlaylistService.Setup(service =>service.GetAllPlaylistsBySong(have)).Returns(playlists);
            //act
            var result = controller.GetAllPlaylistsBySong(have) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.IsInstanceOfType(responseModel,typeof(IEnumerable<PlaylistResponseModel>));
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(playlistsResponse,responseModel);
        }
        [TestMethod()]
        public void GetAllPlaylistsBySongTest_WithUnexistSong_ReturnNotFound()
        {
            var playlists = fixture.CreateMany<PlaylistDto>();
            var playlistsResponse = playlists.Select(playlistDto => fixture.Build<PlaylistResponseModel>()
                .With(x => x.Name, playlistDto.Name)
                .Create());

            mapper.Setup(m => m.Map<IEnumerable<PlaylistResponseModel>>(playlists)).Returns(playlistsResponse);

            mockSongService.Setup(service => service.GetSongById(no)).Returns((SongDto)null);
            mockPlaylistService.Setup(service => service.GetAllPlaylistsBySong(no)).Returns(playlists);
            //act
            var result = controller.GetAllPlaylistsBySong(no);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod()]
        public void GetAllPlaylistsBySongTest_WithUnexistPlaylists_ReturnNotFound()
        {
            var playlists = fixture.CreateMany<PlaylistDto>();
            var playlistsResponse = playlists.Select(playlistDto => fixture.Build<PlaylistResponseModel>()
                .With(x => x.Name, playlistDto.Name)
                .Create());
            var song = fixture.Create<SongDto>();

            mapper.Setup(m => m.Map<IEnumerable<PlaylistResponseModel>>(playlists)).Returns(playlistsResponse);

            mockSongService.Setup(service => service.GetSongById(have)).Returns(song);
            mockPlaylistService.Setup(service => service.GetAllPlaylistsBySong(have)).Returns((IEnumerable<PlaylistDto>)null);
            //act
            var result = controller.GetAllPlaylistsBySong(have);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void AddSongToPlaylistTest_WithExistSongAndPlaylist_ReturnStatusCreated()
        {
            var song = fixture.Create<SongDto>();
            var playlist = fixture.Create<PlaylistDto>();

            mockSongService.Setup(service => service.GetSongById(have)).Returns(song);
            mockPlaylistService.Setup(service => service.GetPlaylist(have)).Returns(playlist);
            //Act
            var result = controller.AddSongToPlaylist(have, have) as StatusCodeResult;
            //assert
            Assert.AreEqual(201, result.StatusCode);
        }
        [TestMethod()]
        public void AddSongToPlaylistTest_WithUnexistSong_ReturnNotFound()
        {
            var playlist = fixture.Create<PlaylistDto>();

            mockSongService.Setup(service => service.GetSongById(no)).Returns((SongDto)null);
            mockPlaylistService.Setup(service => service.GetPlaylist(have)).Returns(playlist);
            //Act
            var result = controller.AddSongToPlaylist(have, no);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod()]
        public void AddSongToPlaylistTest_WithUnexistPlaylist_ReturnNotFound()
        {
            var song = fixture.Create<SongDto>();
            var playlist = fixture.Create<PlaylistDto>();

            mockSongService.Setup(service => service.GetSongById(have)).Returns(song);
            mockPlaylistService.Setup(service => service.GetPlaylist(no)).Returns((PlaylistDto)null);
            //Act
            var result = controller.AddSongToPlaylist(no, have);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
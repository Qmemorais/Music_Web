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

        private readonly int existId = 1, unexistId = 0;

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

            mockPlaylistService.Setup(service => service.GetPlaylist(existId)).Returns(playlist);
            mockSongService.Setup(service => service.GetAllSongsByPlaylist(existId)).Returns(songs);

            //act
            var result = controller.GetAllSongsByPlaylist(existId) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
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

            mockPlaylistService.Setup(service => service.GetPlaylist(unexistId)).Returns((PlaylistDto)null);
            mockSongService.Setup(service => service.GetAllSongsByPlaylist(unexistId)).Returns(songs);
            //act
            var result = controller.GetAllSongsByPlaylist(unexistId);
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

            mockPlaylistService.Setup(service => service.GetPlaylist(existId)).Returns(playlist);
            mockSongService.Setup(service => service.GetAllSongsByPlaylist(existId)).Returns((IEnumerable<SongDto>)null);
            //act
            var result = controller.GetAllSongsByPlaylist(existId);
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

            mockSongService.Setup(service => service.GetSongById(existId)).Returns(song);
            mockPlaylistService.Setup(service =>service.GetAllPlaylistsBySong(existId)).Returns(playlists);
            //act
            var result = controller.GetAllPlaylistsBySong(existId) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
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

            mockSongService.Setup(service => service.GetSongById(unexistId)).Returns((SongDto)null);
            mockPlaylistService.Setup(service => service.GetAllPlaylistsBySong(unexistId)).Returns(playlists);
            //act
            var result = controller.GetAllPlaylistsBySong(unexistId);
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

            mockSongService.Setup(service => service.GetSongById(existId)).Returns(song);
            mockPlaylistService.Setup(service => service.GetAllPlaylistsBySong(existId)).Returns((IEnumerable<PlaylistDto>)null);
            //act
            var result = controller.GetAllPlaylistsBySong(existId);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void AddSongToPlaylistTest_WithExistSongAndPlaylist_ReturnStatusCreated()
        {
            var song = fixture.Create<SongDto>();
            var playlist = fixture.Create<PlaylistDto>();

            mockSongService.Setup(service => service.GetSongById(existId)).Returns(song);
            mockPlaylistService.Setup(service => service.GetPlaylist(existId)).Returns(playlist);
            //Act
            var result = controller.AddSongToPlaylist(existId, existId) as StatusCodeResult;
            //assert
            Assert.AreEqual(201, result.StatusCode);
        }
        
        [TestMethod()]
        public void AddSongToPlaylistTest_WithUnexistSong_ReturnNotFound()
        {
            var playlist = fixture.Create<PlaylistDto>();

            mockSongService.Setup(service => service.GetSongById(unexistId)).Returns((SongDto)null);
            mockPlaylistService.Setup(service => service.GetPlaylist(existId)).Returns(playlist);
            //Act
            var result = controller.AddSongToPlaylist(existId, unexistId);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        
        [TestMethod()]
        public void AddSongToPlaylistTest_WithUnexistPlaylist_ReturnNotFound()
        {
            var song = fixture.Create<SongDto>();
            var playlist = fixture.Create<PlaylistDto>();

            mockSongService.Setup(service => service.GetSongById(existId)).Returns(song);
            mockPlaylistService.Setup(service => service.GetPlaylist(unexistId)).Returns((PlaylistDto)null);
            //Act
            var result = controller.AddSongToPlaylist(unexistId, existId);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
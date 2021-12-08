using AutoFixture;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Web_Music.Models;

namespace Web_Music.Controllers.Tests
{
    [TestClass()]
    public class SongControllerTests
    {
        private Mock<ISongService> mockService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private SongController controller;

        private readonly int existSong = 1, unexistSong = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockService = new Mock<ISongService>();
            mapper = new Mock<IMapper>();

            controller = new SongController(mockService.Object, mapper.Object);
        }
        
        [TestMethod()]
        public void GetSongByIdTest_WithExistId_ReturnModel()
        {
            var song = fixture.Create<SongDto>();
            var songResponse = new SongResponseModel()
            {
                Name = song.Name,
                ArtistId = song.ArtistId,
                AlbumId = song.AlbumId,
                Time = song.Time
            };

            mockService.Setup(service => service.GetSongById(existSong)).Returns(song);
            mapper.Setup(m => m.Map<SongResponseModel>(song)).Returns(songResponse);
            //act
            var result = controller.GetSongById(existSong) as OkObjectResult;
            var responseModel = (SongResponseModel)result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(songResponse, responseModel);
        }
        
        [TestMethod()]
        public void GetSongByIdTest_WithUnexistId_ReturnNotFound()
        {
            //arange
            mockService.Setup(service => service.GetSongById(unexistSong)).Returns((SongDto)null);
            //act
            var result = controller.GetSongById(unexistSong);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void CreateSongTest_WithModel_ReturnCreated()
        {
            var songRequest = fixture.Create<SongCreateRequestModel>();
            var songDto = new SongCreateDto()
            {
                Name = songRequest.Name,
                ArtistId = songRequest.ArtistId,
                AlbumId = songRequest.AlbumId,
                Time = songRequest.Time
            };

            mapper.Setup(m => m.Map<SongCreateDto>(songRequest)).Returns(songDto);
            //act
            var result = controller.CreateSong(songRequest) as StatusCodeResult;
            //assert
            Assert.AreEqual(201, result.StatusCode);
        }
        
        [TestMethod()]
        public void CreateSongTest_WithNullModel_ReturnBadRequest()
        {
            //arrange
            var song = fixture.Create<object>() as SongCreateRequestModel;
            //Act
            var result = controller.CreateSong(song);
            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void DeleteSongTest_WithExistId_ReturnNoContent()
        {
            var song = fixture.Create<SongDto>();
            mockService.Setup(service => service.GetSongById(existSong)).Returns(song);

            var result = controller.DeleteSong(existSong);
            var resultCode = result as NoContentResult;

            Assert.AreEqual(204, resultCode.StatusCode);
        }
        
        [TestMethod()]
        public void DeleteSongTest_WithUnexistId_ReturnNotFound()
        {
            mockService.Setup(service => service.GetSongById(unexistSong)).Returns((SongDto)null);

            var result = controller.DeleteSong(unexistSong);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllSongsTest_ReturnList()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto=>fixture.Build<SongResponseModel>()
                .With(x=>x.AlbumId,songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .With(x => x.Time, songDto.Time)
                .With(x => x.Name, songDto.Name)
                .Create());

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);
            mockService.Setup(service => service.GetAllSongs()).Returns(songs);
            //act
            var result = controller.GetAllSongs() as OkObjectResult;

            var responseModel = result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(songsResponse, responseModel);
        }
        
        [TestMethod()]
        public void GetAllSongsTest_ReturnNotFound()
        {
            mockService.Setup(service => service.GetAllSongs()).Returns((IEnumerable<SongDto>)null);

            var result = controller.GetAllSongs();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void UpdatePlaylistTest()
        {
            var songResponse = fixture.Create<SongUpdateRequestModel>();
            var songUpdate = new SongUpdateDto()
            {
                Name = songResponse.Name,
                ArtistId = songResponse.ArtistId,
                AlbumId = songResponse.AlbumId,
                Time = songResponse.Time
            };
            mapper.Setup(m => m.Map<SongUpdateDto>(songResponse)).Returns(songUpdate);

            var result = controller.UpdateSong(existSong, songResponse);
            var resultCode = result as NoContentResult;

            Assert.AreEqual(204, resultCode.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}
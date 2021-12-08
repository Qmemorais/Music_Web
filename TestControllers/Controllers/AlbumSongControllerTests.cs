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
    public class AlbumSongControllerTests
    {
        private Mock<ISongService> mockSongService;
        private Mock<IAlbumService> mockAlbumService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private AlbumSongController controller;

        private readonly int existId = 1, unexistId = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockAlbumService = new Mock<IAlbumService>();
            mockSongService = new Mock<ISongService>();
            mapper = new Mock<IMapper>();

            controller = new AlbumSongController(mockAlbumService.Object, mockSongService.Object, mapper.Object);
        }

        [TestMethod()]
        public void GetAllSongsByAlbumTest_WithExistAlbumAndSongs_ReturnList()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .With(x => x.Name, songDto.Name)
                .With(x => x.Time, songDto.Time)
                .Create());
            var album = fixture.Create<AlbumDto>();

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockAlbumService.Setup(service => service.GetAlbum(existId)).Returns(album);
            mockSongService.Setup(service => service.GetAllSongsByAlbum(existId)).Returns(songs);
            //act
            var result = controller.GetAllSongsByAlbum(existId) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(songsResponse, responseModel);
        }

        [TestMethod()]
        public void GetAllSongsByAlbumTest_WithUnexistAlbum_ReturnNotFound()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .With(x => x.Name, songDto.Name)
                .With(x => x.Time, songDto.Time)
                .Create());
            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockAlbumService.Setup(service => service.GetAlbum(unexistId)).Returns((AlbumDto)null);
            mockSongService.Setup(service => service.GetAllSongsByAlbum(unexistId)).Returns(songs);
            //act
            var result = controller.GetAllSongsByAlbum(unexistId);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllSongsByAlbumTest_WithUnexistSongs_ReturnNotFound()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .With(x => x.Name, songDto.Name)
                .With(x => x.Time, songDto.Time)
                .Create());
            var album = fixture.Create<AlbumDto>();

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockAlbumService.Setup(service => service.GetAlbum(existId)).Returns(album);
            mockSongService.Setup(service => service.GetAllSongsByAlbum(existId)).Returns((IEnumerable<SongDto>)null);
            //act
            var result = controller.GetAllSongsByAlbum(existId);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
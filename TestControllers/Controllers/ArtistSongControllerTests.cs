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
    public class ArtistSongControllerTests
    {
        private Mock<ISongService> mockSongService;
        private Mock<IArtistService> mockArtistService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private ArtistSongController controller;

        private readonly int have = 1, no = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockArtistService = new Mock<IArtistService>();
            mockSongService = new Mock<ISongService>();
            mapper = new Mock<IMapper>();

            controller = new ArtistSongController(mockArtistService.Object,mockSongService.Object, mapper.Object);
        }


        [TestMethod()]
        public void GetAllSongsByArtistTest_WithExistArtistAndSongs_ReturnList()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x=>x.Name,songDto.Name)
                .With(x => x.Time, songDto.Time)
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .Create());
            var artist = fixture.Create<ArtistDto>();

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockArtistService.Setup(service => service.GetArtist(have)).Returns(artist);
            mockSongService.Setup(service => service.GetAllSongsByArtist(have)).Returns(songs);
            //act
            var result = controller.GetAllSongsByArtist(have) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.AreEqual(songsResponse, responseModel);
            Assert.IsNotNull(responseModel);
        }
        [TestMethod()]
        public void GetAllSongsByArtistTest_WithUnexistArtist_ReturnNotFound()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x => x.Name, songDto.Name)
                .With(x => x.Time, songDto.Time)
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .Create());

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockArtistService.Setup(service => service.GetArtist(no)).Returns((ArtistDto)null);
            mockSongService.Setup(service => service.GetAllSongsByArtist(no)).Returns(songs);
            //act
            var result = controller.GetAllSongsByArtist(no);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod()]
        public void GetAllSongsByArtistTest_WithUnexistSongs_ReturnNotFound()
        {
            var songs = fixture.CreateMany<SongDto>();
            var songsResponse = songs.Select(songDto => fixture.Build<SongResponseModel>()
                .With(x => x.Name, songDto.Name)
                .With(x => x.Time, songDto.Time)
                .With(x => x.AlbumId, songDto.AlbumId)
                .With(x => x.ArtistId, songDto.ArtistId)
                .Create());
            var artist = fixture.Create<ArtistDto>();

            mapper.Setup(m => m.Map<IEnumerable<SongResponseModel>>(songs)).Returns(songsResponse);

            mockArtistService.Setup(service => service.GetArtist(have)).Returns(artist);
            mockSongService.Setup(service => service.GetAllSongsByArtist(have)).Returns((IEnumerable<SongDto>)null);
            //act
            var result = controller.GetAllSongsByArtist(have);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
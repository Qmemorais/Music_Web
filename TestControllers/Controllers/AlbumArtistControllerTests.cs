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
    public class AlbumArtistControllerTests
    {
        private Mock<IArtistService> mockArtistService;
        private Mock<IAlbumService> mockAlbumService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private AlbumArtistController controller;

        private readonly int have = 1, no = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockArtistService = new Mock<IArtistService>();
            mockAlbumService = new Mock<IAlbumService>();
            mapper = new Mock<IMapper>();

            controller = new AlbumArtistController(mockArtistService.Object, mockAlbumService.Object, mapper.Object);
        }

        [TestMethod()]
        public void GetAllAlbumsByArtistTest_WithExistAlbumAndArtist_ReturnList()
        {
            var albums = fixture.CreateMany<AlbumDto>();
            var albumResponse = albums.Select(albumDto => fixture.Build<AlbumResponseModel>()
                .With(x => x.Name, albumDto.Name)
                .With(x=>x.AtristId, albumDto.AtristId)
                .Create());
            var artist = fixture.Create<ArtistDto>();

            mapper.Setup(m => m.Map<IEnumerable<AlbumResponseModel>>(albums)).Returns(albumResponse);

            mockArtistService.Setup(service => service.GetArtist(have)).Returns(artist);
            mockAlbumService.Setup(service => service.GetAllAlbumsByArtist(have)).Returns(albums);
            //act
            var result = controller.GetAllAlbumsByArtist(have) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(albumResponse, responseModel);
        }

        [TestMethod()]
        public void GetAllAlbumsByArtistTest_WithUnexistAlbum_ReturnNotFound()
        {
            var albums = fixture.CreateMany<AlbumDto>();
            var albumResponse = albums.Select(albumDto => fixture.Build<AlbumResponseModel>()
                .With(x => x.Name, albumDto.Name)
                .With(x => x.AtristId, albumDto.AtristId)
                .Create());
            var artist = fixture.Create<ArtistDto>();

            mapper.Setup(m => m.Map<IEnumerable<AlbumResponseModel>>(albums)).Returns(albumResponse);

            mockArtistService.Setup(service => service.GetArtist(have)).Returns(artist);
            mockAlbumService.Setup(service => service.GetAllAlbumsByArtist(have)).Returns((IEnumerable<AlbumDto>)null);
            //act
            var result = controller.GetAllAlbumsByArtist(have);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllAlbumsByArtistTest_WithUnexistArtist_ReturnNotFound()
        {
            var albums = fixture.CreateMany<AlbumDto>();
            var albumResponse = albums.Select(albumDto => fixture.Build<AlbumResponseModel>()
                .With(x => x.Name, albumDto.Name)
                .With(x => x.AtristId, albumDto.AtristId)
                .Create());

            mapper.Setup(m => m.Map<IEnumerable<AlbumResponseModel>>(albums)).Returns(albumResponse);

            mockArtistService.Setup(service => service.GetArtist(no)).Returns((ArtistDto)null);
            mockAlbumService.Setup(service => service.GetAllAlbumsByArtist(no)).Returns(albums);
            //act
            var result = controller.GetAllAlbumsByArtist(no);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
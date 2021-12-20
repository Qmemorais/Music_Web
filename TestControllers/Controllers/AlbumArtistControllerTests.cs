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

        private readonly int existId = 1, unexistId = 0;

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

            mockArtistService.Setup(service => service.GetArtist(existId)).Returns(artist);
            mockAlbumService.Setup(service => service.GetAllAlbumsByArtist(existId)).Returns(albums);
            //act
            var result = controller.GetAllAlbumsByArtist(existId) as OkObjectResult;
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

            mockArtistService.Setup(service => service.GetArtist(existId)).Returns(artist);
            mockAlbumService.Setup(service => service.GetAllAlbumsByArtist(existId)).Returns((IEnumerable<AlbumDto>)null);
            //act
            var result = controller.GetAllAlbumsByArtist(existId);
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

            mockArtistService.Setup(service => service.GetArtist(unexistId)).Returns((ArtistDto)null);
            mockAlbumService.Setup(service => service.GetAllAlbumsByArtist(unexistId)).Returns(albums);
            //act
            var result = controller.GetAllAlbumsByArtist(unexistId);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
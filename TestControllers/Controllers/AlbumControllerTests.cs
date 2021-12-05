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
    public class AlbumControllerTests
    {
        private Mock<IAlbumService> mockService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private AlbumController controller;

        private readonly int have = 1, no = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockService = new Mock<IAlbumService>();
            mapper = new Mock<IMapper>();

            controller = new AlbumController(mockService.Object, mapper.Object);
        }

        [TestMethod()]
        public void GetAlbumByIdTest_WithExistId_ReturnModel()
        {
            var album = fixture.Create<AlbumDto>();
            var albumResponse = new AlbumResponseModel()
            {
                Name = album.Name,
                AtristId = album.AtristId
            };

            mapper.Setup(m => m.Map<AlbumResponseModel>(album)).Returns(albumResponse);
            mockService.Setup(service => service.GetAlbum(have)).Returns(album);
            //act
            var result = controller.GetAlbumById(have) as OkObjectResult;
            var responseModel = (AlbumResponseModel)result?.Value;
            //assert
            Assert.AreEqual(albumResponse, responseModel);
        }
        [TestMethod()]
        public void GetAlbumByIdTest_WithUnexistId_ReturnNotFound()
        {
            mockService.Setup(service => service.GetAlbum(no)).Returns((AlbumDto)null);
            //act
            var result = controller.GetAlbumById(no);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllAlbumsTest_ReturnList()
        {
            var albums = fixture.CreateMany<AlbumDto>();
            var albumsResponse = albums.Select(albumDto => fixture.Build<AlbumResponseModel>()
                .With(x => x.AtristId, albumDto.AtristId)
                .With(x => x.Name, albumDto.Name)
                .Create());

            mapper.Setup(m => m.Map<IEnumerable<AlbumResponseModel>>(albums)).Returns(albumsResponse);
            mockService.Setup(service => service.GetAllAlbums()).Returns(albums);
            //act
            var result = controller.GetAllAlbums() as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.AreEqual(albumsResponse, responseModel);
            Assert.IsNotNull(responseModel);
        }
        [TestMethod()]
        public void GetAllAlbumsTest_ReturnNotFound()
        {
            mockService.Setup(service => service.GetAllAlbums()).Returns((IEnumerable<AlbumDto>)null);
            //act
            var result = controller.GetAllAlbums();
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void CreateAlbumTest_WithModel_ReturnStatusCreated()
        {
            var albumRequest = fixture.Create<AlbumCreateRequestModel>();
            var albumDto = new AlbumCreateDto()
            {
                AtristId = albumRequest.AtristId,
                Name = albumRequest.Name
            };

            mapper.Setup(m => m.Map<AlbumCreateDto>(albumRequest)).Returns(albumDto);

            var result = controller.CreateAlbum(albumRequest) as StatusCodeResult;

            Assert.AreEqual(201, result.StatusCode);
        }
        [TestMethod()]
        public void CreateAlbumTest_WithNull_ReturnBadRequest()
        {
            //arrange
            var album = fixture.Create<object>() as AlbumCreateRequestModel;
            //Act
            var result = controller.CreateAlbum(album);
            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void DeleteAlbumTest_WithExistId_ReturnNoContent()
        {
            var album = fixture.Create<AlbumDto>();

            mockService.Setup(service => service.GetAlbum(have)).Returns(album);

            var result = controller.DeleteAlbum(have);
            var statusCode = result as NoContentResult;

            Assert.AreEqual(204, statusCode.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod()]
        public void DeleteAlbumTest_WithUnexistId_ReturnNotFound()
        {
            mockService.Setup(service => service.GetAlbum(no)).Returns((AlbumDto)null);

            var result = controller.DeleteAlbum(no);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void UpdateAlbumTest()
        {
            var albumUpdate = fixture.Create<AlbumUpdateDto>();
            var albumResponse = fixture.Create<AlbumUpdateRequestModel>();

            mapper.Setup(m => m.Map<AlbumUpdateDto>(albumResponse)).Returns(albumUpdate);

            var result = controller.UpdateAlbum(have, albumResponse);
            var resultCode = result as NoContentResult;

            Assert.AreEqual(204, resultCode.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}
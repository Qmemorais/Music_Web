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
    public class ArtistControllerTests
    {
        private Mock<IArtistService> mockService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private ArtistController controller;

        private readonly int have = 1, no = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockService = new Mock<IArtistService>();
            mapper = new Mock<IMapper>();

            controller = new ArtistController(mockService.Object, mapper.Object);
        }

        [TestMethod()]
        public void GetArtistByIdTest_WithExistId_ReturnModel()
        {
            var artist = fixture.Create<ArtistDto>();
            var artistResponse = fixture.Create<ArtistResponseModel>();

            mapper.Setup(m => m.Map<ArtistResponseModel>(artist)).Returns(artistResponse);
            mockService.Setup(service => service.GetArtist(have)).Returns(artist);
            //act
            var result = controller.GetArtistById(have) as OkObjectResult;
            var responseModel = (ArtistResponseModel)result?.Value;
            //assert
            Assert.AreEqual(artistResponse, responseModel);
        }
        [TestMethod()]
        public void GetArtistByIdTest_WithUnexistId_ReturnNotFound()
        {
            mockService.Setup(service => service.GetArtist(no)).Returns((ArtistDto)null);
            //act
            var result = controller.GetArtistById(no);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllArtistsTest_ReturnList()
        {
            var artists = fixture.CreateMany<ArtistDto>();
            var artistsResponse = artists.Select(artistDto => fixture.Build<ArtistResponseModel>()
                .With(x => x.Name, artistDto.Name)
                .Create());

            mapper.Setup(m => m.Map<IEnumerable<ArtistResponseModel>>(artists)).Returns(artistsResponse);
            mockService.Setup(service => service.GetAllArtists()).Returns(artists);
            //act
            var result = controller.GetAllArtists() as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.AreEqual(artistsResponse, responseModel);
            Assert.IsNotNull(responseModel);
        }
        [TestMethod()]
        public void GetAllArtistsTest_ReturnNotFound()
        {
            mockService.Setup(service => service.GetAllArtists()).Returns((IEnumerable<ArtistDto>)null);
            //act
            var result = controller.GetAllArtists();
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void CreateArtistTest_WithModel_ReturnStatusCreated()
        {
            var artistRequest = fixture.Create<ArtistCreateRequestModel>();
            var artistDto = fixture.Create<ArtistCreateDto>();

            mapper.Setup(m => m.Map<ArtistCreateDto>(artistRequest)).Returns(artistDto);

            var result = controller.CreateArtist(artistRequest) as StatusCodeResult;

            Assert.AreEqual(201, result.StatusCode);
        }
        [TestMethod()]
        public void CreateArtistTest_WithNull_ReturnBadRequest()
        {
            //arrange
            var artist = fixture.Create<object>() as ArtistCreateRequestModel;
            //Act
            var result = controller.CreateArtist(artist);
            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void DeleteArtistTest_WithExistId_ReturnNoContent()
        {
            var artist = fixture.Create<ArtistDto>();

            mockService.Setup(service => service.GetArtist(have)).Returns(artist);

            var result = controller.DeleteArtist(have);
            var statusCode = result as NoContentResult;

            Assert.AreEqual(204, statusCode.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod()]
        public void DeleteArtistTest_WithUnexistId_ReturnNotFound()
        {
            mockService.Setup(service => service.GetArtist(no)).Returns((ArtistDto)null);

            var result = controller.DeleteArtist(no);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void UpdateArtistTest()
        {
            var artistUpdate = fixture.Create<ArtistUpdateDto>();
            var artistResponse = fixture.Create<ArtistUpdateRequestModel>();

            mapper.Setup(m => m.Map<ArtistUpdateDto>(artistResponse)).Returns(artistUpdate);

            var result = controller.UpdateArtist(have, artistResponse);
            var resultCode = result as NoContentResult;

            Assert.AreEqual(204, resultCode.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}
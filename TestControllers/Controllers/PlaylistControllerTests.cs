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
    public class PlaylistControllerTests
    {
        private Mock<IPlaylistService> mockService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private PlaylistController controller;

        private readonly int existPlaylist = 1, unexistPlaylist = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockService = new Mock<IPlaylistService>();
            mapper = new Mock<IMapper>();

            controller = new PlaylistController(mockService.Object, mapper.Object);
        }

        [TestMethod()]
        public void GetPlaylistByIdTest_WithExistId_ReturnModel()
        {
            var playlist = fixture.Create<PlaylistDto>();
            var playlistResponse = new PlaylistResponseModel()
            {
                Name = playlist.Name
            };

            mockService.Setup(service=>service.GetPlaylist(existPlaylist)).Returns(playlist);
            mapper.Setup(m => m.Map<PlaylistResponseModel>(playlist)).Returns(playlistResponse);

            //act
            var result = controller.GetPlaylistById(existPlaylist) as OkObjectResult;
            var responseModel = (PlaylistResponseModel)result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(playlistResponse, responseModel);
        }

        [TestMethod()]
        public void GetPlaylistByIdTest_WithUnexistId_ReturnNotFound()
        {
            //arange
            mockService.Setup(service => service.GetPlaylist(unexistPlaylist)).Returns((PlaylistDto)null);
            //act
            var result = controller.GetPlaylistById(unexistPlaylist);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllPlaylistTest_ReturnList()
        {
            var playlists = fixture.CreateMany<PlaylistDto>();
            var playlistsResponse = playlists.Select(playlistDTO => fixture.Build<PlaylistResponseModel>()
                            .With(x => x.Name, playlistDTO.Name)
                            .Create());

            mapper.Setup(m => m.Map<IEnumerable<PlaylistResponseModel>>(playlists)).Returns(playlistsResponse);
            mockService.Setup(service => service.GetAllPlaylists()).Returns(playlists);
            //act
            var result = controller.GetAllPlaylist() as OkObjectResult;

            var responseModel = result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(playlistsResponse, responseModel);
        }
        
        [TestMethod()]
        public void GetAllPlaylistTest_ReturnNotFound()
        {
            mockService.Setup(service => service.GetAllPlaylists()).Returns((IEnumerable<PlaylistDto>)null);
            
            var result = controller.GetAllPlaylist();
            
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void CreatePlaylistTest_WithExistModel_ReturnCreatedStatus()
        {
            var playlist = fixture.Create<PlaylistCreateRequestModel>();
            var playlistDto = new PlaylistCreateDto()
            {
                Name = playlist.Name,
                UserId = playlist.UserId
            };

            mapper.Setup(m => m.Map<PlaylistCreateDto>(playlist)).Returns(playlistDto);

            var result = controller.CreatePlaylist(playlist) as StatusCodeResult;

            Assert.AreEqual(201, result.StatusCode);
        }
        
        [TestMethod()]
        public void CreatePlaylistTest_WithNullModel_ReturnBadRequest()
        {
            //arrange
            var playlist = fixture.Create<object>() as PlaylistCreateRequestModel;
            //Act
            var result = controller.CreatePlaylist(playlist);
            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void DeletePlaylistTest_WithExistId_ReturnNoContent()
        {
            var playlist = fixture.Create<PlaylistDto>();
            mockService.Setup(service => service.GetPlaylist(existPlaylist)).Returns(playlist);

            var result = controller.DeletePlaylist(existPlaylist);
            var resultCode = result as NoContentResult;

            Assert.AreEqual(204, resultCode.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        
        [TestMethod()]
        public void DeletePlaylistTest_WithUnexistId_ReturnNotFound()
        {
            mockService.Setup(service => service.GetPlaylist(unexistPlaylist)).Returns((PlaylistDto)null);

            var result = controller.DeletePlaylist(unexistPlaylist);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void UpdatePlaylistTest()
        {
            var playlist = fixture.Create<PlaylistDto>();
            var playlistUpdate = fixture.Create<PlaylistUpdateDto>();
            var playlistResponse = new PlaylistUpdateRequestModel()
            {
                Name = playlist.Name
            };

            mapper.Setup(m => m.Map<PlaylistUpdateDto>(playlistResponse)).Returns(playlistUpdate);
            mockService.Setup(service => service.GetPlaylist(existPlaylist)).Returns(playlist);

            var result = controller.UpdatePlaylist(existPlaylist, playlistResponse);
            var resultCode = result as NoContentResult;

            Assert.AreEqual(204, resultCode.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}
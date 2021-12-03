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
    public class UserPlaylistControllerTests
    {
        private Mock<IUserService> mockUserService;
        private Mock<IPlaylistService> mockPlaylistService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private UserPlaylistController controller;

        private readonly int haveUser = 1, noUser = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockPlaylistService = new Mock<IPlaylistService>();
            mockUserService = new Mock<IUserService>();
            mapper = new Mock<IMapper>();

            //expectUser = CreateUser();

            controller = new UserPlaylistController(mockPlaylistService.Object, mockUserService.Object, mapper.Object);
        }

        [TestMethod()]
        public void GetAllPlaylistsByUserTest_WithExistUserAndPlaylists_ReturnList()
        {
            var playlists = fixture.CreateMany<PlaylistDto>();
            var playlistResponse = playlists.Select(playlistDTO => fixture.Build<PlaylistResponseModel>()
                            .With(x => x.Name, playlistDTO.Name)
                            .With(x => x.Songs.Count, playlistDTO.Songs.Count)
                            .With(x => x.Users.Count, playlistDTO.Users.Count)
                            .Create());
            var user = fixture.Create<UserDto>();

            mapper.Setup(m => m.Map<IEnumerable<PlaylistResponseModel>>(playlists)).Returns(playlistResponse);

            mockUserService.Setup(service => service.GetUser(haveUser)).Returns(user);
            mockPlaylistService.Setup(service => service.GetAllPlaylistsByUser(haveUser)).Returns(playlists);
            //act
            var result = controller.GetAllPlaylistsByUser(haveUser) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(playlistResponse, responseModel);
        }
        [TestMethod()]
        public void GetAllPlaylistsByUserTest_WithUnexistUser_ReturnNotFound()
        {
            var playlists = fixture.CreateMany<PlaylistDto>();
            var playlistResponse = playlists.Select(playlistDTO => fixture.Build<PlaylistResponseModel>()
                            .With(x => x.Name, playlistDTO.Name)
                            .With(x => x.Songs.Count, playlistDTO.Songs.Count)
                            .With(x => x.Users.Count, playlistDTO.Users.Count)
                            .Create());
            var user = fixture.Create<UserDto>();

            mapper.Setup(m => m.Map<IEnumerable<PlaylistResponseModel>>(playlists)).Returns(playlistResponse);

            mockUserService.Setup(service => service.GetUser(noUser)).Returns((UserDto)null);
            mockPlaylistService.Setup(service => service.GetAllPlaylistsByUser(noUser)).Returns(playlists);
            //act
            var result = controller.GetAllPlaylistsByUser(noUser);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod()]
        public void GetAllPlaylistsByUserTest_WithUnexistPlaylists_ReturnNotFound()
        {
            var playlists = fixture.CreateMany<PlaylistDto>();
            var playlistResponse = playlists.Select(playlistDTO => fixture.Build<PlaylistResponseModel>()
                            .With(x => x.Name, playlistDTO.Name)
                            .With(x => x.Songs.Count, playlistDTO.Songs.Count)
                            .With(x => x.Users.Count, playlistDTO.Users.Count)
                            .Create());
            var user = fixture.Create<UserDto>();

            mapper.Setup(m => m.Map<IEnumerable<PlaylistResponseModel>>(playlists)).Returns(playlistResponse);

            mockUserService.Setup(service => service.GetUser(haveUser)).Returns(user);
            mockPlaylistService.Setup(service => service.GetAllPlaylistsByUser(haveUser)).Returns((IEnumerable<PlaylistDto>)null);
            //act
            var result = controller.GetAllPlaylistsByUser(haveUser);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void AddPlaylistToUserTest_ExistUserExistPlaylist_ReturnCreated()
        {
            var user = fixture.Create<UserDto>();
            var playlist = fixture.Create<PlaylistDto>();

            mockUserService.Setup(service => service.GetUser(haveUser)).Returns(user);
            mockPlaylistService.Setup(service => service.GetPlaylist(haveUser)).Returns(playlist);
            //Act
            var result = controller.AddPlaylistToUser(haveUser, haveUser) as StatusCodeResult;
            //assert
            Assert.AreEqual(201, result.StatusCode);
        }
        [TestMethod()]
        public void AddPlaylistToUserTest_UnexistUserExistPlaylist_ReturnNotFouned()
        {
            var playlist = fixture.Create<PlaylistDto>();

            mockPlaylistService.Setup(service => service.GetPlaylist(haveUser)).Returns(playlist);
            mockUserService.Setup(service => service.GetUser(noUser)).Returns((UserDto)null);
            //act
            var result = controller.AddPlaylistToUser(haveUser, haveUser) as StatusCodeResult;
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod()]
        public void AddPlaylistToUserTest_ExistUserUnexistPlaylist_ReturnNotFouned()
        {
            var user = fixture.Create<UserDto>();
            mockUserService.Setup(service => service.GetUser(haveUser)).Returns(user);
            mockPlaylistService.Setup(service => service.GetPlaylist(noUser)).Returns((PlaylistDto)null);
            //act
            var result = controller.AddPlaylistToUser(haveUser, haveUser) as StatusCodeResult;
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllUsersByPlaylistTest_WithExistPlaylist_ReturnList()
        {
            var users = fixture.CreateMany<UserDto>();
            var userResponse = users.Select(userDTO => fixture.Build<UserResponseModel>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .With(x => x.Playlists.Count, userDTO.Playlists.Count)
                            .Create());
            var playlist = fixture.Create<PlaylistDto>();

            mapper.Setup(m => m.Map<IEnumerable<UserResponseModel>>(users)).Returns(userResponse);
            mockPlaylistService.Setup(service => service.GetPlaylist(haveUser)).Returns(playlist);
            mockUserService.Setup(service => service.GetAllUsersByPlaylist(haveUser)).Returns(users);
            //act
            var result = controller.GetAllUsersByPlaylist(haveUser) as OkObjectResult;
            var responseModel = result?.Value;
            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(userResponse, responseModel);
        }
        [TestMethod()]
        public void GetAllUsersByPlaylistTest_WithUnexistPlaylist_ReturnNotFound()
        {
            var users = fixture.CreateMany<UserDto>();
            var userResponse = users.Select(userDTO => fixture.Build<UserResponseModel>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .With(x => x.Playlists.Count, userDTO.Playlists.Count)
                            .Create());
            mapper.Setup(m => m.Map<IEnumerable<UserResponseModel>>(users)).Returns(userResponse);
            mockPlaylistService.Setup(service => service.GetPlaylist(haveUser)).Returns((PlaylistDto)null);
            mockUserService.Setup(service => service.GetAllUsersByPlaylist(haveUser)).Returns(users);
            //act
            var result = controller.GetAllPlaylistsByUser(noUser);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod()]
        public void GetAllUsersByPlaylistTest_WithUnexistUsers_ReturnNotFound()
        {
            var users = fixture.CreateMany<UserDto>();
            var userResponse = users.Select(userDTO => fixture.Build<UserResponseModel>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .With(x => x.Playlists.Count, userDTO.Playlists.Count)
                            .Create());
            var playlist = fixture.Create<PlaylistDto>();

            mapper.Setup(m => m.Map<IEnumerable<UserResponseModel>>(users)).Returns(userResponse);
            mockPlaylistService.Setup(service => service.GetPlaylist(haveUser)).Returns(playlist);
            mockUserService.Setup(service => service.GetAllUsersByPlaylist(haveUser)).Returns((IEnumerable<UserDto>)null);
            //act
            var result = controller.GetAllPlaylistsByUser(haveUser);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
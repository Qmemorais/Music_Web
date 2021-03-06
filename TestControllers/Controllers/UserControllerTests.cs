using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using BusinessLayer.Services.Interface;
using Moq;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Web_Music.Models;
using System.Linq;
using AutoFixture;

namespace Web_Music.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        private Mock<IUserService> mockService;
        private Mock<IMapper> mapper;
        private Fixture fixture;

        private UserController controller;

        private UserDto expectUser;
        private readonly int existUser = 1, unexistUser = 0;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            mockService = new Mock<IUserService>();
            mapper = new Mock<IMapper>();

            expectUser = CreateUser();

            controller = new UserController(mockService.Object, mapper.Object);
        }

        [TestMethod()]
        public void GetUserByIdTest_WithExistId_ReturnExistModel()
        {
            //arrange
            var userResponse = new UserResponseModel()
            {
                Name = expectUser.Name,
                Surname = expectUser.Surname,
                Email = expectUser.Email,
            };

            mockService.Setup(service => service.GetUser(existUser)).Returns(expectUser);
            mapper.Setup(m => m.Map<UserResponseModel>(expectUser)).Returns(userResponse);

            //act
            var result = controller.GetUserById(existUser) as OkObjectResult;

            var responseModel = (UserResponseModel)result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(userResponse, responseModel);
        }
        
        [TestMethod()]
        public void GetUserByIdTest_WithUnexistId_ReturnNotFound()
        {
            //arange
            mockService.Setup(service => service.GetUser(unexistUser)).Returns((UserDto)null);
            //act
            var result = controller.GetUserById(unexistUser);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllUsersTest()
        {
            var users = fixture.CreateMany<UserDto>();
            var userResponse = users.Select(userDTO => fixture.Build<UserResponseModel>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create());

            mapper.Setup(m => m.Map<IEnumerable<UserResponseModel>>(users)).Returns(userResponse);
            mockService.Setup(service => service.GetAllUsers()).Returns(users);
            //act
            var result = controller.GetAllUsers() as OkObjectResult;

            var responseModel = result?.Value;
            //assert
            Assert.IsNotNull(responseModel);
            Assert.AreEqual(userResponse, responseModel);
        }
        
        [TestMethod()]
        public void CreateUserTest_WithoutModel_ReturnBadRequest()
        {
            //arrange
            var user = fixture.Create<object>() as UserCreateRequestModel;
            //Act
            var result = controller.CreateUser(user);
            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        
        [TestMethod()]
        public void CreateUserTest_WithUnexistEmail_ReturnHttpStatusCodeCreated()
        {
            //arrange
            var user = fixture.Create<UserCreateRequestModel>();
            var userResponse = new UserCreateDto()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
            };

            mapper.Setup(m => m.Map<UserCreateDto>(user)).Returns(userResponse);
            //Act
            var result = controller.CreateUser(user) as StatusCodeResult;
            //assert
            Assert.AreEqual(201, result.StatusCode);
        }
        
        [TestMethod()]
        public void DeleteUserTest_WithExistId_ReturnNoContent()
        {
            //arrange
            mockService.Setup(service => service.GetUser(existUser)).Returns(expectUser);
            //Act
            var result = controller.DeleteUser(existUser);
            var resultCode = result as NoContentResult;
            //assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.AreEqual(204, resultCode.StatusCode);
        }
        [TestMethod()]
        public void DeleteUserTest_WithUnexistId_ReturnNotFound()
        {
            //arange
            mockService.Setup(service => service.GetUser(unexistUser)).Returns((UserDto)null);
            //act
            var result = controller.DeleteUser(unexistUser);
            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        
        [TestMethod()]
        public void UpdateUserTest()
        {
            //arrange
            var userResponse = new UserUpdateRequestModel()
            {
                Name = expectUser.Name,
                Surname = expectUser.Surname,
                Email = expectUser.Email,
            };
            var userUpdateDto = new UserUpdateDto()
            {
                Name = expectUser.Name,
                Surname = expectUser.Surname,
                Email = expectUser.Email,
            };

            mapper.Setup(m => m.Map<UserUpdateDto>(userResponse)).Returns(userUpdateDto);
            mockService.Setup(service => service.GetUser(existUser)).Returns(expectUser);
            //Act
            var result = controller.UpdateUser(existUser, userResponse);
            var resultCode = result as NoContentResult;
            //assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.AreEqual(204, resultCode.StatusCode);
        }
        
        public static UserDto CreateUser()
        {
            var playlists = new List<PlaylistUpdateDto>
            {
                new PlaylistUpdateDto { Name = "PlaylistToWakeUp" },
                new PlaylistUpdateDto { Name = "PlaylistToCook" },
                new PlaylistUpdateDto { Name = "Thanks" },
                new PlaylistUpdateDto { Name = "TestPlaylistServer" }
            };
            return new()
            {
                Email = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Surname = Guid.NewGuid().ToString(),
                Playlists = playlists
            };
        }
    }
}
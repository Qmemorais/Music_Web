using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Moq;
using System.Collections.Generic;
using DataLayer.Models;
using AutoFixture;
using System;
using BusinessLayer.Models;

namespace BusinessLayer.Services.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private Mock<MusicContext> context;

        private Mock<IMapper> mapper;
        private UserService service;
        private Fixture fixture;

        private int userId;
        private int playlistId;

        private List<User> user;
        private List<Playlist> playlist;

        private Mock<DbSet<Playlist>> DbSetPlaylist;
        private Mock<DbSet<User>> DbSetUser;

        private UserDto mappedUser;
        private IEnumerable<UserDto> mappedUsers;
        private UserUpdateDto mappedUserUpdate;
        private UserCreateDto userToCreate, userToCreateNew;
        private User userModel;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            userId = fixture.Create<int>();
            user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            playlistId = fixture.Create<int>();
            playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .With(x => x.Users, user)
                .CreateMany(1)
                .ToList();

            DbSetPlaylist = CreateDbSetMock(playlist);
            DbSetUser = CreateDbSetMock(user);

            mappedUser = user.Select(userDTO => fixture.Build<UserDto>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create()).First();

            mappedUsers = user.Select(userDTO => fixture.Build<UserDto>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create());

            userToCreate = user.Select(userCreateDTO => fixture.Build<UserCreateDto>()
                            .With(x => x.Name, userCreateDTO.Name)
                            .With(x => x.Email, userCreateDTO.Email)
                            .With(x => x.Surname, userCreateDTO.Surname)
                            .Create()).First();

            userToCreateNew = fixture.Create<UserCreateDto>();
            userModel = fixture.Build<User>()
                            .With(x => x.Name, userToCreateNew.Name)
                            .With(x => x.Email, userToCreateNew.Email)
                            .With(x => x.Surname, userToCreateNew.Surname)
                            .Create();

            mappedUserUpdate = user.Select(userDTO => fixture.Build<UserUpdateDto>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create()).First();

            context = new Mock<MusicContext>();
            mapper = new Mock<IMapper>();
            mapper.Setup(mapper => mapper.Map<UserDto>(user.First())).Returns(mappedUser);
            mapper.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(user)).Returns(mappedUsers);
            mapper.Setup(mapper => mapper.Map<User>(userToCreate)).Returns(user.First());
            mapper.Setup(mapper => mapper.Map<User>(userToCreateNew)).Returns(userModel);

            service = new UserService(context.Object, mapper.Object);
        }

        [TestMethod()]
        public void AddPlaylistToUserTest_WithExistUserAndPlaylist()
        {
            //arange
            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            service.AddPlaylistToUser(userId,playlistId);
            //assert
            context.Verify(x => x.Playlists.Update(playlist.First()));
            context.Verify(x => x.Users.Update(user.First()));
            context.Verify(x=>x.SaveChanges());
        }
        
        [TestMethod()]
        public void AddPlaylistToUserTest_WithUnexistUser()
        {
            //arange
            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Users).Returns((DbSet<User>)null);
            //assert
            try
            {
                service.AddPlaylistToUser(userId, playlistId);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
        
        [TestMethod()]
        public void AddPlaylistToUserTest_WithUnexistPlaylist()
        {
            //arange
            context.Setup(x => x.Playlists).Returns((DbSet<Playlist>)null);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //assert
            try
            {
                service.AddPlaylistToUser(userId, playlistId);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
        
        [TestMethod()]
        public void GetAllUsersTest_WithExistModels_ReturnAllUsers()
        {
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            var users = service.GetAllUsers();
            //assert
            Assert.IsInstanceOfType(users, typeof(IEnumerable<UserDto>));
            Assert.AreEqual(mappedUsers.Count(), users.Count());
            Assert.AreEqual(mappedUsers.ElementAt(0).Email, users.ElementAt(0).Email);
            Assert.AreEqual(mappedUsers.ElementAt(0).Name, users.ElementAt(0).Name);
            Assert.AreEqual(mappedUsers.ElementAt(0).Surname, users.ElementAt(0).Surname);
        }
        
        [TestMethod()]
        public void GetAllUsersByPlaylistTest_WithExistPlaylist_ReturnAllUsers()
        {
            //arange
            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            var result = service.GetAllUsersByPlaylist(playlistId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<UserDto>));
            Assert.AreEqual(mappedUsers.Count(), result.Count());
            Assert.AreEqual(mappedUsers.ElementAt(0).Email, result.ElementAt(0).Email);
            Assert.AreEqual(mappedUsers.ElementAt(0).Name, result.ElementAt(0).Name);
            Assert.AreEqual(mappedUsers.ElementAt(0).Surname, result.ElementAt(0).Surname);
        }
        
        [TestMethod()]
        public void GetAllUsersByPlaylistTest_WithUnexistPlaylist_ReturnUnexistModel()
        {
            //arange
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            var result = service.GetAllUsersByPlaylist(playlistId);
            //assert 
            Assert.IsNull(result);
        }
        
        [TestMethod()]
        public void GetUserTest_WithUnexistId_ReturnUnexistModel()
        {
            //act
            var result = service.GetUser(userId);
            //assert 
            Assert.IsNull(result);
        }
        
        [TestMethod()]
        public void GetUserTest_WithExistId_ReturnExistModel()
        {
            //arange
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            var result = service.GetUser(userId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(UserDto));
            Assert.AreEqual(mappedUser.Email, result.Email);
            Assert.AreEqual(mappedUser.Name, result.Name);
            Assert.AreEqual(mappedUser.Surname, result.Surname);
        }
        
        [TestMethod()]
        public void CreateUserTest_WithExistEmail_ReturnFalse()
        {
            //arange
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //assert 
            try
            {
                service.CreateUser(userToCreate);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
        
        [TestMethod()]
        public void CreateUserTest_WithUnexistEmail_ReturnTrue()
        {
            //arange
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            service.CreateUser(userToCreateNew);
            //assert 
            context.Verify(x => x.Users.Add(userModel));
            context.Verify(x => x.SaveChanges());
        }
        
        [TestMethod()]
        public void UpdateUserTest_WithExistId_ReturnTrue()
        {
            //arange
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            service.UpdateUser(userId, mappedUserUpdate);
            //assert 
            context.Verify(x => x.Users.Update(user.First()));
            context.Verify(x => x.SaveChanges());
        }
        
        [TestMethod()]
        public void UpdateUserTest_WithUnexistId_ReturnFalse()
        {
            //assert 
            try
            {
                service.UpdateUser(userId, mappedUserUpdate);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
        
        [TestMethod()]
        public void DeleteUserTest_WithExistId_ReturnTrue()
        {
            //arange
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            service.DeleteUser(userId);
            //assert 
            context.Verify(x => x.Users.Remove(user.First()));
            context.Verify(x => x.SaveChanges());
        }

        [TestMethod()]
        public void DeleteUserTest_WithUnexistId_ReturnFalse()
        {
            //arange
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //assert 
            try
            {
                service.DeleteUser(userId);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        private Mock<DbSet<T>> CreateDbSetMock<T>(List<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(s => elements.Add(s));

            return dbSetMock;
        }
    }
}
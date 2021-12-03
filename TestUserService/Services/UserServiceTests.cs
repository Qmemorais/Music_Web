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

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            context = new Mock<MusicContext>();
            mapper = new Mock<IMapper>();

            service = new UserService(context.Object, mapper.Object);
        }

        [TestMethod()]
        public void AddPlaylistToUserTest_WithExistUserAndPlaylist()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .With(x => x.Users, user)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var DbSetUser = CreateDbSetMock(user);

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
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .With(x => x.Users, user)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Users).Returns((DbSet<User>)null);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.AddPlaylistToUser(userId, playlistId));
        }
        
        [TestMethod()]
        public void AddPlaylistToUserTest_WithUnexistPlaylist()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();

            var DbSetUser = CreateDbSetMock(user);

            context.Setup(x => x.Playlists).Returns((DbSet<Playlist>)null);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.AddPlaylistToUser(userId, playlistId));
        }

        [TestMethod()]
        public void AddPlaylistToUserTest_WichAlreadyExist()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistId = user[0].Playlists.First().Id;
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .With(x => x.Users, user)
                .CreateMany(1)
                .ToList();

            var DbSetUser = CreateDbSetMock(user);
            var DbSetPlaylist = CreateDbSetMock(playlist);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //assert
            service.AddPlaylistToUser(userId, playlistId);
            context.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod()]
        public void GetAllUsersTest_WithExistModels_ReturnAllUsers()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var DbSetUser = CreateDbSetMock(user);

            var mappedUsers = user.Select(userDTO => fixture.Build<UserDto>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create());

            mapper.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(user)).Returns(mappedUsers);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            var users = service.GetAllUsers();
            //assert
            Assert.IsInstanceOfType(users, typeof(IEnumerable<UserDto>));
            Assert.IsNotNull(users);
            Assert.AreEqual(mappedUsers.Count(), users.Count());
            Assert.AreEqual(mappedUsers.ElementAt(0).Email, users.ElementAt(0).Email);
            Assert.AreEqual(mappedUsers.ElementAt(0).Name, users.ElementAt(0).Name);
            Assert.AreEqual(mappedUsers.ElementAt(0).Surname, users.ElementAt(0).Surname);
        }
        
        [TestMethod()]
        public void GetAllUsersByPlaylistTest_WithExistPlaylist_ReturnAllUsers()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();
            var playlist = fixture.Build<Playlist>()
                .With(x => x.Id, playlistId)
                .With(x => x.Users, user)
                .CreateMany(1)
                .ToList();

            var DbSetPlaylist = CreateDbSetMock(playlist);
            var DbSetUser = CreateDbSetMock(user);

            var mappedUsers = user.Select(userDTO => fixture.Build<UserDto>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create());

            mapper.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(user)).Returns(mappedUsers);

            context.Setup(x => x.Playlists).Returns(DbSetPlaylist.Object);
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //act
            var result = service.GetAllUsersByPlaylist(playlistId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<UserDto>));
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedUsers.Count(), result.Count());
            Assert.AreEqual(mappedUsers.ElementAt(0).Email, result.ElementAt(0).Email);
            Assert.AreEqual(mappedUsers.ElementAt(0).Name, result.ElementAt(0).Name);
            Assert.AreEqual(mappedUsers.ElementAt(0).Surname, result.ElementAt(0).Surname);
        }
        
        [TestMethod()]
        public void GetAllUsersByPlaylistTest_WithUnexistPlaylist_ReturnUnexistModel()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var playlistId = fixture.Create<int>();

            var DbSetUser = CreateDbSetMock(user);

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
            var userId = fixture.Create<int>();

            context.Setup(x => x.Users).Returns((DbSet<User>)null);

            var result = service.GetUser(userId);
            //assert 
            Assert.IsNull(result);
        }
        
        [TestMethod()]
        public void GetUserTest_WithExistId_ReturnExistModel()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var mappedUser = user.Select(userDTO => fixture.Build<UserDto>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create()).First();

            var DbSetUser = CreateDbSetMock(user);

            mapper.Setup(mapper => mapper.Map<UserDto>(user.First())).Returns(mappedUser);

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
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var userToCreate = user.Select(userCreateDTO => fixture.Build<UserCreateDto>()
                            .With(x => x.Name, userCreateDTO.Name)
                            .With(x => x.Email, userCreateDTO.Email)
                            .With(x => x.Surname, userCreateDTO.Surname)
                            .Create()).First();

            var DbSetUser = CreateDbSetMock(user);

            mapper.Setup(mapper => mapper.Map<User>(userToCreate)).Returns(user.First());
            context.Setup(x => x.Users).Returns(DbSetUser.Object);
            //assert 
            service.CreateUser(userToCreate);
            context.Verify(x=>x.SaveChanges(),Times.Never());
        }
        
        [TestMethod()]
        public void CreateUserTest_WithUnexistEmail_ReturnTrue()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var userToCreateNew = fixture.Create<UserCreateDto>();
            var userModel = fixture.Build<User>()
                            .With(x => x.Name, userToCreateNew.Name)
                            .With(x => x.Email, userToCreateNew.Email)
                            .With(x => x.Surname, userToCreateNew.Surname)
                            .Create();

            var DbSetUser = CreateDbSetMock(user);

            mapper.Setup(mapper => mapper.Map<User>(userToCreateNew)).Returns(userModel);
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
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();
            var mappedUserUpdate = user.Select(userDTO => fixture.Build<UserUpdateDto>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create()).First();

            var DbSetUser = CreateDbSetMock(user);

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
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();
            var mappedUserUpdate = user.Select(userDTO => fixture.Build<UserUpdateDto>()
                            .With(x => x.Name, userDTO.Name)
                            .With(x => x.Email, userDTO.Email)
                            .With(x => x.Surname, userDTO.Surname)
                            .Create()).First();
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.UpdateUser(userId, mappedUserUpdate));
        }
        
        [TestMethod()]
        public void DeleteUserTest_WithExistId_ReturnTrue()
        {
            //arange
            var userId = fixture.Create<int>();
            var user = fixture.Build<User>()
                .With(x => x.Id, userId)
                .CreateMany(1)
                .ToList();

            var DbSetUser = CreateDbSetMock(user);

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
            var userId = fixture.Create<int>();

            context.Setup(x => x.Users).Returns((DbSet<User>)null);
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.DeleteUser(userId));
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
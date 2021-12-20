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
    public class AlbumServiceTests
    {
        private Mock<MusicContext> context;

        private Mock<IMapper> mapper;
        private AlbumService service;
        private Fixture fixture;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            context = new Mock<MusicContext>();
            mapper = new Mock<IMapper>();

            service = new AlbumService(context.Object, mapper.Object);
        }

        [TestMethod()]
        public void CreateAlbumTest_WithExistArtistAndNoName_ReturnCreated()
        {
            var albumCreate = fixture.Create<AlbumCreateDto>();
            var albumModel = fixture.Build<Album>().With(x => x.Name, albumCreate.Name).Create();
            var artist = fixture.Build<Artist>().With(x => x.Id , albumCreate.AtristId).CreateMany(1).ToList();

            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>().With(x => x.Id, albumId).CreateMany(1).ToList();

            var dbSetAlbum = CreateDbSetMock(album);
            var dbSetArtist = CreateDbSetMock(artist);

            mapper.Setup(mapper => mapper.Map<Album>(albumCreate)).Returns(albumModel);
            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            context.Setup(x => x.Artists).Returns(dbSetArtist.Object);
            //act
            service.CreateAlbum(albumCreate);
            //assert 
            context.Verify(x => x.Albums.Add(albumModel));
            context.Verify(x => x.SaveChanges());
        }
        
        [TestMethod()]
        public void CreateAlbumTest_WithUnexistArtist_ReturnException()
        {
            var albumCreate = fixture.Create<AlbumCreateDto>();
            var albumModel = fixture.Build<Album>().With(x => x.Name, albumCreate.Name).Create();
            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>().With(x => x.Id, albumId).CreateMany(1).ToList();
            var dbSetAlbum = CreateDbSetMock(album);

            mapper.Setup(mapper => mapper.Map<Album>(albumCreate)).Returns(albumModel);
            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.CreateAlbum(albumCreate));
        }
        
        [TestMethod()]
        public void CreateAlbumTest_WithExistArtistAndExistName_ReturnNotVerify()
        {
            var albumCreate = fixture.Create<AlbumCreateDto>();
            var artist = fixture.Build<Artist>().With(x => x.Id, albumCreate.AtristId).CreateMany(1).ToList();

            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>().With(x => x.Id, albumId).CreateMany(1).ToList();

            var dbSetAlbum = CreateDbSetMock(album);
            var dbSetArtist = CreateDbSetMock(artist);

            mapper.Setup(mapper => mapper.Map<Album>(albumCreate)).Returns(album.First());
            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            context.Setup(x => x.Artists).Returns(dbSetArtist.Object);
            //act
            service.CreateAlbum(albumCreate);
            //assert 
            context.Verify(x => x.Albums.Add(album.First()), Times.Never());
            context.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod()]
        public void DeleteAlbumTest_WithUnexistId_ReturnException()
        {
            //arange
            var albumId = fixture.Create<int>();

            context.Setup(x => x.Albums).Returns((DbSet<Album>)null);
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.DeleteAlbum(albumId));
        }
        
        [TestMethod()]
        public void DeleteAlbumTest_WithExistId_ReturnTrue()
        {
            //arange
            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>()
                .With(x => x.Id, albumId)
                .CreateMany(1)
                .ToList();

            var dbSetAlbum = CreateDbSetMock(album);

            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            //act
            service.DeleteAlbum(albumId);
            //assert 
            context.Verify(x => x.Albums.Remove(album.First()));
            context.Verify(x => x.SaveChanges());
        }

        [TestMethod()]
        public void GetAlbumTest_WithUnexistId_ReturnUnexistModel()
        {
            var albumId = fixture.Create<int>();
            context.Setup(x => x.Albums).Returns((DbSet<Album>)null);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAlbum(albumId));
        }
        
        [TestMethod()]
        public void GetAlbumTest_WithExistId_ReturnExistModel()
        {
            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>()
                .With(x => x.Id, albumId)
                .CreateMany(1)
                .ToList();
            var dbSetAlbum = CreateDbSetMock(album);

            var mappedAlbum = album.Select(albumDTO => fixture.Build<AlbumDto>()
                            .With(x => x.Name, albumDTO.Name)
                            .Create()).First();
            mapper.Setup(mapper => mapper.Map<AlbumDto>(album.First())).Returns(mappedAlbum);
            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            //act
            var result = service.GetAlbum(albumId);
            //assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedAlbum.Name, result.Name);
            Assert.AreEqual(mappedAlbum.AtristId, result.AtristId);
        }

        [TestMethod()]
        public void GetAllAlbumsTest_WithUnexistModels_ReturnException()
        {
            context.Setup(x => x.Albums).Returns((DbSet<Album>)null);
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => service.GetAllAlbums());
        }
        
        [TestMethod()]
        public void GetAllAlbumsTest_WithExistModels_ReturnList()
        {
            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>()
                .With(x => x.Id, albumId)
                .CreateMany(1)
                .ToList();

            var dbSetAlbum = CreateDbSetMock(album);

            var mappedAlbum = album.Select(songDTO => fixture.Build<AlbumDto>()
                            .With(x => x.Name, songDTO.Name)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<AlbumDto>>(album)).Returns(mappedAlbum);
            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            //act
            var result = service.GetAllAlbums();
            //assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedAlbum.Count(), result.Count());
            Assert.AreEqual(mappedAlbum.ElementAt(0).Name, result.ElementAt(0).Name);
        }

        [TestMethod()]
        public void GetAllAlbumsByArtistTest_WithExistArtist_ReturnAllAlbums()
        {
            var artistId = fixture.Create<int>();
            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>()
                .With(x => x.Id, albumId)
                .With(x => x.AtristId, artistId)
                .CreateMany(1)
                .ToList();
            var dbSetAlbum = CreateDbSetMock(album);
            var mappedAlbum = album.Select(albumDTO => fixture.Build<AlbumDto>()
                            .With(x => x.Name, albumDTO.Name)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<AlbumDto>>(album)).Returns(mappedAlbum);
            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            //act
            var result = service.GetAllAlbumsByArtist(artistId);
            //assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedAlbum.Count(), result.Count());
            Assert.AreEqual(mappedAlbum.ElementAt(0).Name, result.ElementAt(0).Name);
        }
        
        [TestMethod()]
        public void GetAllAlbumsByArtistTest_WithUneistArtist_ReturnNull()
        {
            var artistId = fixture.Create<int>();
            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>()
                .With(x => x.Id, albumId)
                .CreateMany(1)
                .ToList();
            var dbSetAlbum = CreateDbSetMock(album);
            var mappedAlbum = album.Select(albumDTO => fixture.Build<AlbumDto>()
                            .With(x => x.Name, albumDTO.Name)
                            .Create());
            mapper.Setup(mapper => mapper.Map<IEnumerable<AlbumDto>>(album)).Returns(mappedAlbum);
            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            //act
            var result = service.GetAllAlbumsByArtist(artistId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<AlbumDto>));
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public void UpdateAlbumTest_WithUnexistId_ReturnFalse()
        {
            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>()
                .With(x => x.Id, albumId)
                .CreateMany(1)
                .ToList();
            var mappedAlbum = album.Select(albumDTO => fixture.Build<AlbumUpdateDto>()
                            .With(x => x.Name, albumDTO.Name)
                            .Create()).First();
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.UpdateAlbum(albumId, mappedAlbum));
        }
        
        [TestMethod()]
        public void UpdateAlbumTest_WithExistId_ReturnTrue()
        {
            var albumId = fixture.Create<int>();
            var album = fixture.Build<Album>()
                .With(x => x.Id, albumId)
                .CreateMany(1)
                .ToList();
            var mappedAlbum = album.Select(albumDTO => fixture.Build<AlbumUpdateDto>()
                            .With(x => x.Name, albumDTO.Name)
                            .Create()).First();
            var dbSetAlbum = CreateDbSetMock(album);
            context.Setup(x => x.Albums).Returns(dbSetAlbum.Object);
            //act
            service.UpdateAlbum(albumId, mappedAlbum);
            //assert 
            context.Verify(x => x.Albums.Update(album.First()));
            context.Verify(x => x.SaveChanges());
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
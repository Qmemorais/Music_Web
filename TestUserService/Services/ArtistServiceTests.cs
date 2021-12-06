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
    public class ArtistServiceTests
    {
        private Mock<MusicContext> context;

        private Mock<IMapper> mapper;
        private ArtistService service;
        private Fixture fixture;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            context = new Mock<MusicContext>();
            mapper = new Mock<IMapper>();

            service = new ArtistService(context.Object, mapper.Object);
        }

        [TestMethod()]
        public void CreateArtistTest_WithExistName_ReturnFalse()
        {
            //arange
            var artistId = fixture.Create<int>();
            var artist = fixture.Build<Artist>()
                .With(x => x.Id, artistId)
                .CreateMany(1)
                .ToList();

            var artistToCreate = artist.Select(userCreateDTO => fixture.Build<ArtistCreateDto>()
                            .Create()).First();

            var DbSetArtist = CreateDbSetMock(artist);

            mapper.Setup(mapper => mapper.Map<Artist>(artistToCreate)).Returns(artist.First());
            context.Setup(x => x.Artists).Returns(DbSetArtist.Object);
            //assert 
            service.CreateArtist(artistToCreate);
            context.Verify(x => x.SaveChanges(), Times.Never());
        }
        [TestMethod()]
        public void CreateArtistTest_WithUnexistName_ReturnCreated()
        {
            //arange
            var artistId = fixture.Create<int>();
            var artist = fixture.Build<Artist>()
                .With(x => x.Id, artistId)
                .CreateMany(1)
                .ToList();

            var artistToCreateNew = fixture.Create<ArtistCreateDto>();
            var artistModel = fixture.Build<Artist>()
                            .With(x => x.Name, artistToCreateNew.Name)
                            .Create();

            var DbSetArtist = CreateDbSetMock(artist);

            mapper.Setup(mapper => mapper.Map<Artist>(artistToCreateNew)).Returns(artistModel);
            context.Setup(x => x.Artists).Returns(DbSetArtist.Object);
            //act
            service.CreateArtist(artistToCreateNew);
            //assert 
            context.Verify(x => x.SaveChanges());
        }

        [TestMethod()]
        public void DeleteArtistTest_WithExistId_ReturnTrue()
        {
            //arange
            var artistId = fixture.Create<int>();
            var artist = fixture.Build<Artist>()
                .With(x => x.Id, artistId)
                .CreateMany(1)
                .ToList();

            var DbSetArtist = CreateDbSetMock(artist);

            context.Setup(x => x.Artists).Returns(DbSetArtist.Object);
            //act
            service.DeleteArtist(artistId);
            //assert 
            context.Verify(x => x.SaveChanges());
        }
        [TestMethod()]
        public void DeleteArtistTest_WithUneistId_ReturnFalse()
        {
            //arange
            var artistId = fixture.Create<int>();

            context.Setup(x => x.Artists).Returns((DbSet<Artist>)null);
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.DeleteArtist(artistId));
        }

        [TestMethod()]
        public void GetAllArtistsTest_WithExistModels_ReturnAllArtists()
        {
            //arange
            var artistId = fixture.Create<int>();
            var artist = fixture.Build<Artist>()
                .With(x => x.Id, artistId)
                .CreateMany(1)
                .ToList();

            var DbSetArtist = CreateDbSetMock(artist);

            var mappedArtist = artist.Select(artistDTO => fixture.Build<ArtistDto>()
                            .With(x=>x.Name, artistDTO.Name)
                            .Create());

            mapper.Setup(mapper => mapper.Map<IEnumerable<ArtistDto>>(artist)).Returns(mappedArtist);
            context.Setup(x => x.Artists).Returns(DbSetArtist.Object);
            //act
            var users = service.GetAllArtists();
            //assert
            Assert.IsInstanceOfType(users, typeof(IEnumerable<ArtistDto>));
            Assert.IsNotNull(users);
            Assert.AreEqual(mappedArtist.Count(), users.Count());
            Assert.AreEqual(mappedArtist.ElementAt(0).Name, users.ElementAt(0).Name);
        }

        [TestMethod()]
        public void GetArtistTest_WithUnexistId_ReturnUnexistModel()
        {
            //act
            var artistId = fixture.Create<int>();

            context.Setup(x => x.Artists).Returns((DbSet<Artist>)null);
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.GetArtist(artistId));
        }
        [TestMethod()]
        public void GetArtistTest_WithExistId_ReturnExistModel()
        {
            //arange
            var artistId = fixture.Create<int>();
            var artist = fixture.Build<Artist>()
                .With(x => x.Id, artistId)
                .CreateMany(1)
                .ToList();

            var mappedArtist = artist.Select(artistDTO => fixture.Build<ArtistDto>()
                            .With(x => x.Name, artistDTO.Name)
                            .Create()).First();

            var DbSetArtist = CreateDbSetMock(artist);

            mapper.Setup(mapper => mapper.Map<ArtistDto>(artist.First())).Returns(mappedArtist);

            context.Setup(x => x.Artists).Returns(DbSetArtist.Object);
            //act
            var result = service.GetArtist(artistId);
            //assert 
            Assert.IsInstanceOfType(result, typeof(ArtistDto));
            Assert.AreEqual(mappedArtist.Name, result.Name);
        }

        [TestMethod()]
        public void UpdateArtistTest_WithUnexistId_ReturnFalse()
        {
            //arange
            var artistId = fixture.Create<int>();
            var artist = fixture.Build<Artist>()
                .With(x => x.Id, artistId)
                .CreateMany(1)
                .ToList();
            var mappedArtistUpdate = artist.Select(artistDTO => fixture.Build<ArtistUpdateDto>()
                            .With(x => x.Name, artistDTO.Name)
                            .Create()).First();
            //assert 
            Assert.ThrowsException<ArgumentNullException>(() => service.UpdateArtist(artistId, mappedArtistUpdate));
        }
        [TestMethod()]
        public void UpdateArtistTest_WithExistId_ReturnTrue()
        {
            //arange
            var artistId = fixture.Create<int>();
            var artist = fixture.Build<Artist>()
                .With(x => x.Id, artistId)
                .CreateMany(1)
                .ToList();
            var mappedArtistUpdate = artist.Select(artistDTO => fixture.Build<ArtistUpdateDto>()
                            .With(x => x.Name, artistDTO.Name)
                            .Create()).First();

            var DbSetArtist = CreateDbSetMock(artist);

            context.Setup(x => x.Artists).Returns(DbSetArtist.Object);
            //act
            service.UpdateArtist(artistId, mappedArtistUpdate);
            //assert 
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
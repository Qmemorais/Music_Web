using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace BusinessLayer.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public AlbumService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public void CreateAlbum(AlbumCreateDTO albumCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteAlbum(Guid id)
        {
            throw new NotImplementedException();
        }

        public AlbumDTOToGet GetAlbumById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<AlbumDTOToGet> GetAlbums()
        {
            throw new NotImplementedException();
        }

        public List<AlbumDTOToGet> GetAllAlbumsByArtist(Guid artistId)
        {
            throw new NotImplementedException();
        }

        public List<AlbumDTOToGet> GetAllAlbumsByTime(DateTime time)
        {
            throw new NotImplementedException();
        }

        public void UpdateAlbum(AlbumUpdateDTO albumUpdate, Guid albumId)
        {
            throw new NotImplementedException();
        }
    }
}

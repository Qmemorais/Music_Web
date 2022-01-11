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
            var album = _mapper.Map<Album>(albumCreate);
            var artistWhoCreate = _uow.Artists.Find(a => a.Id == albumCreate.AtristId).First();
            var isAlbumFromArtist = artistWhoCreate.Albums.Any(a => a.Name == albumCreate.Name);

            if (!isAlbumFromArtist)
            {
                _uow.Albums.Create(album);
                artistWhoCreate.Albums.Add(album);
                _uow.Artists.Update(artistWhoCreate);
                _uow.Save();
            }
        }

        public void DeleteAlbum(Guid id)
        {
            var albumFromDB = _uow.Albums.Get(id);

            if (albumFromDB != null)
            {
                _uow.Albums.Delete(id);
                _uow.Save();
            }
        }

        public AlbumDTOToGet GetAlbumById(Guid id)
        {
            try
            {
                var albumFromContext = _uow.Albums.Get(id);
                var mappedAlbum = _mapper.Map<AlbumDTOToGet>(albumFromContext);
                return mappedAlbum;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AlbumDTOToGet> GetAlbums()
        {
            try
            {
                var albumsFromContext = _uow.Albums.GetAll();
                var mappedAlbums = _mapper.Map<IEnumerable<AlbumDTOToGet>>(albumsFromContext).ToList();
                return mappedAlbums;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AlbumDTOToGet> GetAllAlbumsByArtist(Guid artistId)
        {
            try
            {
                var albumsByArtistId = _uow.Albums.Find(a => a.AtristId == artistId);
                var mappedAlbums = _mapper.Map<IEnumerable<AlbumDTOToGet>>(albumsByArtistId).ToList();
                return mappedAlbums;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AlbumDTOToGet> GetAllAlbumsByTime(DateTime time)
        {
            try
            {
                var albumsByTime = _uow.Albums.Find(a => a.Time == time);
                var mappedAlbums = _mapper.Map<IEnumerable<AlbumDTOToGet>>(albumsByTime).ToList();
                return mappedAlbums;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateAlbum(AlbumUpdateDTO albumUpdate, Guid albumId)
        {
            var albumFromDB = _uow.Albums.Get(albumId);

            if (albumFromDB != null)
            {
                if (albumFromDB.Name != albumUpdate.Name)
                {
                    var artistWhoCreate = _uow.Artists.Find(a => a.Id == albumFromDB.AtristId).First();
                    var isAlbumFromArtist = artistWhoCreate.Albums.Any(a => a.Name == albumUpdate.Name);

                    if (!isAlbumFromArtist)
                    {
                        albumFromDB.Name = albumUpdate.Name;
                    }
                }

                _uow.Albums.Update(albumFromDB);
                _uow.Save();
            }
        }
    }
}

using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateAlbum(AlbumCreateDto albumToCreate)
        {
            var artistWhoCreatePlaylist = _unitOfWork.Artists.Get(albumToCreate.AtristId);
            var mappedAlbum = _mapper.Map<Album>(albumToCreate);
            var isAlbumExist = _unitOfWork.Albums.GetAll().Any(album => album.Name == mappedAlbum.Name
            && album.AtristId== mappedAlbum.AtristId);

            if (!isAlbumExist)
            {
                var newAlbum = _unitOfWork.Albums.Create(mappedAlbum);
                artistWhoCreatePlaylist.Albums.Add(newAlbum);
                _unitOfWork.Save();
            }
        }

        public void DeleteAlbum(int albumId)
        {
            _unitOfWork.Albums.Delete(albumId);
            _unitOfWork.Save();
        }

        public AlbumDto GetAlbum(int albumId)
        {
            var albumFromDB = _unitOfWork.Albums.Get(albumId);
            var mappedAlbum = _mapper.Map<AlbumDto>(albumFromDB);
            return mappedAlbum;
        }

        public IEnumerable<AlbumDto> GetAllAlbums()
        {
            var allAlbums = _unitOfWork.Albums.GetAll();
            var mappedAlbums = _mapper.Map<IEnumerable<AlbumDto>>(allAlbums);
            return mappedAlbums;
        }

        public IEnumerable<AlbumDto> GetAllAlbumsByArtist(int artistId)
        {
            var allAlbumsByArtist = _unitOfWork.Albums.GetAll().Where(album => album.AtristId== artistId);
            var mappedAlbums = _mapper.Map<IEnumerable<AlbumDto>>(allAlbumsByArtist);
            return mappedAlbums;
        }

        public void UpdateAlbum(int albumId, AlbumUpdateDto albumToUpdate)
        {
            var album = _unitOfWork.Albums.Get(albumId);
            album.Name = albumToUpdate.Name;

            foreach(Song song in albumToUpdate.Songs)
            {
                var songToGet = album.Songs.FirstOrDefault(song => song.Id == song.Id);

                if (songToGet == null)
                    album.Songs.Add(songToGet);
            }

            _unitOfWork.Albums.Update(album);
            _unitOfWork.Save();
        }

    }
}

using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Services.Interfaces
{
    public interface IAlbumService
    {
        public void CreateAlbum(AlbumCreateDTO albumCreate);
        public void UpdateAlbum(AlbumUpdateDTO albumUpdate,Guid albumId);
        public void DeleteAlbum(Guid id);
        public List<AlbumDTOToGet> GetAlbums();
        public AlbumDTOToGet GetAlbumById(Guid id);
        public List<AlbumDTOToGet> GetAllAlbumsByArtist(Guid artistId);
        public List<AlbumDTOToGet> GetAllAlbumsByTime(DateTime time);
    }
}

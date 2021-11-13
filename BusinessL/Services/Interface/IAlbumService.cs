using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IAlbumService
    {
        public void CreateAlbum(AlbumCreateDto albumToCreate);
        public void DeleteAlbum(int albumId);
        public void UpdateAlbum(int albumId, AlbumUpdateDto albumToUpdate);
        public IEnumerable<AlbumDto> GetAllAlbums();
        public IEnumerable<AlbumDto> GetAllAlbumsByArtist(int artistId);
        public AlbumDto GetAlbum(int albumId);

    }
}

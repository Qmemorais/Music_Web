using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly MusicContext _db;
        private readonly IMapper _mapper;


        public AlbumService(MusicContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public void CreateAlbum(AlbumCreateDto albumToCreate)
        {
            var artistWhoCreatePlaylist = _db.Artists.Find(albumToCreate.AtristId);
            var mappedAlbum = _mapper.Map<Album>(albumToCreate);
            var isAlbumExist = _db.Albums.Any(album => album.Name == mappedAlbum.Name
            && album.AtristId== mappedAlbum.AtristId);

            if (!isAlbumExist)
            {
                _db.Albums.Add(mappedAlbum);
                artistWhoCreatePlaylist.Albums.Add(mappedAlbum);
                _db.Artists.Update(artistWhoCreatePlaylist);
                _db.SaveChanges();
            }
        }

        public void DeleteAlbum(int albumId)
        {
            var album = _db.Albums.Find(albumId);
            _db.Albums.Remove(album);
            _db.SaveChanges();
        }

        public AlbumDto GetAlbum(int albumId)
        {
            var albumFromDB = _db.Albums.Include(x => x.Songs).Where(album => album.Id == albumId).First();
            var mappedAlbum = _mapper.Map<AlbumDto>(albumFromDB);
            return mappedAlbum;
        }

        public IEnumerable<AlbumDto> GetAllAlbums()
        {
            var allAlbums = _db.Albums.Include(x => x.Songs);
            var mappedAlbums = _mapper.Map<IEnumerable<AlbumDto>>(allAlbums);
            return mappedAlbums;
        }

        public IEnumerable<AlbumDto> GetAllAlbumsByArtist(int artistId)
        {
            var allAlbumsByArtist = _db.Albums.Where(album => album.AtristId== artistId);
            var mappedAlbums = _mapper.Map<IEnumerable<AlbumDto>>(allAlbumsByArtist);
            return mappedAlbums;
        }

        public void UpdateAlbum(int albumId, AlbumUpdateDto albumToUpdate)
        {
            var album = _db.Albums.Find(albumId);
            album.Name = albumToUpdate.Name;
            _db.Albums.Update(album);
            _db.SaveChanges();
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Services.Interface;
using BusinessLayer.Models;
using DataLayer.Models;
using AutoMapper;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class SongService : ISongService
    {
        private readonly MusicContext _db;
        private readonly IMapper _mapper;


        public SongService(MusicContext context,IMapper mapper)
        {
            _db=context;
            _mapper = mapper;
        }
        public void CreateSong(SongCreateDto songToCreate)
        {
            var createSong = _mapper.Map<Song>(songToCreate);
            var isSongExist = _db.Songs.Any(song => song.Equals(createSong));

            if(!isSongExist)
            {
                var song = _db.Songs.Add(createSong);
                _db.SaveChanges();
            }
        }

        public void DeleteSong(int songId)
        {
            var song = _db.Songs.Find(songId);

            if (song != null)
            {
                _db.Songs.Remove(song);
                _db.SaveChanges();
            }
        }

        public IEnumerable<SongDto> GetAllSongsByPlaylist(int playlistId)
        {
            var playlistToGetSongs = _db.Playlists.Find(playlistId);
            var songFromPlaylist = _db.Songs.Where(song => song.Playlists.Contains(playlistToGetSongs));
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromPlaylist);
            return mappedSongs;
        }

        public IEnumerable<SongDto> GetAllSongs()
        {
            var songFromDB = _db.Songs.Include(x => x.Playlists);
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromDB);
            return mappedSongs;
        }

        public SongDto GetSongById(int songId)
        {
            var songFromDB = _db.Songs.Include(x => x.Playlists).First(s => s.Id==songId);
            var mappedSong = _mapper.Map<SongDto>(songFromDB);
            return mappedSong;
        }

        public void UpdateSong(int songId, SongUpdateDto songToUpdate)
        {
            var song = _db.Songs.Find(songId);
            song.Name = songToUpdate.Name;
            _db.Songs.Update(song);
            _db.SaveChanges();
        }

        public IEnumerable<SongDto> GetAllSongsByArtist(int artistId)
        {
            var songFromArtist = _db.Songs.Where(song => song.ArtistId==artistId);
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromArtist);
            return mappedSongs;
        }

        public IEnumerable<SongDto> GetAllSongsByAlbum(int albumId)
        {
            var songFromAlbum = _db.Songs.Where(song => song.AlbumId == albumId);
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromAlbum);
            return mappedSongs;
        }
    }
}

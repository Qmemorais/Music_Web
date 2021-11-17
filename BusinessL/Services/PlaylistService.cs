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
    public class PlaylistService: IPlaylistService
    {
        private readonly MusicContext _db;
        private readonly IMapper _mapper;


        public PlaylistService(MusicContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public void AddSongToPlaylist(int playlistId, int songId)
        {
            var playlist = _db.Playlists.Include(x => x.Songs).Where(s => s.Id == playlistId).First();
            var isSongExist = playlist.Songs.Any(song => song.Id == songId);

            if (!isSongExist)
            {
                var song = _db.Songs.Find(songId);
                playlist.Songs.Add(song);
                _db.Playlists.Update(playlist);
                _db.Songs.Update(song);
                _db.SaveChanges();
            }
        }

        public void CreatePlaylist(PlaylistCreateDto playlistToCreate)
        {
            var userWhoCreatePlaylist = _db.Users.Find(playlistToCreate.UserId);
            var playlistCreate = _mapper.Map<Playlist>(playlistToCreate);
            var isPlaylistExisting = _db.Playlists.Any(playlist => playlist.Name == playlistCreate.Name);

            if (!isPlaylistExisting)
            {
                _db.Playlists.Add(playlistCreate);
                userWhoCreatePlaylist.Playlists.Add(playlistCreate);
                playlistCreate.Users.Add(userWhoCreatePlaylist);
                _db.Users.Update(userWhoCreatePlaylist);
                _db.SaveChanges();
            }
        }

        public void DeletePlaylist(int playlistId)
        {
            var playlist = _db.Playlists.Find(playlistId);
            _db.Playlists.Remove(playlist);
            _db.SaveChanges();
        }

        public IEnumerable<PlaylistDto> GetAllPlaylists()
        {
            var allPlaylists = _db.Playlists.Include(x => x.Songs).Include(s => s.Users);
            var mappedPlaylists = _mapper.Map<IEnumerable<PlaylistDto>>(allPlaylists);
            return mappedPlaylists;
        }

        public IEnumerable<PlaylistDto> GetAllPlaylistsBySong(int songId)
        {
            var songToGetPlaylists = _db.Songs.Find(songId);
            var allPlaylistsBySong = _db.Playlists.Include(x => x.Songs).Where(playlist => playlist.Songs.Contains(songToGetPlaylists));
            var mappedPlaylists = _mapper.Map<IEnumerable<PlaylistDto>>(allPlaylistsBySong);
            return mappedPlaylists;
        }

        public IEnumerable<PlaylistDto> GetAllPlaylistsByUser(int userId)
        {
            var userToGetPlaylists = _db.Users.Find(userId);
            var allPlaylistsByUser = _db.Playlists.Include(s => s.Users).Where(playlist => playlist.Users.Contains(userToGetPlaylists));
            var mappedPlaylists= _mapper.Map<IEnumerable<PlaylistDto>>(allPlaylistsByUser);
            return mappedPlaylists;
        }

        public PlaylistDto GetPlaylist(int playlistId)
        {
            var playlistFromDB = _db.Playlists.Include(x => x.Songs).Include(s => s.Users).Where(playlist => playlist.Id== playlistId).First();
            var mappedPlaylist = _mapper.Map<PlaylistDto>(playlistFromDB);
            return mappedPlaylist;
        }

        public void UpdatePlaylist(int playlistId, PlaylistUpdateDto playlistToUpdate)
        {
            var playlist = _db.Playlists.Find(playlistId);
            playlist.Name = playlistToUpdate.Name;
            _db.Playlists.Update(playlist);
            _db.SaveChanges();
        }
    }
}

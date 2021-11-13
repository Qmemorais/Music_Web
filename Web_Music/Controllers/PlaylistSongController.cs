using AutoMapper;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Web_Music.Models;

namespace Web_Music.Controllers
{
    [Route("[Playlist/{playlistId}/songs]")]
    public class PlaylistSongController : Controller
    {
        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mapper;

        public PlaylistSongController(ISongService songService, IPlaylistService playlistService, IMapper mapper)
        {
            _songService = songService;
            _playlistService = playlistService;
            _mapper = mapper;
        }

        [HttpGet("{playlistId}")]
        [ProducesResponseType(typeof(IEnumerable<SongResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllSongsByPlaylist([FromRoute] int playlistId)
        {
            try
            {
                var getPlaylist = _playlistService.GetPlaylist(playlistId);
                if (getPlaylist == null)
                    return NotFound();

                var allSongsByPlaylist = _songService.GetAllSongsByPlaylist(playlistId);
                if (allSongsByPlaylist == null)
                    return NotFound();

                var getAllSongsByPlaylist = _mapper.Map<IEnumerable<SongResponseModel>>(allSongsByPlaylist);
                return Ok(getAllSongsByPlaylist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{songId}")]
        [ProducesResponseType(typeof(IEnumerable<SongResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllPlaylistsBySong([FromRoute] int songId)
        {
            try
            {
                var getSong = _songService.GetSongById(songId);
                if (getSong == null)
                    return NotFound();

                var allPlaylistsBySong = _playlistService.GetAllPlaylistsBySong(songId);
                if (allPlaylistsBySong == null)
                    return NotFound();

                var getAllSongsByPlaylist = _mapper.Map<IEnumerable<PlaylistResponseModel>>(allPlaylistsBySong);
                return Ok(getAllSongsByPlaylist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult AddPlaylistToUser(int playlistId, int songId)
        {
            try
            {
                var song = _songService.GetSongById(songId);

                if (song == null)
                    return NotFound();

                var playlist = _playlistService.GetPlaylist(playlistId);

                if (playlist == null)
                    return NotFound();

                _playlistService.AddSongToPlaylist(playlistId,songId);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

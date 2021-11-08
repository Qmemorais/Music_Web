using AutoMapper;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web_Music.Models;

namespace Web_Music.Controllers
{
    [Route("[controller]")]
    public class PlaylistSongController : Controller
    {
        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mappedSong;

        public PlaylistSongController(ISongService songService, IPlaylistService playlistService, IMapper mappedSong)
        {
            _songService = songService;
            _playlistService = playlistService;
            _mappedSong = mappedSong;
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
                var getAllSongsByPlaylist = _mappedSong.Map<IEnumerable<SongResponseModel>>(allSongsByPlaylist);
                return Ok(getAllSongsByPlaylist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

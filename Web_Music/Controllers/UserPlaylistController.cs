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
    public class UserPlaylistController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly IUserService _userService;
        private readonly IMapper _mappedPlaylist;


        public UserPlaylistController(IPlaylistService playlistService, IUserService userService, IMapper mappedPlaylist)
        {
            _playlistService = playlistService;
            _userService = userService;
            _mappedPlaylist = mappedPlaylist;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(IEnumerable<PlaylistResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllPlaylistsByUser([FromRoute] int userId)
        {
            try
            {
                var getUser = _userService.GetUser(userId);
                if (getUser == null)
                    return NotFound();
                var allPlaylistsByUser = _playlistService.GetAllPlaylistsByUser(userId);
                if (allPlaylistsByUser == null)
                    return NotFound();
                var getPlaylistsByUser = _mappedPlaylist.Map<IEnumerable<PlaylistResponseModel>>(allPlaylistsByUser);
                return Ok(getPlaylistsByUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

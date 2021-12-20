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
    [ApiController]
    [Route("User/{userId}/playlists")]
    public class UserPlaylistController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public UserPlaylistController(IPlaylistService playlistService, IUserService userService, IMapper mapper)
        {
            _playlistService = playlistService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
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

                var mappedPlaylistsByUser = _mapper.Map<IEnumerable<PlaylistResponseModel>>(allPlaylistsByUser);

                return Ok(mappedPlaylistsByUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{playlistId}")]
        public IActionResult AddPlaylistToUser([FromRoute] int userId, [FromRoute] int playlistId)
        {
            try
            {
                var user = _userService.GetUser(userId);

                if (user == null)
                    return NotFound();

                var playlist = _playlistService.GetPlaylist(playlistId);

                if (playlist == null)
                    return NotFound();

                _userService.AddPlaylistToUser(userId, playlistId);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{playlistId}")]
        [ProducesResponseType(typeof(IEnumerable<PlaylistResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllUsersByPlaylist([FromRoute] int playlistId)
        {
            try
            {
                var getPlaylist = _playlistService.GetPlaylist(playlistId);

                if (getPlaylist == null)
                    return NotFound();

                var allUsersByPlaylist = _userService.GetAllUsersByPlaylist(playlistId);

                if (allUsersByPlaylist == null)
                    return NotFound();

                var mappedUsersByPlaylist = _mapper.Map<IEnumerable<UserResponseModel>>(allUsersByPlaylist);
                return Ok(mappedUsersByPlaylist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

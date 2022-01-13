using AutoMapper;
using BusinessLayer.Services.Interfaces;
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
        public IActionResult GetAllPlaylistsByUser([FromRoute] Guid userId)
        {
            try
            {
                var getUser = _userService.GetUserById(userId);

                if (getUser == null)
                    return NotFound("NotFound User");

                var allPlaylistsByUser = _playlistService.GetAllPlaylistsByUser(userId);

                if (allPlaylistsByUser == null)
                    return NotFound("NotFound Playlists");

                var mappedPlaylistsByUser = _mapper.Map<IEnumerable<PlaylistResponseModel>>(allPlaylistsByUser);

                return Ok(mappedPlaylistsByUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{playlistId}")]
        public IActionResult AddPlaylistToUser([FromRoute] Guid userId, [FromRoute] Guid playlistId)
        {
            try
            {
                var user = _userService.GetUserById(userId);

                if (user == null)
                    return NotFound("NotFound User");

                var playlist = _playlistService.GetPlaylistById(playlistId);

                if (playlist == null)
                    return NotFound("NotFound Playlist");

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
        public IActionResult GetAllUsersByPlaylist([FromRoute] Guid playlistId)
        {
            try
            {
                var getPlaylist = _playlistService.GetPlaylistById(playlistId);

                if (getPlaylist == null)
                    return NotFound("NotFound Playlist");

                var allUsersByPlaylist = _userService.GetUsersByPlaylist(playlistId);

                if (allUsersByPlaylist == null)
                    return NotFound("NotFound Users");

                var mappedUsersByPlaylist = _mapper.Map<IEnumerable<UserResponseModel>>(allUsersByPlaylist);
                return Ok(mappedUsersByPlaylist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{playlistId}")]
        public IActionResult DeletePlaylistAsOwner([FromRoute] Guid userId, [FromRoute] Guid playlistId)
        {
            try
            {
                var user = _userService.GetUserById(userId);

                if (user == null)
                    return NotFound("NotFound User");

                var Playlist = _playlistService.GetPlaylistById(playlistId);

                if (Playlist == null)
                    return NotFound("NotFound Playlist");

                _playlistService.DeletePlaylistAsOwner(userId,playlistId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{playlistId}")]
        public IActionResult DeletePlaylistFromUserList([FromRoute] Guid userId, [FromRoute] Guid playlistId)
        {
            try
            {
                var user = _userService.GetUserById(userId);

                if (user == null)
                    return NotFound("NotFound User");

                var Playlist = _playlistService.GetPlaylistById(playlistId);

                if (Playlist == null)
                    return NotFound("NotFound Playlist");

                _userService.RemovePlaylistFromListOfUser(userId, playlistId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

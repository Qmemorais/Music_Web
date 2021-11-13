using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Web_Music.Models;

namespace Web_Music.Controllers
{
    [Route("[controller]s")]
    public class PlaylistController: Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mapper;


        public PlaylistController(IPlaylistService playlistService, IMapper mapper)
        {
            _playlistService = playlistService;
            _mapper = mapper;
        }

        [HttpGet("{playlistId}")]
        [ProducesResponseType(typeof(PlaylistResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetPlaylistById([FromRoute] int playlistId)
        {
            try
            {
                var playlist = _playlistService.GetPlaylist(playlistId);
                if (playlist == null)
                    return NotFound();

                var mappedPlaylist = _mapper.Map<PlaylistResponseModel>(playlist);
                return Ok(mappedPlaylist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlaylistResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            try
            {
                var playlists = _playlistService.GetAllPlaylists();
                if (playlists == null)
                    return NotFound();

                var mappedPlaylists = _mapper.Map<IEnumerable<UserResponseModel>>(playlists);

                return Ok(mappedPlaylists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreatePlaylist([FromBody] PlaylistCreateRequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                    return BadRequest();

                var mappedPlaylist = _mapper.Map<PlaylistCreateDto>(requestModel);
                _playlistService.CreatePlaylist(mappedPlaylist);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{playlistId}")]
        public IActionResult DeletePlaylist([FromRoute] int playlistId)
        {
            try
            {
                var playlist = _playlistService.GetPlaylist(playlistId);
                if (playlist == null)
                    return NotFound();

                _playlistService.DeletePlaylist(playlistId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{playlistId}")]
        public IActionResult UpdatePlaylist([FromRoute] int playlistId, [FromBody] PlaylistUpdateRequestModel requestModel)
        {
            try
            {
                var mappedPlaylistToUpdate = _mapper.Map<PlaylistUpdateDto>(requestModel);
                _playlistService.UpdatePlaylist(playlistId, mappedPlaylistToUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

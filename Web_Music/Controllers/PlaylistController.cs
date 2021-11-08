using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Web_Music.Models;

namespace Web_Music.Controllers
{
    [Route("[controller]")]
    public class PlaylistController: Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mappedPlaylists;


        public PlaylistController(IPlaylistService playlistService, IMapper mappedPlaylists)
        {
            _playlistService = playlistService;
            _mappedPlaylists = mappedPlaylists;
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
                var getPlaylist = _mappedPlaylists.Map<PlaylistResponseModel>(playlist);
                return Ok(getPlaylist);
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

                var playlist = _mappedPlaylists.Map<PlaylistCreateDto>(requestModel);
                _playlistService.CreatePlaylist(playlist);

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
                var playlistToUpdate = _mappedPlaylists.Map<PlaylistUpdateDto>(requestModel);
                _playlistService.UpdatePlaylist(playlistId, playlistToUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

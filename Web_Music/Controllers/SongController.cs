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
    public class SongController: Controller
    {
        private readonly ISongService _songService;
        private readonly IMapper _mappedSongs;


        public SongController(ISongService songService, IMapper mappedSongs)
        {
            _songService = songService;
            _mappedSongs = mappedSongs;
        }
        [HttpGet("{songId}")]
        [ProducesResponseType(typeof(SongResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetSongById([FromRoute] int songId)
        {
            try
            {
                var song = _songService.GetSongById(songId);
                if (song == null)
                    return NotFound();
                var getSong = _mappedSongs.Map<SongResponseModel>(song);
                return Ok(getSong);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateSong([FromBody] SongCreateRequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                    return BadRequest();

                var song = _mappedSongs.Map<SongCreateDto>(requestModel);
                _songService.CreateSong(song);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{songId}")]
        public IActionResult DeleteSong([FromRoute] int songId)
        {
            try
            {
                var playlist = _songService.GetSongById(songId);
                if (playlist == null)
                    return NotFound();
                _songService.DeleteSong(songId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
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
    public class SongController: Controller
    {
        private readonly ISongService _songService;
        private readonly IMapper _mapper;


        public SongController(ISongService songService, IMapper mapper)
        {
            _songService = songService;
            _mapper = mapper;
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

                var mappedSong = _mapper.Map<SongResponseModel>(song);
                return Ok(mappedSong);
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

                var mappedSong = _mapper.Map<SongCreateDto>(requestModel);
                _songService.CreateSong(mappedSong);

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
                var song = _songService.GetSongById(songId);
                if (song == null)
                    return NotFound();
                _songService.DeleteSong(songId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SongResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllSongs()
        {
            try
            {
                var songs = _songService.GetAllSongs();
                if (songs == null)
                    return NotFound();

                var mappedSongs = _mapper.Map<IEnumerable<SongResponseModel>>(songs);

                return Ok(mappedSongs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{songId}")]
        public IActionResult UpdateSong([FromRoute] int songId, [FromBody] SongUpdateRequestModel requestModel)
        {
            try
            {
                var mappedSongToUpdate = _mapper.Map<SongUpdateDto>(requestModel);
                _songService.UpdateSong(songId, mappedSongToUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
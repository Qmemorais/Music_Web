using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web_Music.Models;
using AutoMapper;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace Web_Music.Controllers
{
    [Route("[controller]s")]
    //[Route("[Album/{albumId}/songs]")]
    public class AlbumSongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly IMapper _mapper;


        public AlbumSongController(ISongService songService, IMapper mapper)
        {
            _songService = songService;
            _mapper = mapper;
        }

        [HttpGet("{albumId}")]
        [ProducesResponseType(typeof(IEnumerable<SongResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllSongsByAlbum([FromRoute] int albumId)
        {
            try
            {
                var songs = _songService.GetAllSongsByAlbum(albumId);
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
    }
}

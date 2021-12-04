using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web_Music.Models;
using AutoMapper;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace Web_Music.Controllers
{
    [ApiController]
    [Route("Album/{albumId}/songs")]
    public class AlbumSongController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly ISongService _songService;
        private readonly IMapper _mapper;


        public AlbumSongController(IAlbumService albumService,ISongService songService, IMapper mapper)
        {
            _albumService = albumService;
            _songService = songService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SongResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllSongsByAlbum([FromRoute] int albumId)
        {
            try
            {
                var album = _albumService.GetAlbum(albumId);
                if (album == null)
                    return NotFound();

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

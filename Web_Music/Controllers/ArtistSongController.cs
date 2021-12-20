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
    [Route("Artist/{artistId}/songs")]
    public class ArtistSongController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly ISongService _songService;
        private readonly IMapper _mapper;


        public ArtistSongController(IArtistService artistService, ISongService songService, IMapper mapper)
        {
            _artistService = artistService;
            _songService = songService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SongResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllSongsByArtist([FromRoute] int artistId)
        {
            try
            {
                var artist = _artistService.GetArtist(artistId);
                if(artist == null)
                    return NotFound();

                var songs = _songService.GetAllSongsByArtist(artistId);
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

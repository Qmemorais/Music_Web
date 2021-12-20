using AutoMapper;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web_Music.Models;

namespace Web_Music.Controllers
{
    [ApiController]
    [Route("Artist/{artistId}/albums")]
    public class AlbumArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;


        public AlbumArtistController(IArtistService artistService,IAlbumService albumService, IMapper mapper)
        {
            _artistService = artistService;
            _albumService = albumService;
            _mapper = mapper;
        }

        [HttpGet("{albumId}")]
        [ProducesResponseType(typeof(IEnumerable<AlbumResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllAlbumsByArtist([FromRoute] int artistId)
        {
            try
            {
                var artist = _artistService.GetArtist(artistId);
                if (artist == null)
                    return NotFound();

                var albums = _albumService.GetAllAlbumsByArtist(artistId);
                if (albums == null)
                    return NotFound();

                var mappedAlbums = _mapper.Map<IEnumerable<AlbumResponseModel>>(albums);

                return Ok(mappedAlbums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

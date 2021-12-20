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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet("{artistId}")]
        [ProducesResponseType(typeof(ArtistResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetArtistById([FromRoute] int artistId)
        {
            try
            {
                var artist = _artistService.GetArtist(artistId);
                if (artist == null)
                    return NotFound();

                var mappedArtist = _mapper.Map<ArtistResponseModel>(artist);
                return Ok(mappedArtist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArtistResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllArtists()
        {
            try
            {
                var artists = _artistService.GetAllArtists();
                if (artists == null)
                    return NotFound();

                var mappedArtists = _mapper.Map<IEnumerable<ArtistResponseModel>>(artists);

                return Ok(mappedArtists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateArtist([FromBody] ArtistCreateRequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                    return BadRequest();

                var mappedArtist = _mapper.Map<ArtistCreateDto>(requestModel);
                _artistService.CreateArtist(mappedArtist);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{artistId}")]
        public IActionResult DeleteArtist([FromRoute] int artistId)
        {
            try
            {
                var artist = _artistService.GetArtist(artistId);
                if (artist == null)
                    return NotFound();

                _artistService.DeleteArtist(artistId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{artistId}")]
        public IActionResult UpdateArtist([FromRoute] int artistId, [FromBody] ArtistUpdateRequestModel requestModel)
        {
            try
            {
                var mappedArtistToUpdate = _mapper.Map<ArtistUpdateDto>(requestModel);
                _artistService.UpdateArtist(artistId, mappedArtistToUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Web_Music.Models;

namespace Web_Music.Controllers
{
    [Route("[controller]s")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        public AlbumController(IAlbumService albumService, IMapper mapper)
        {
            _albumService = albumService;
            _mapper = mapper;
        }

        [HttpGet("{albumId}")]
        [ProducesResponseType(typeof(AlbumResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetAlbumById([FromRoute] Guid albumId)
        {
            try
            {
                var album = _albumService.GetAlbumById(albumId);

                if (album == null)
                    return NotFound();

                var mappedAlbum = _mapper.Map<AlbumResponseModel>(album);
                return Ok(mappedAlbum);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlbumResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllAlbums()
        {
            try
            {
                var albums = _albumService.GetAlbums();

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

        [HttpPost]
        public IActionResult CreateAlbum([FromBody] AlbumCreateRequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                    return BadRequest();

                var mappedAlbum = _mapper.Map<AlbumCreateDTO>(requestModel);
                _albumService.CreateAlbum(mappedAlbum);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{albumId}")]
        public IActionResult DeleteAlbum([FromRoute] Guid albumId)
        {
            try
            {
                var album = _albumService.GetAlbumById(albumId);

                if (album == null)
                    return NotFound();

                _albumService.DeleteAlbum(albumId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{albumId}")]
        public IActionResult UpdateAlbum([FromRoute] Guid albumId, [FromBody] AlbumUpdateRequestModel requestModel)
        {
            try
            {
                var mappedAlbumToUpdate = _mapper.Map<AlbumUpdateDTO>(requestModel);
                _albumService.UpdateAlbum(mappedAlbumToUpdate, albumId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{time}")]
        [ProducesResponseType(typeof(AlbumResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetAlbumsByTime([FromRoute] DateTime time)
        {
            try
            {
                var album = _albumService.GetAllAlbumsByTime(time);

                if (album == null)
                    return NotFound();

                var mappedAlbum = _mapper.Map<AlbumResponseModel>(album);
                return Ok(mappedAlbum);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

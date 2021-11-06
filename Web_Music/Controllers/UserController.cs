using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Web_Music.Models;

namespace Web_Music.Controls
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: Controller
    {
        private readonly IUserService _userService;
        private IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [Route("Get/{id:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var user = _userService.GetUser(id);
                var getUser = _mapper.Map<UserResponseModel>(user);
                return Ok(getUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [Route("GetAll")]
        [HttpGet]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUser();
                var getUser = _mapper.Map<IEnumerable<UserResponseModel>>(users);
                return Ok(getUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [Route("Create")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var user = _mapper.Map<UserCreateDto>(model);
                _userService.Create(user);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [Route("Delete/{id:int}")]
        [HttpDelete]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [Route("Update")]
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserResponseModel requestModel)
        {
            try
            {
                _userService.Update(_mapper.Map<UserUpdateDto>(requestModel));
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

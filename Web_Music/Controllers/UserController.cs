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
    public class UserController: Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetUserById([FromRoute] int userId)
        {
            try
            {
                var user = _userService.GetUser(userId);
                if (user == null)
                    return NotFound();

                var mappedUser = _mapper.Map<UserResponseModel>(user);

                return Ok(mappedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                if (users == null)
                    return NotFound();

                var mappedUsers = _mapper.Map<IEnumerable<UserResponseModel>>(users);

                return Ok(mappedUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                    return BadRequest();

                var mappedUser = _mapper.Map<UserCreateDto>(requestModel);
                _userService.CreateUser(mappedUser);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser([FromRoute] int userId)
        {
            try
            {
                var user = _userService.GetUser(userId);
                if(user == null)
                    return NotFound();

                _userService.DeleteUser(userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser([FromRoute] int userId, [FromBody] UserUpdateRequestModel requestModel)
        {
            try
            {
                var mappedUserToUpdate = _mapper.Map<UserUpdateDto>(requestModel);
                _userService.UpdateUser(userId, mappedUserToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

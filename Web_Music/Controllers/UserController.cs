using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserUpdateDto, UserUpdateRequestModel>());
                _mapper = new Mapper(config);
                var getUser = _mapper.Map<UserUpdateRequestModel>(_userService.GetUser(id));
                return Ok(getUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var listOfUsers = _userService.GetAllUser();
                return Ok(listOfUsers);
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

                var user = _mapper.Map<UserCreateDto>(requestModel);
                _userService.Create(user);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpDelete("id")]
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
        [HttpPut("id")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] UserUpdateRequestModel requestModel)
        {
            try
            {
                UserUpdateRequestModel user = _mapper.Map<UserUpdateDto, UserUpdateRequestModel>(_userService.GetUser(id));
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

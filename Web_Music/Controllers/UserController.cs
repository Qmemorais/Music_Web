using BusinessLayer.Services.Interface;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Web_Music.Controls
{
    public class UserController: Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(int id)
        {
            return Content($"Hello {_userService.GetUser(id).Name}");
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var ListOfUsers = _userService.GetAllUser();
                return Ok(ListOfUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] User CreateNewUser)//string email, string name, string surname
        {
            try
            {
                _userService.Create(CreateNewUser);
                return Ok($"Welcome {CreateNewUser.Email}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost]
        public IActionResult DeleteUser([FromBody] User UserToDelete)
        {
            try
            {
                var IdUserToDelete = UserToDelete.Id;
                _userService.Delete(IdUserToDelete);
                return Ok("User deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPut]
        public IActionResult UpdateUser([FromBody] User UserToUpdate)
        {
            try
            {
                _userService.Update(UserToUpdate);
                return Ok("Update succesfull. To view new info get HomePage");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

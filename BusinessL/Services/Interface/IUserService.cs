using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IUserService
    {
        public void CreateUser(UserCreateDto userToCreate);
        public void DeleteUser(int userId);
        public void UpdateUser(int userId, UserUpdateDto userToUpdate);
        public UserDto GetUser(int userId);
        public IEnumerable<UserDto> GetAllUsers();
    }
}

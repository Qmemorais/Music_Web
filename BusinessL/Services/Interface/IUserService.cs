using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IUserService
    {
        public void Create(UserCreateDto userToCreate);
        public void Delete(int id);
        public void Update( UserUpdateDto userToUpdate);
        public UserUpdateDto GetUser(int id);
        public IEnumerable<UserUpdateDto> GetAllUser();
    }
}

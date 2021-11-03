using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IUserService
    {
        public void Create(User UserToCreate);
        public void Delete(int id);
        public void Update(User UserToUpdate);
        public User GetUser(int id);
        public IEnumerable<User> GetAllUser();
    }
}

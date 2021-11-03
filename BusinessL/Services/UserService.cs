using BusinessLayer.Services.Interface;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(User UserToCreate)
        {
            var AnyUser = _unitOfWork.Users.GetAll().Any(user => user.Email == UserToCreate.Email);
            if (AnyUser.Equals(false))
            {
                _unitOfWork.Users.Create(UserToCreate);
                _unitOfWork.Save();
            }
        }
        public void Delete(int id)
        {
            _unitOfWork.Users.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<User> GetAllUser()
        {
            return _unitOfWork.Users.GetAll();
        }

        public User GetUser(int id)
        {
            return _unitOfWork.Users.Get(id);
        }

        public void Update(User UserToUpdate)
        {
            _unitOfWork.Users.Update(UserToUpdate);
            _unitOfWork.Save();
        }

    }
}

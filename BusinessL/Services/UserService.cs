using DataLayer.Models;
using DataLayer.Repository.Interface;

namespace BusinessLayer.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateUser(int id, string email, string name = "", string surname = "")
        {
            if (_unitOfWork.Generic.Get(id) == null)
            {
                _unitOfWork.Generic.Create(new User { Email = email, Name = name, Surname = surname });
                _unitOfWork.Save();
                return true;
            }
            else return false;
        }
        public bool DeleteUser(int id)
        {
            _unitOfWork.Generic.Delete(id);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateUserName(int id, string name, string surname, string email)
        {
            User user = (User)_unitOfWork.Generic.Get(id);
            user.Name = name ?? user.Name;
            user.Surname = surname ?? user.Surname;
            user.Email = email ?? user.Email;
            _unitOfWork.Generic.Update(user);
            _unitOfWork.Save();
            return true;
        }
    }
}

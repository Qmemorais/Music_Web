using BusinessLayer.Services.Interface;
using DataLayer.Models;
using DataLayer.Repository.Interface;
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
        public bool Create(int id, string email, string name = "", string surname = "")
        {
            if(_unitOfWork.User.GetAll().Where(user => user.Email == email) == null)
            {
                _unitOfWork.User.Create(new User { Email = email, Name = name, Surname = surname });
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
        public bool Delete(int id)
        {
            _unitOfWork.User.Delete(_unitOfWork.User.Get(id));
            _unitOfWork.Save();
            return true;
        }

        public bool Reemail(int id, string newEmail)
        {
            if (_unitOfWork.User.GetAll().Where(user => user.Email == newEmail) == null)
            {
                User user = _unitOfWork.User.Get(id);
                user.Email = newEmail;
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool Rename(int id, string newName)
        {
            User user = _unitOfWork.User.Get(id);
            user.Name = newName;
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();
            return true;
        }

        public bool Resurname(int id, string newSurname)
        {
            User user = _unitOfWork.User.Get(id);
            user.Surname = newSurname;
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();
            return true;
        }
    }
}

using AutoMapper;
using BusinessLayer.Models;
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
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Create(UserCreateDto userToCreate)
        {
            var createUser = _mapper.Map<User>(userToCreate);
            var anyUser = _unitOfWork.Users.GetAll().Any(user => user.Email == createUser.Email);
            if (anyUser.Equals(false))
            {
                _unitOfWork.Users.Create(createUser);
                _unitOfWork.Save();
            }
        }

        public void Delete(int id)
        {
            _unitOfWork.Users.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<UserUpdateDto> GetAllUser()
        {
            var users = _unitOfWork.Users.GetAll();
            var usersToGet = _mapper.Map<IEnumerable<UserUpdateDto>>(users);
            return usersToGet;
        }

        public UserUpdateDto GetUser(int id)
        {
            var userFromDB = _unitOfWork.Users.Get(id);
            var user = _mapper.Map<UserUpdateDto>(userFromDB);
            return user;
        }

        public void Update(UserUpdateDto userToUpdate)
        {
            User user = _mapper.Map<User>(userToUpdate);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }
    }
}

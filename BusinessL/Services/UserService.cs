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
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(UserCreateDto userToCreate)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserCreateDto,User>());
            var mapper = new Mapper(config);
            User CreateUser = mapper.Map<UserCreateDto, User>(userToCreate);
            var anyUser = _unitOfWork.Users.GetAll().Any(user => user.Email == CreateUser.Email);
            if (anyUser.Equals(false))
            {
                _unitOfWork.Users.Create(CreateUser);
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserUpdateDto>());
            var mapper = new Mapper(config);
            var users = mapper.Map<List<UserUpdateDto>>(_unitOfWork.Users.GetAll());
            return users;
        }

        public UserUpdateDto GetUser(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserUpdateDto>());
            var mapper = new Mapper(config);
            var userFromDB = _unitOfWork.Users.Get(id);
            var user = mapper.Map<UserUpdateDto>(userFromDB);
            return user;
        }

        public void Update(UserUpdateDto userToUpdate)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserUpdateDto, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<UserUpdateDto, User>(userToUpdate);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }
    }
}

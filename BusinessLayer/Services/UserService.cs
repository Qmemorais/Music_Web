using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public void AddPlaylistToUser(Guid userId, Guid playlistId)
        {
            var playlist = _uow.Playlists.Get(playlistId);
            var user = _uow.Users.Get(userId);
            var isPlaylistExistInUser = user.Playlists.Any(p => p.Id == playlistId);

            if (!isPlaylistExistInUser)
            {
                user.Playlists.Add(playlist);
                playlist.Users.Add(user);
                _uow.Users.Update(user);
                _uow.Playlists.Update(playlist);
                _uow.Save();
            }
        }

        public void CreateUser(UserCreateDTO userCreate)
        {
            var mappedUser = _mapper.Map<User>(userCreate);
            var anyUserEmail = _uow.Users.Find(u => u.Email == mappedUser.Email).First();

            if (anyUserEmail == null)
            {
                _uow.Users.Create(mappedUser);
                _uow.Save();
            }

        }

        public void DeleteUser(Guid id)
        {
            var user = _uow.Users.Get(id);

            if (user != null)
            {
                _uow.Users.Delete(id);
                _uow.Save();
            }
        }

        public UserDTOToGet GetUserById(Guid id)
        {
            try
            {
                var userFromContext = _uow.Users.Get(id);
                var mappedUser = _mapper.Map<UserDTOToGet>(userFromContext);
                return mappedUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<UserDTOToGet> GetUsers()
        {
            throw new NotImplementedException();
        }

        public List<UserDTOToGet> GetUsersByAge(int age)
        {
            throw new NotImplementedException();
        }

        public List<UserDTOToGet> GetUsersByCountry(string countryName)
        {
            throw new NotImplementedException();
        }

        public List<UserDTOToGet> GetUsersByPlaylist(Guid playlistId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserUpdateDTO userUpdate, Guid id)
        {
            var user = _uow.Users.Get(id);

            if (user != null)
            {
                user.Country = userUpdate.Country;
                user.Name = userUpdate.Name;
                user.Surname = userUpdate.Surname;
                user.Age = userUpdate.Age;

                if (user.Email != userUpdate.Email)
                {
                    var anyUserEmail = _uow.Users.Find(u => u.Email == userUpdate.Email).First();

                    if (anyUserEmail == null)
                        user.Email = userUpdate.Email;
                }

                _uow.Users.Update(user);
                _uow.Save();
            }
        }
    }
}
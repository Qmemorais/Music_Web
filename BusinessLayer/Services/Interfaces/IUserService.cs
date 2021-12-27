using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        public void CreateUser(UserCreateDTO userCreate);
        public void UpdateUser(UserUpdateDTO userUpdate, Guid id);
        public void DeleteUser(Guid id);
        public List<UserDTOToGet> GetUsers();
        public UserDTOToGet GetUserById(Guid id);
        public void AddPlaylistToUser(Guid userId, Guid playlistId);
        public List<UserDTOToGet> GetUsersByPlaylist(Guid playlistId);
        public List<UserDTOToGet> GetUsersByCountry(string countryName);
        public List<UserDTOToGet> GetUsersByAge(int age);

    }
}

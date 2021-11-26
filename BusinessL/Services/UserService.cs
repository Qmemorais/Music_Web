using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly MusicContext _db;
        private readonly IMapper _mapper;


        public UserService(MusicContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public void CreateUser(UserCreateDto userToCreate)
        {
            var mappedUser = _mapper.Map<User>(userToCreate);
            var anyUser = _db.Users.Any(user => user.Email == mappedUser.Email);

            if (!anyUser)
            {
                _db.Users.Add(mappedUser);
                _db.SaveChanges();
            }
        }

        public void AddPlaylistToUser(int userId, int playlistId)
        {
            var playlist = _db.Playlists.Include(y => y.Users).Where(q => q.Id == playlistId).First();
            var user = _db.Users.Include(x => x.Playlists).Where(s => s.Id == userId).First();
            var isExistingPlaylist = user.Playlists.Any(x => x.Id == playlistId);
            if (!isExistingPlaylist)
            {

                if (playlist != null)
                {
                    user.Playlists.Add(playlist);
                    playlist.Users.Add(user);
                    _db.Playlists.Update(playlist);
                    _db.Users.Update(user);
                    _db.SaveChanges();
                }
            }
        }

        public void DeleteUser(int userId)
        {
            var users = _db.Users.First(x => x.Id == userId);

            if (users != null)
            {
                _db.Users.Remove(users);
                _db.SaveChanges();
            }
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _db.Users.Include(x => x.Playlists);
            var mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);
            return mappedUsers;
        }

        public IEnumerable<UserDto> GetAllUsersByPlaylist(int playlistId)
        {
            try
            {
                var playlistToGetUsers = _db.Playlists.Include(x => x.Users).Where(s => s.Id == playlistId).First();
                var users = playlistToGetUsers.Users;
                var mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);
                return mappedUsers;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public UserDto GetUser(int userId)
        {
            try
            {
                var userFromDB = _db.Users.Include(x => x.Playlists).Where(s => s.Id == userId).First();
                var mappedUser = _mapper.Map<UserDto>(userFromDB);
                return mappedUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateUser(int userId, UserUpdateDto userToUpdate)
        {
            var user = _db.Users.First(x => x.Id == userId);

            if (user != null)
            {
                user.Name = userToUpdate.Name;
                user.Surname = userToUpdate.Surname;
                user.Email = userToUpdate.Email;
                _db.Users.Update(user);
                _db.SaveChanges();
            }
        }
    }
}

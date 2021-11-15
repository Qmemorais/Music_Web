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

        public void CreateUser(UserCreateDto userToCreate)
        {
            var mappedUser = _mapper.Map<User>(userToCreate);
            var anyUser = _unitOfWork.Users.GetAll().Any(user => user.Email == mappedUser.Email);

            if (!anyUser)
            {
                _unitOfWork.Users.Create(mappedUser);
                _unitOfWork.Save();
            }
        }

        public void AddPlaylistToUser(int userId, int playlistId)
        {
            var user = _unitOfWork.Users.Get(userId);
            var isExistingPlaylist = user.Playlists.Any(x => x.Id == playlistId);
            if (!isExistingPlaylist)
            {
                var playlist = _unitOfWork.Playlists.Get(playlistId);
                if (playlist != null)
                {
                    user.Playlists.Add(playlist);
                    playlist.Users.Add(user);
                    _unitOfWork.Playlists.Update(playlist);
                    _unitOfWork.Users.Update(user);
                    _unitOfWork.Save();
                }
            }
        }

        public void DeleteUser(int userId)
        {
            _unitOfWork.Users.Delete(userId);
            _unitOfWork.Save();
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _unitOfWork.Users.GetAll();
            var mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);
            return mappedUsers;
        }

        public IEnumerable<UserDto> GetAllUsersByPlaylist(int playlistId)
        {
            var allUsers = GetAllUsers().Where(users => users.PlaylistsId.Contains(playlistId));
            return allUsers;
        }

        public UserDto GetUser(int userId)
        {
            var userFromDB = _unitOfWork.Users.Get(userId);
            var mappedUser = _mapper.Map<UserDto>(userFromDB);
            return mappedUser;
        }

        public void UpdateUser(int userId, UserUpdateDto userToUpdate)
        {
            var user = _unitOfWork.Users.Get(userId);
            user.Name = userToUpdate.Name;
            user.Surname = userToUpdate.Surname;
            user.Email = userToUpdate.Email;

            foreach(int playlistId in userToUpdate.PlaylistsId)
            {
                var playlist = user.Playlists.FirstOrDefault(playlist => playlist.Id == playlistId);

                if (playlist == null)
                    user.Playlists.Add(playlist);
            }

            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }
    }
}

using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web_Music.Controls
{
    public class UserController: Controller
    {
        public UserController()
        {
        }
        public void GetData(string login,string passwd)
        {//to login to acc
            //unitOfWork.Playlist.GetAll(unitOfWork.User.Find(login, passwd).Id)
        }
    }
}

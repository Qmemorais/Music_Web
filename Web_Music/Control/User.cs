using BusinessL.UnitOfWork;
using BusinessL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web_Music.Control
{
    public class User : Controller
    {
        UnitOfWork unitOfWork;
        public User()
        {
            unitOfWork = new UnitOfWork();
        }
        public IActionResult GetData(string login,string passwd)
        {//to login to acc
            //unitOfWork.Playlist.GetAll(unitOfWork.User.Find(login, passwd).Id)
            try
            {
                return Json(unitOfWork.Playlist.GetAll(unitOfWork.User.Find(login, passwd).Id));
            }
            catch
            {
                return Content("Wrong data. Please try again");
            }
        }
    }
}

using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web_Music.Controls
{
    public class UserController: Controller
    {
        PlaylistService playlistService;
        public UserController()
        {
        }
        public IActionResult GetData()
        {
            return Content("dfg");
        }
        public JsonResult GetUser(int id)
        {
            return Json(playlistService.GetAll(id));
        }
    }
}

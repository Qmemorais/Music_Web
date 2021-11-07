using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
namespace Web_Music.Controllers
{
    public class PlaylistController: Controller
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }
        public IActionResult GetInfo(int id)
        {
            //var listPlaylists = _playlistService.GetAll(id);
            return Ok();
        }
    }
}

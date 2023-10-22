using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TablonAnuncios.Controllers
{
    public class AdsController : Controller
    {
        [Authorize]
        public IActionResult index()
        {
            return View("createAd");
        }
        [HttpPost]
        [Authorize]
        public IActionResult create()
        {
            return View();
        }
    }
}

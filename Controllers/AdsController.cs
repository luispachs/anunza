using Microsoft.AspNetCore.Mvc;

namespace TablonAnuncios.Controllers
{
    public class AdsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

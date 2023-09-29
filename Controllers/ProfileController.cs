using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using TablonAnuncios.Models;

namespace TablonAnuncios.Controllers
{
    public class ProfileController : Controller
    {
        private anunzaContext _context;
        public ProfileController(anunzaContext context)
        {
            _context = context;
        }    
        [Authorize]
        public IActionResult Index()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault();
            if (userEmail == null) { }
            return View("profile");
        }
        [Authorize]
        public IActionResult editAction(User user) {

            return View("edit");
        }
    }
}

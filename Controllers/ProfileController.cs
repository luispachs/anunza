using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TablonAnuncios.DataModels;
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
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email);
            if (userEmail == null) { 
                return RedirectToAction("login", "Home");
            }
            profileDataModel data = new profileDataModel();
            data.user= _context.Users.First(elem => elem.Email==userEmail.Value);
            return View("profile",data);
        }
        [Authorize]
        public IActionResult editAction(User user) {

            return View("edit");
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TablonAnuncios.DataModels;
using TablonAnuncios.Models;
using TablonAnuncios.Utils.Tools;

namespace TablonAnuncios.Controllers
{
    public class HomeController : Controller
    {
        private anunzaContext _context;
        private ILogger<HomeController> _logger;
        public HomeController(anunzaContext context,ILogger<HomeController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var states = await _context.States.ToListAsync();
            int? userId = this.HttpContext.Session.GetInt32("id");
            User? user = await this._context.Users.FindAsync(userId);
            IndexDataModel model = new IndexDataModel();
            model.States = states;
            model.User = user;

            return View(model);
        }
       
        public async Task<IActionResult> register()
        {
            RegisterDataModel register = new RegisterDataModel();
            register.State = await _context.States.ToListAsync();
            return View("register", register);
        }
      
        public IActionResult login()
        {
            return View("login");
        }

        [HttpPost]
        public async Task<IActionResult> loginProcess(string user,string password,string returnUrl)
        {
          
            User userObject = _context.Users.Where(u => u.Email == user).First();
            if(userObject == null)
            {
                return View();
            }
            if (Password.compare(password, userObject.Password))
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Email, userObject.Email),
                    new Claim("fullName", userObject.Firstname+" "+userObject.Lastname),
                    new Claim(ClaimTypes.Role, userObject.Role),
                };
                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {

                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),authProperties);
                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Profile");
                }
                return Redirect(returnUrl);
            }
            return Redirect("/Home/login");
        }

        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return LocalRedirect("/");
        }

        [HttpGet("get-cities/{id}")]
        public  IActionResult getCities( int id)
        {   
            var cities =  _context.Cities.Where(c => c.State.Equals(id)).ToListAsync();
            return Json(new { citiesList=cities });
        }
        [HttpPost("new-user")]
        public IActionResult newUserAction()
        {
            var data = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString()); ;
            User newUser = new User();
            try
            {
                if (data != null)
                {
                    if ((data["password-repeat"] == data["password"]) && (!string.IsNullOrEmpty(data["password"])))
                    {
                        newUser.Nit = long.Parse(data["nit"]);
                        newUser.Email = data["email"];
                        newUser.Language = "es";
                        newUser.State = int.Parse(data["estate"]);
                        newUser.Firstname = data["firstname"];
                        newUser.City = int.Parse(data["city"]);
                        newUser.Lastname = data["lastname"];
                        newUser.Phone = data["phone"];
                        newUser.Password = Password.encode(data["password"]);
                        _context.Add(newUser);
                        _context.SaveChanges();
                        return Redirect("/home/login");
                    }
                }
                return Redirect("/home/register");
            }
            catch(Exception e)
            {
                _logger.LogError("Error:{message} => trace => {trace} " , e.Message,e.StackTrace);
                return Redirect("/home/register");
            }
        }
    }

 
}

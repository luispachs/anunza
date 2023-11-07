using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TablonAnuncios.DataModels;
using TablonAnuncios.Models;

namespace TablonAnuncios.Controllers
{
    [Authorize]
    public class AdsController : Controller
    { private anunzaContext _context;
        public AdsController(anunzaContext context) { 
            _context = context;
        }
        
        public IActionResult index()
        {
            var data = new IndexDataModel();
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "id").Value;
            var user = _context.Users.Find(int.Parse(userId));
            var states = _context.States.ToListAsync().Result;

            data.States = states;
            data.User = user;
            return View("createAd",data);
        }
        [HttpPost]
        public  async Task<IActionResult> create()
        {
            string title = Request.Form["title"];
            string description = Request.Form["description"];
            string value = Request.Form["value"];
            string type = Request.Form["type"];
            string city = Request.Form["city"];
            var userId = HttpContext.User.Claims.FirstOrDefault(claim=>claim.Type == "id").Value;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(value) || string.IsNullOrEmpty(type))
            {
                return RedirectToAction("index");
            }

            string uploadFolder = "C:\\Users\\gato1\\OneDrive\\Imágenes\\temporalImages\\"+userId+"\\" ;
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            List<string> files = new List<string>();
            var images = Request.Form.Files;
            foreach (var image in images)
            {
                string fileName = userId +"_"+ title.Replace(" ", "_")+"_"+DateTime.Now.ToFileTime()+image.ContentType.Replace("image/",".");
                files.Add(fileName);
                var ms = new FileStream(Path.Combine(uploadFolder,fileName),FileMode.Create);
                image.CopyToAsync(ms);

            }
            Dictionary<string,object >  config = new Dictionary<string, object>();
            config.Add("status", "enable");
            config.Add("images", files);
            var anuncio = new Anuncio();
            anuncio.Title = title;
            anuncio.Description = description;
            anuncio.Value = float.Parse(value);
            anuncio.UserId = int.Parse(userId);
            anuncio.Config = config.ToString();
            anuncio.City = int.Parse(city);
            _context.Add(anuncio);

            return Redirect("/");
        }
    }
}

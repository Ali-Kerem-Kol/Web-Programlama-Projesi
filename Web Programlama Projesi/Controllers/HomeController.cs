using Microsoft.AspNetCore.Mvc;
using Web_Programlama_Projesi.Data;
using Web_Programlama_Projesi.Models;

namespace Web_Programlama_Projesi.Controllers
{
    public class HomeController : Controller
    {
        // Selamun aleykum beyler
        private readonly KuaforContext _context;

        public HomeController(KuaforContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kullan�c� ad� kontrol�
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Bu kullan�c� ad� zaten al�nm��.");
                return View(model);
            }

            // Yeni kullan�c� olu�turma
            var user = new User
            {
                Username = model.Username,
                Password = model.Password, // Ger�ek uygulamalarda �ifreler hash'lenmelidir!
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Ba�ar�l� kay�t sonras� y�nlendirme
            return RedirectToAction("Login", "Home");
        }

    }
}

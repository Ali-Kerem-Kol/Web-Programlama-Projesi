using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Web_Programlama_Projesi.Data;
using Web_Programlama_Projesi.Models;

namespace Web_Programlama_Projesi.Controllers
{
    public class HomeController : Controller
    {

        private readonly KuaferContext _context;

        public HomeController(KuaferContext context)
        {
            _context = context;
        }


        // Ana sayfa
        public IActionResult Index()
        {
            // Kullan�c�n�n giri� yap�p yapmad���n� kontrol et
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            // Giri� yapm��sa, bilgiyi ViewData'ya g�nder
            if (username != null)
            {
                ViewData["Username"] = username;
                ViewData["Role"] = role;
            }
            else
            {
                ViewData["Username"] = null;
                ViewData["Role"] = null;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //----------------------------Register--------------------------------------------------------
        // Kullan�c� Kay�t Sayfas�na Y�nlendiren GET Aksiyonu
        public IActionResult Register()
        {
            return View();
        }

        // Kullan�c� Kay�t ��lemi Ger�ekle�tiren POST Aksiyonu
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            // Kullan�c� ad� kontrol�
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                ModelState.AddModelError("Username", "Bu kullan�c� ad� zaten al�nm��.");
                return View(user);  // Hata varsa, formu tekrar g�steriyoruz
            }

            // �ifreyi ve di�er verileri veritaban�na kaydediyoruz
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Users.Add(user);  // Yeni kullan�c�y� ekliyoruz
                    await _context.SaveChangesAsync();  // Veritaban�na kaydediyoruz
                    return RedirectToAction("Index", "Home");  // Ana sayfaya y�nlendiriyoruz
                }
                catch (Exception ex)
                {
                    // Hata mesaj� ekliyoruz
                    ModelState.AddModelError("", "Veritaban�na kaydetme i�lemi ba�ar�s�z oldu: " + ex.Message);
                }
            }

            // E�er model ge�erli de�ilse, formu tekrar g�steriyoruz
            return View(user);
        }
        //----------------------------Register--------------------------------------------------------
        
        
        //----------------------------Login--------------------------------------------------------
        // Giri� sayfas�na y�nlendiren GET aksiyonu
        public IActionResult Login()
        {
            return View();
        }

        // Kullan�c� giri� i�lemi
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            // Kullan�c� kontrol� ve �ifre do�rulama
            if (user == null || user.Password != password)
            {
                ModelState.AddModelError(string.Empty, "Ge�ersiz kullan�c� ad� veya �ifre.");
                return View();
            }

            if (user.IsActive == false)
            {
                ModelState.AddModelError(string.Empty, "Hesab�n�z Ask�ya Al�nd�.");
                return View();
            }

            // Kullan�c� bilgilerini session'da sakl�yoruz
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role); // Rol� sakla
            HttpContext.Session.SetInt32("Id", user.Id);

            // ViewData'ya aktar�m
            ViewData["Username"] = user.Username;
            ViewData["Role"] = user.Role;
            ViewData["Id"] = user.Id;

            return RedirectToAction("Index","Home");

        }

        //----------------------------Login--------------------------------------------------------


        //----------------------------Logout--------------------------------------------------------
        // ��k�� metodu
        public IActionResult Logout()
        {
            // Session temizleme
            HttpContext.Session.Clear();

            // ViewData'y� s�f�rla
            ViewData["Username"] = null;
            ViewData["Role"] = null;

            // Ana sayfaya y�nlendirme
            return RedirectToAction("Index");
        }
        //----------------------------Logout--------------------------------------------------------

    }

}

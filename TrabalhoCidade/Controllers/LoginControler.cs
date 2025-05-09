using Microsoft.AspNetCore.Mvc;

namespace TrabalhoCidade.Controllers
{
    public class LoginControler : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}

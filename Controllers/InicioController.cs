using Microsoft.AspNetCore.Mvc;

namespace ControlePetWeb.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ControlePetWeb.Controllers
{
    public class PaginaInicialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

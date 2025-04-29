using Microsoft.AspNetCore.Mvc;

namespace ControlePetWeb.Controllers
{
    public class PaginaInicialController : Controller
    {
        public IActionResult Index()
        {

            //if ( 1==1 )return RedirectToAction("Index", "Login");

            return View();
        }
    }
}

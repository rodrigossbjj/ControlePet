using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc;

public class CadastroController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(CadastroModel login)
    {
       return View();
    }
}

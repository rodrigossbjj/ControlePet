using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(LoginModel login)
    {
        if (login.Usuario == "admin" && login.Senha == "1234")
        {
            ViewBag.Mensagem = "Login bem-sucedido! Bem-vindo, " + login.Usuario + ".";
        }
        else
        {
            ViewBag.Mensagem = "Usuário ou senha inválidos!";
        }

        return View();
    }
}

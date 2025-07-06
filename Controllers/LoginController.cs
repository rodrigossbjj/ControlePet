using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

public class LoginController : Controller
{
    private readonly Context _context;

    public LoginController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Entrar(string Usuario, string Senha)
    {
        // 1. Busca o usuário (agora compara senha em texto puro)
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Us_Email == Usuario && u.Us_SenhaHash == Senha); // Comparação direta

        if (usuario == null)
        {
            ModelState.AddModelError("", "Usuário ou senha incorretos");
            return View("Index"); // Volta para a página de login com erro
        }

        // 2. Cria a identidade do usuário
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuario.Us_Nome),
        new Claim(ClaimTypes.Email, usuario.Us_Email)
    };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

        // 3. Redireciona PARA O CONTROLLER CORRETO
        return RedirectToAction("Index", "Inicio"); 
    }

    [HttpPost]
    public async Task<IActionResult> Sair()
    {
        //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("~/Views/Inicio/Index.cshtml"); // Corrigido para redirecionar para Index
    }
}
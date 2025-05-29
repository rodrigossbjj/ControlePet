using Microsoft.AspNetCore.Mvc;
using ControlePetWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class RecuperacaoSenhaController : Controller
{
    private readonly Context _context;

    public RecuperacaoSenhaController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SolicitarCodigo(string email)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Us_Email == email);
        if (usuario == null)
        {
            ModelState.AddModelError("", "E-mail não encontrado.");
            return View("Index");
        }

        var codigo = new Random().Next(100000, 999999).ToString();
        usuario.CodigoRecuperacao = codigo;
        usuario.CodigoValidoAte = DateTime.Now.AddMinutes(10);
        await _context.SaveChangesAsync();

        //var corpo = $"Seu código de recuperação é: {codigo}";
        var corpo = EmailHelper.GerarHtmlCodigo(codigo);
        EmailHelper.EnviarEmail(usuario.Us_Email, "Recuperação de Senha - Cuida Pet", corpo);

        TempData["EmailUsuario"] = email;
        return RedirectToAction("Index", "ValidarCodigo");
    }

    [HttpGet]
    public IActionResult ValidarCodigo()
    {
        return View("Index", "ValidarCodigo");
    }

    [HttpPost]
    public async Task<IActionResult> ValidarCodigo(string codigo)
    {
        var email = TempData["EmailUsuario"]?.ToString();
        if (email == null) return RedirectToAction("Index", "SolicitarCodigo");

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Us_Email == email);
        if (usuario == null || usuario.CodigoRecuperacao != codigo || usuario.CodigoValidoAte < DateTime.Now)
        {
            TempData["ErroValidacao"] = "Código inválido ou expirado.";
            TempData["EmailUsuario"] = email;
            return RedirectToAction("Index", "ValidarCodigo");
        }

        TempData["EmailUsuario"] = email;
        return RedirectToAction("Index", "RedefinirSenha");
    }

    [HttpGet]
    public IActionResult RedefinirSenha()
    {
        return View("Index", "RedefinirSenha");
    }

    [HttpPost]
    public async Task<IActionResult> RedefinirSenha(string novaSenha)
    {
        var email = TempData["EmailUsuario"]?.ToString();
        if (email == null) return RedirectToAction("Index", "RecuperacaoSenha");

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Us_Email == email);
        if (usuario == null) return RedirectToAction("Index", "RecuperacaoSenha");

        usuario.Us_SenhaHash = novaSenha;
        usuario.CodigoRecuperacao = null;
        usuario.CodigoValidoAte = null;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Login");
    }
}

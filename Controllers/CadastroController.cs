using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class CadastroController : Controller
{
    private readonly Context _context;

    public CadastroController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new CadastroModel());
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarUsuario(CadastroModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Verifique os dados e tente novamente.");
            return View(model);
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Define data de cadastro
            model.Usuario.Us_DataCadastro = DateTime.Now;

            // Define o nome do usuário a partir do nome do cliente (caso você não use Us_Nome diretamente)
            model.Usuario.Us_Nome = model.Cliente.Cli_NomeCompleto;

            // Associa Cliente ao Usuario
            model.Usuario.Cliente = model.Cliente;
            model.Cliente.Usuario = model.Usuario;

            // Associa Clinica ao Cliente
            model.Cliente.Clinica = model.Clinica;
            model.Clinica.Cliente = model.Cliente;

            // Salva tudo em cascata a partir do topo (Usuario)
            _context.Usuarios.Add(model.Usuario);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
            return RedirectToAction("Index", "Login");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError("", "Erro ao realizar cadastro: " + (ex.InnerException?.Message ?? ex.Message));
            return View(model);
        }
    }
}

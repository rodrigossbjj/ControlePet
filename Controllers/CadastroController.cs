using ControlePetWeb.Helpers;
using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return View("Index", model);
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var dataNascimento = model.Cliente.Cli_DataNascimento;
            var idade = DateTime.Today.Year - dataNascimento.Year;

            //Ajusta se ainda n�o fez anivers�rio esse ano
            if (dataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

            //� necess�rio que o usu�rio seja maior de idade
            if (idade < 18)
            {
                ModelState.AddModelError("Cliente.Cli_DataNascimento", "O cliente deve ter no m�nimo 18 anos.");
                return View("Index", model);
            }

            //Verifia CPF V�lido
            if (!ValidarCPF.CpfValido(model.Cliente.Cli_CPF))
            {
                ModelState.AddModelError("Cliente.Cli_CPF", "CPF inv�lido");
                return View("Index", model);
            }

            //Verifica CNPJ V�lido
            if (!ValidarCNPJ.CnpjValido(model.Clinica.Cln_CNPJ))
            {
                ModelState.AddModelError("Clinica.Cln_CNPJ", "CNPJ inv�lido");
                return View("Index", model);
            }

            //Define data de cadastro
            model.Usuario.Us_DataCadastro = DateTime.Now;

            //Define o nome do usu�rio a partir do nome do cliente (caso voc� n�o use Us_Nome diretamente)
            model.Usuario.Us_Nome = model.Cliente.Cli_NomeCompleto;

            //Associa Cliente ao Usuario
            model.Usuario.Cliente = model.Cliente;
            model.Cliente.Usuario = model.Usuario;

            // Associa Clinica ao Cliente
            model.Cliente.Clinica = model.Clinica;
            model.Clinica.Cliente = model.Cliente;

            //Salva tudo em cascata a partir do topo (Usuario)
            _context.Usuarios.Add(model.Usuario);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
            return RedirectToAction("Index", "Login");
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException?.Message.Contains("UQ__Usuarios") == true ||
                ex.InnerException?.Message.Contains("duplicate") == true) //caso rode em outro banco
            {
                ModelState.AddModelError("Usuario.Us_Email", "Este e-mail j� est� em uso. Por favor, informe outro.");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao realizar cadastro: " + (ex.InnerException?.Message ?? ex.Message));
            }

            await transaction.RollbackAsync();
            return View("Index", model);
        }

        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError("", "Erro ao realizar cadastro: " + (ex.InnerException?.Message ?? ex.Message));
            return View("Index", model);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using ControlePetWeb.Models;

namespace ControlePetWeb.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly Context _context;

        public ConsultasController(Context context)
        {
            _context = context;
        }

        // GET: Consultas/Agendar
        public IActionResult Agendar()
        {
            ViewBag.Pets = _context.Pets.ToList();
            return View();
        }

        // POST: Consultas/Agendar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agendar(
            [Bind("Con_PetId,Con_Data,Con_Descricao")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                consulta.Con_Status = "Agendada"; // Status padrão
                _context.Consultas.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pets = _context.Pets.ToList();
            return View(consulta);
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var consultas = await _context.Consultas
                .Include(c => c.Pet)
                .OrderByDescending(c => c.Con_Data)
                .ToListAsync();

            return View(consultas);
        }

        // GET: Consultas/Cancelar/5
        public async Task<IActionResult> Cancelar(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            consulta.Con_Status = "Cancelada";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
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
            return View("~/Views/Consulta/Agendar.cshtml");
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

                TempData["MensagemSucesso"] = "Consulta agendada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensagemErro"] = "Erro ao agendar consulta. Verifique os dados informados.";
            ViewBag.Pets = _context.Pets.ToList();
            return View("~/Views/Consulta/Agendar.cshtml", consulta);
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

        // GET: Consultas/HistoricoTutores
        public IActionResult HistoricoTutores(string filtroNome)
        {
            var tutoresQuery = _context.Tutores.AsQueryable();

            if (!string.IsNullOrEmpty(filtroNome))
            {
                tutoresQuery = tutoresQuery.Where(t => t.tut_Nome.Contains(filtroNome));
            }

            ViewBag.FiltroNome = filtroNome; // Para manter o filtro na view
            return View(tutoresQuery.ToList());
        }

        // GET: Consultas/HistoricoPorTutor/5
        public IActionResult HistoricoPorTutor(int tutorId)
        {
            var tutor = _context.Tutores
                .Include(t => t.Pets)
                    .ThenInclude(p => p.Consultas)
                .FirstOrDefault(t => t.tut_Id == tutorId);

            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }
    }
}
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
            var consultas = _context.Consultas
                .Include(c => c.Pet)
                    .ThenInclude(p => p.Tutor)
                .OrderByDescending(c => c.Con_Data)
                .ToList();

            return View("~/Views/Consulta/Index.cshtml", consultas);
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


        // GET: Consultas/HistoricoPorPet/5
        public IActionResult HistoricoPorPet(int petId)
        {
            var pet = _context.Pets
                .Include(p => p.Consultas)
                    .ThenInclude(c => c.Pet)
                .Include(p => p.Tutor)  // Inclui o tutor para mostrar na view
                .FirstOrDefault(p => p.pet_Id == petId);

            if (pet == null)
            {
                return NotFound();
            }

            // Ordena as consultas por data decrescente
            pet.Consultas = pet.Consultas
                .OrderByDescending(c => c.Con_Data)
                .ToList();

            return View(pet);
        }

        public IActionResult HistoricoTutores(string filtroNome)
        {
            // Carrega os tutores com a contagem de pets (mas não carrega todas as consultas ainda)
            var query = _context.Tutores
                .Include(t => t.Pets)  // Inclui os pets para poder contar
                .AsQueryable();

            // Aplica filtro se existir
            if (!string.IsNullOrEmpty(filtroNome))
            {
                query = query.Where(t => t.tut_Nome.Contains(filtroNome));
            }

            // Ordena por nome do tutor e executa a query
            var tutores = query.OrderBy(t => t.tut_Nome)
                              .ToList();

            // Passa o filtro atual para a view poder repopular o campo
            ViewBag.FiltroNome = filtroNome;

            return View("~/Views/Consulta/HistoricoTutores.cshtml", tutores);
        }

        // GET: Consultas/HistoricoPorTutor/5
        public IActionResult HistoricoPorTutor(int tutorId)
        {
            var tutor = _context.Tutores
                .Include(t => t.Pets)  // Carrega os pets do tutor
                    .ThenInclude(p => p.Consultas)  // Carrega as consultas de cada pet
                .FirstOrDefault(t => t.tut_Id == tutorId);

            if (tutor == null)
            {
                return NotFound();
            }

            // Ordena os pets (opcional)
            tutor.Pets = tutor.Pets?
                .OrderBy(p => p.pet_Nome)
                .ToList();

            // Para cada pet, ordena suas consultas por data decrescente
            foreach (var pet in tutor.Pets ?? Enumerable.Empty<Pet>())
            {
                pet.Consultas = pet.Consultas?
                    .OrderByDescending(c => c.Con_Data)
                    .ThenByDescending(c => c.Con_Hora)
                    .ToList();
            }

            return View("~/Views/Consulta/HistoricoPorTutor.cshtml", tutor);
        }

    }
}
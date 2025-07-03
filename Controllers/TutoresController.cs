using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControlePetWeb.Controllers
{
    public class TutoresController : Controller
    {
        private readonly Context _context;

        public TutoresController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            ViewBag.TutoresParaSelecao = _context.Tutores.ToList();

            if (id != null)
            {
                var tutorParaEditar = _context.Tutores.Find(id);
                if (tutorParaEditar == null)
                    return NotFound();

                return View("~/Views/AlterarTutor/Index.cshtml", tutorParaEditar);
            }

            return View("~/Views/AlterarTutor/Index.cshtml", new Tutor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                var tutorExistente = _context.Tutores.Find(tutor.tut_Id);
                if (tutorExistente == null)
                    return NotFound();

                //Atualiza os campos
                tutorExistente.tut_Nome = tutor.tut_Nome;
                tutorExistente.tut_Email = tutor.tut_Email;
                tutorExistente.tut_Telefone = tutor.tut_Telefone;

                _context.Update(tutorExistente);
                _context.SaveChanges();

                return RedirectToAction("Index", new { id = tutor.tut_Id });
            }

            //Se inválido, recarrega lista para view (se precisar)
            ViewBag.TutoresParaSelecao = _context.Tutores.ToList();

            return View("~/Views/AlterarTutor/Index.cshtml", tutor);
        }

        public IActionResult CadastrarTutor()
        {
            return View("~/Views/Tutores/CadastrarTutor.cshtml");
        }

        [HttpPost]
        public IActionResult CadastrarTutor(Tutor tutor)
        {
            //Verifica se já existe um e-mail igual no banco
            if (_context.Tutores.Any(t => t.tut_Email == tutor.tut_Email))
            {
                ModelState.AddModelError("tut_Email", "Já existe um tutor cadastrado com esse e-mail.");
            }

            if (ModelState.IsValid)
            {
                _context.Tutores.Add(tutor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tutor);
        }
    }
}
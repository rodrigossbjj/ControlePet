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

        public IActionResult Index()
        {
            var tutores = _context.Tutores.ToList();
            return View(tutores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tutor tutor)
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

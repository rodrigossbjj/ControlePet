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

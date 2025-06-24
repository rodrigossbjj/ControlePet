using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlePetWeb.Controllers
{
    public class PetsController : Controller
    {
        private readonly Context _context;

        public PetsController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var pets = _context.Pets.Include(p => p.Tutor).ToList();
            return View(pets);
        }

        public IActionResult CadastrarPet()
        {
            ViewBag.Tutores = _context.Tutores.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarPet(Pet pet)
        {
            //Verifica se data de nascimento é futura
            if (pet.pet_DataNascimento.HasValue && pet.pet_DataNascimento.Value > DateTime.Today)
            {
                ModelState.AddModelError("pet_DataNascimento", "A data de nascimento não pode ser no futuro.");
            }

            if (ModelState.IsValid)
            {
                _context.Pets.Add(pet);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tutores = _context.Tutores.ToList();
            return View(pet);
        }
    }
}

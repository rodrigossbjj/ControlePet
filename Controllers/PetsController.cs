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

        public IActionResult Index(int? id)
        {
            ViewBag.PetsParaSelecao = _context.Pets.Include(p => p.Tutor).ToList();
            ViewBag.Tutores = _context.Tutores.ToList();

            // Se tiver ID, carrega o pet para edição
            if (id != null)
            {
                var petParaEditar = _context.Pets.Find(id);
                return View("~/Views/AlterarPet/Index.cshtml", petParaEditar);
            }

            return View("~/Views/AlterarPet/Index.cshtml",new Pet());
        }

        public IActionResult CadastrarPet()
        {
            ViewBag.Tutores = _context.Tutores.ToList();
            return View("~/Views/Pets/CadastrarPet.cshtml", new Pet());
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

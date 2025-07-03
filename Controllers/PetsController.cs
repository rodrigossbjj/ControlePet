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

            //Se tiver ID, carrega o pet para edição
            if (id != null)
            {
                var petParaEditar = _context.Pets.Find(id);
                return View("~/Views/AlterarPet/Index.cshtml", petParaEditar);
            }

            return View("~/Views/AlterarPet/Index.cshtml",new Pet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Pet pet)
        {
            if (ModelState.IsValid)
            {
                var petExistente = _context.Pets.Find(pet.pet_Id);
                if (petExistente == null)
                {
                    return NotFound();
                }

                //Atualiza os campos (exemplo básico, atualize todos os que quiser)
                petExistente.pet_Nome = pet.pet_Nome;
                petExistente.pet_Especie = pet.pet_Especie;
                petExistente.pet_Raca = pet.pet_Raca;
                petExistente.pet_Sexo = pet.pet_Sexo;
                petExistente.pet_DataNascimento = pet.pet_DataNascimento;
                petExistente.pet_Castrado = pet.pet_Castrado;
                petExistente.pet_Cor = pet.pet_Cor;
                petExistente.pet_Porte = pet.pet_Porte;
                petExistente.pet_Observacoes = pet.pet_Observacoes;
                petExistente.pet_FotoUrl = pet.pet_FotoUrl;
                petExistente.pet_TutorId = pet.pet_TutorId;
                petExistente.pet_Ativo = pet.pet_Ativo;

                _context.Update(petExistente);
                _context.SaveChanges();

                //Aqui vai pra mesma view com pet atualizado
                return RedirectToAction("Index", new { id = pet.pet_Id });
            }

            //Se inválido, precisa carregar os dados para as listas para não dar erro na view
            ViewBag.PetsParaSelecao = _context.Pets.Include(p => p.Tutor).ToList();
            ViewBag.Tutores = _context.Tutores.ToList();

            //Retorna a view com o modelo e mensagens de erro
            return View("~/Views/AlterarPet/Index.cshtml", pet);
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

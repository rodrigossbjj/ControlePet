using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ControlePetWeb.Models
{
    public class Tutor
    {
        [Key]
        public int tut_Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string tut_Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string tut_Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "Telefone inválido.")]
        public string tut_Telefone { get; set; }

        //Relacionamento: um Tutor pode ter muitos Pets
        [ValidateNever]
        public ICollection<Pet> Pets { get; set; }
    }
}
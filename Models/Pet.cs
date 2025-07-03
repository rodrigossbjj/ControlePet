using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlePetWeb.Models
{
    public class Pet
    {
        [Key]
        [Column("pet_id")]
        public int pet_Id { get; set; }

        [Required(ErrorMessage = "O nome do pet é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string pet_Nome { get; set; }

        [Required(ErrorMessage = "A espécie é obrigatória.")]
        public string pet_Especie { get; set; }

        public string pet_Raca { get; set; }

        [StringLength(1, ErrorMessage = "Sexo deve ser M (Macho) ou F (Fêmea).")]
        public string pet_Sexo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime? pet_DataNascimento { get; set; }

        public bool pet_Castrado { get; set; }

        public string pet_Cor { get; set; }

        public string pet_Porte { get; set; }

        [StringLength(500, ErrorMessage = "Observações devem ter no máximo 500 caracteres.")]
        public string pet_Observacoes { get; set; }

        public string pet_FotoUrl { get; set; }

        public bool pet_Ativo { get; set; } = true;

        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

        //ForingKey para o Tutor
        [Required(ErrorMessage = "É necessário selecionar um tutor.")]
        [ForeignKey("Tutor")]
        public int pet_TutorId { get; set; }

        [ValidateNever]
        public Tutor Tutor { get; set; }
    }
}
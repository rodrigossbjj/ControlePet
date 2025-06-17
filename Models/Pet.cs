using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Pets")]
public class Pet
{
    [Key]
    public int pet_Id { get; set; }

    [Required]
    public string pet_Nome { get; set; }

    public string pet_Especie { get; set; }

    public string pet_Raca { get; set; }

    [StringLength(1)]
    public string pet_Sexo { get; set; }

    [DataType(DataType.Date)]
    public DateTime? pet_DataNascimento { get; set; }

    public bool pet_Castrado { get; set; }

    public string pet_Cor { get; set; }

    public string pet_Porte { get; set; }

    public string pet_Observacoes { get; set; }

    public string pet_FotoUrl { get; set; }

    public bool pet_Ativo { get; set; } = true;

    // FK para o Tutor
    [ForeignKey("Tutor")]
    public int pet_TutorId { get; set; }

    [ValidateNever]
    public Tutor Tutor { get; set; }
}

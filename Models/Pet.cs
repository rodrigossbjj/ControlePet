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

    public DateTime pet_DataNascimento { get; set; }

    // FK para o Tutor
    [ForeignKey("Tutor")]
    public int pet_TutorId { get; set; }

    [ValidateNever]
    public Tutor Tutor { get; set; }
}

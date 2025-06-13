using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Tutores")]
public class Tutor
{
    [Key]
    public int tut_Id { get; set; }

    [Required]
    public string tut_Nome { get; set; }

    public string tut_Email { get; set; }

    public string tut_Telefone { get; set; }

    //Relacionamento: um Tutor pode ter muitos Pets
    [ValidateNever]
    public ICollection<Pet> Pets { get; set; }
}

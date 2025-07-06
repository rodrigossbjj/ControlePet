using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlePetWeb.Models
{
    public class Consulta
    {
        [Key]
        public int Con_Id { get; set; }

        [Required]
        public int Con_PetId { get; set; }

        [Required]
        public DateTime Con_Data { get; set; }
        
        [Required]
        public TimeSpan Con_Hora { get; set; }

        public string Con_Descricao { get; set; }

        public string? Con_Status { get; set; } //Ex: "Agendada", "Realizada", "Cancelada"

        [ForeignKey("Con_PetId")]
        [ValidateNever]
        public Pet Pet { get; set; }
    }
}

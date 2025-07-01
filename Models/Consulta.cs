using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

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

        [ValidateNever]
        public Pet Pet { get; set; }
    }

}

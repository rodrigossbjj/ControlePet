using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ControlePetWeb.Models
{
    public class Consulta
    {
        public int Con_Id { get; set; }

        [Required]
        public int Con_PetId { get; set; }

        [Required]
        public DateTime Con_Data { get; set; }

        public string Con_Descricao { get; set; }

        public string Con_Status { get; set; } //Ex: "Agendada", "Realizada", "Cancelada"

        [ValidateNever]
        public Pet Pet { get; set; }
    }

}

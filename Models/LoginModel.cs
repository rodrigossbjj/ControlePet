
using System.ComponentModel.DataAnnotations;

namespace ControlePetWeb.Models
{
    public class LoginModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}

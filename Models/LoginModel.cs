
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlePetWeb.Models
{
    //[Table("Usuarios")]
    //[Table("Clientes")]
    public class LoginModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}

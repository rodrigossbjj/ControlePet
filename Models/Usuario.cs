using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlePetWeb.Models
{
    public class Usuario
    {
        [Key] // Define como chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Us_Id { get; set; }

        [ValidateNever]
        public string Us_Nome { get; set; }
        public string Us_Email { get; set; }
        public string Us_SenhaHash { get; set; }
        public bool Us_Ativo { get; set; }
        public DateTime Us_DataCadastro { get; set; }
        public string? CodigoRecuperacao { get; set; }
        public DateTime? CodigoValidoAte { get; set; }

        [ValidateNever]
        public Cliente Cliente { get; set; }

    }
}

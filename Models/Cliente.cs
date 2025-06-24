using ControlePetWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlePetWeb.Models
{
    public class Cliente
    {
        [Key]
        public int Cli_Id { get; set; }

        [ForeignKey("Usuario")]
        public int Cli_UserId { get; set; }

        [Required]
        public string Cli_NomeCompleto { get; set; }

        public string Cli_CPF { get; set; }

        public string Cli_RG { get; set; }

        public DateTime Cli_DataNascimento { get; set; }

        public string Cli_EnderecoResidencial { get; set; }

        public string Cli_Telefone { get; set; }

        public string Cli_Celular { get; set; }

        public string Cli_CRMV { get; set; }

        public string Cli_Especialidade { get; set; }

        //public int UsuarioId { get; set; }

        [ValidateNever]
        public Usuario Usuario { get; set; }

        [ValidateNever]
        public Clinica Clinica { get; set; }

    }
}
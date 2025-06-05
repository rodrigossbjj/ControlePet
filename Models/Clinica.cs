using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Clinica
{
    [Key]
    public int Cln_Id { get; set; }

    [ForeignKey("Cliente")]
    public int Cln_ClienteId { get; set; }

    [Required]
    public string Cln_Nome { get; set; }

    public string Cln_Endereco { get; set; }

    public string Cln_CNPJ { get; set; }

    public string Cln_Telefone { get; set; }

    public string Cln_Tipo { get; set; }

    public string Cln_HorarioFuncionamento { get; set; }
    
    [ValidateNever]
    public Cliente Cliente { get; set; }
}

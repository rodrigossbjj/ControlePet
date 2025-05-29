using System.ComponentModel.DataAnnotations;

public class RecuperacaoSenhaModel
{
    [Required(ErrorMessage = "Informe o e-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }
}
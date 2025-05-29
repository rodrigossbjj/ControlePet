using System.ComponentModel.DataAnnotations;

public class ValidarCodigoModel
{
    [Required(ErrorMessage = "O código é obrigatório.")]
    public string Codigo { get; set; }
    public string Email { get; set; }
    public string MensagemErro { get; set; }
}

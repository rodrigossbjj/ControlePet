using System.ComponentModel.DataAnnotations;

public class RedefinirSenhaModel
{
    [Required(ErrorMessage = "Informe a nova senha.")]
    [DataType(DataType.Password)]
    public string NovaSenha { get; set; }

    [Required(ErrorMessage = "Confirme a nova senha.")]
    [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem.")]
    [DataType(DataType.Password)]
    public string ConfirmarSenha { get; set; }

    public string Email { get; set; } // pode vir escondido como campo hidden
}

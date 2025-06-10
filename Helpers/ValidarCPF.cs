using System;
using System.Text.RegularExpressions;

public static class ValidarCPF
{
    public static bool CpfValido(string cpf)
    {
        // Remove caracteres não numéricos
        cpf = Regex.Replace(cpf, @"[^\d]", "");

        // Verifica tamanho ou sequências inválidas
        if (cpf.Length != 11 || Regex.IsMatch(cpf, @"^(\d)\1{10}$"))
            return false;

        // Calcula primeiro dígito verificador
        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * (10 - i);

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        // Calcula segundo dígito verificador
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(cpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        // Verifica se os dígitos calculados conferem com os informados
        return cpf.EndsWith($"{digito1}{digito2}");
    }
}
namespace ControlePetWeb.Helpers
{
    public class ValidarCNPJ
    {
        public static bool CnpjValido(string cnpj)
        { 
            if (string.IsNullOrEmpty(cnpj))
                return false;

            // Remove caracteres não numéricos
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;

            // CNPJs inválidos conhecidos
            string[] cnpjsInvalidos = {
        "00000000000000", "11111111111111", "22222222222222", "33333333333333",
        "44444444444444", "55555555555555", "66666666666666", "77777777777777",
        "88888888888888", "99999999999999"
    };

            if (cnpjsInvalidos.Contains(cnpj))
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cnpjSemDigitos = cnpj.Substring(0, 12);
            string digitos = cnpj.Substring(12, 2);

            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpjSemDigitos[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            soma = 0;
            string cnpjComDigito1 = cnpjSemDigitos + digito1;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpjComDigito1[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            return digitos == $"{digito1}{digito2}";
        }

    }
}

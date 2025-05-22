public static class PasswordHasher
{
    // ⚠️ ATENÇÃO: Método INSECURE para desenvolvimento apenas!
    public static string Hash(string senha)
    {
        // Apenas retorna a senha em texto puro (não faça isso em produção!)
        return senha;
    }

    // ⚠️ Comparação direta (texto puro)
    public static bool Verify(string senhaDigitada, string senhaArmazenada)
    {
        return senhaDigitada == senhaArmazenada;
    }
}
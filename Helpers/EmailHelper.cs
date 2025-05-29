using System.Net;
using System.Net.Mail;

public static class EmailHelper
{
    public static void EnviarEmail(string destino, string assunto, string corpo)
    {
        try
        {
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential(
                    "rodrigosousasales4@gmail.com",
                    "ifsp skoh xpry acev"
                );

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Timeout = 15000; // 15 segundos

                var mail = new MailMessage("rodrigosousasales4@gmail.com", destino, assunto, corpo)
                {
                    IsBodyHtml = true
                };

                smtp.Send(mail);
            }
        }
        catch (SmtpException smtpEx)
        {
            throw new ApplicationException($"Erro SMTP ao enviar e-mail: {smtpEx.StatusCode} - {smtpEx.Message}", smtpEx);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Erro geral ao enviar e-mail: {ex.Message}", ex);
        }
    }
    public static string GerarHtmlCodigo(string codigo)
    {
        return $@"
        <!DOCTYPE html>
        <html lang='pt-br'>
        <head>
            <meta charset='UTF-8'>
            <style>
                body {{
                    background-color: #f2f2f2;
                    font-family: 'Segoe UI', sans-serif;
                    padding: 20px;
                    color: #333;
                }}
                .container {{
                    max-width: 500px;
                    margin: auto;
                    background-color: #ffffff;
                    padding: 30px;
                    border-radius: 10px;
                    box-shadow: 2px 2px 15px rgba(0, 0, 0, 0.1);
                }}
                h1 {{
                    text-align: center;
                    color: #5E8FCF;
                    font-size: 26px;
                    margin-bottom: 30px;
                }}
                p {{
                    font-size: 18px;
                    line-height: 1.6;
                }}
                .codigo {{
                    display: block;
                    margin: 20px auto;
                    padding: 15px;
                    background-color: #e0ecff;
                    color: #2c4f99;
                    font-size: 24px;
                    font-weight: bold;
                    text-align: center;
                    border-radius: 8px;
                    letter-spacing: 3px;
                    width: fit-content;
                }}
                .footer {{
                    text-align: center;
                    margin-top: 30px;
                    font-size: 14px;
                    color: #888;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h1>Recuperação de Senha - Cuida Pet</h1>
                <p>Olá!</p>
                <p>Recebemos uma solicitação para redefinir sua senha. Use o código abaixo para continuar:</p>
                <span class='codigo'>{codigo}</span>
                <p>Se você não solicitou essa alteração, pode ignorar este e-mail.</p>
                <div class='footer'>
                    Cuida Pet - Cuidando com carinho 🐾
                </div>
            </div>
        </body>
        </html>";
    }
}

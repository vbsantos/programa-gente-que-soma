using System.IO;
using System.Net;
using System.Net.Mail;

namespace aula_exe.Output
{
  public class Email : IEmail
  {
    public string EmailEnvia { get; set; }
    public string SenhaEmailEnvia { get; set; }
    public string EmailHost { get; set; }
    public int EmailHostPort { get; set; }

    public Email(string emailEnvia, string senhaEmailEnvia, string emailHost, int emailHostPort)
    {
      EmailEnvia = emailEnvia;
      SenhaEmailEnvia = senhaEmailEnvia;
      EmailHost = emailHost;
      EmailHostPort = emailHostPort;
    }

    public bool Enviar(string destinatario, string remetente, string assunto, string corpo, string nomeCompletoArquivo = null)
    {
      try
      {
        //cria uma mensagem
        MailMessage mail = new MailMessage();

        //define os endereços
        mail.From = new MailAddress(remetente);
        mail.To.Add(destinatario);

        //define o conteúdo
        mail.Subject = assunto;
        mail.Body = corpo;

        if (nomeCompletoArquivo != null)
        {
          //anexa arquivo com clientes
          if (!File.Exists(nomeCompletoArquivo))
            return false;
          mail.Attachments.Add(new Attachment(nomeCompletoArquivo));
        }

        using (var smtp = new SmtpClient(EmailHost))
        {
          smtp.EnableSsl = true; // GMail requer SSL
          smtp.Port = EmailHostPort; // porta para SSL
          smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
          smtp.UseDefaultCredentials = false; // utilizando credencias especificas
          smtp.Credentials = new NetworkCredential(EmailEnvia, SenhaEmailEnvia); // seu usuário e senha para autenticação
          smtp.Send(mail); // envia o e-mail
        }

        return true;
      }
      catch
      {
        return false;
      }
      finally
      {
        if (File.Exists(nomeCompletoArquivo))
          File.Delete(nomeCompletoArquivo);
      }
    }
  }
}

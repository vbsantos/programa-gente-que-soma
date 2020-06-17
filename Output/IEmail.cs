namespace aula_exe.Output
{
  public interface IEmail
  {
    string EmailEnvia { set; get; }
    string SenhaEmailEnvia { set; get; }
    string EmailHost { set; get; }
    int EmailHostPort { set; get; }

    bool Enviar(string destinatario, string remetente, string assunto, string corpo, string nomeCompletoArquivo = null);
  }
}
namespace aula_exe.Entidades
{
  public partial class Endereco
  {
    public string Rua { get; set; }

    public string Numero { get; set; }

    public string Cep { get; set; }

    public string Cidade { get; set; }

    public string Estado { get; set; }

    public Endereco() { }

    public Endereco(string cep)
    {
      Cep = cep;
    }

    public string RetornarDados()
    {
      return $"Endereço: {Rua}, {Numero} - {Cidade}/{Estado} ({Cep})";
    }
  }
}
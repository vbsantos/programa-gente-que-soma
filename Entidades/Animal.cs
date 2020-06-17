namespace aula_exe.Entidades
{
  public partial class Animal
  {
    public string Nome { set; get; }
    public int Idade { set; get; }
    public EnumSexo Sexo { set; get; }

    public Animal() { }

    public Animal(string nome, int idade, EnumSexo sexo)
    {
      Nome = nome;
      Idade = idade;
      Sexo = sexo;
    }

    public void InformarNome(string nome)
    {
      Nome = nome;
    }
    public void InformarIdade(int idade)
    {
      Idade = idade;
    }
    public void InformarSexo(EnumSexo sexo)
    {
      Sexo = sexo;
    }

    public string RetornarNome()
    {
      return Nome;
    }

    public int RetornarIdade()
    {
      return Idade;
    }

    public EnumSexo RetornarSexo()
    {
      return Sexo;
    }

    public string RetornarDadosSimples()
    {
      var info = $"{Nome} ({Sexo.ToString()})";
      return info;
    }
    public string RetornarDados()
    {
      var info = $"Nome: {Nome}\nIdade: {Idade}\nSexo: {Sexo.ToString()}\n";
      return info;
    }

  }
}
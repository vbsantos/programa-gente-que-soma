using System;

namespace aula_exe.Entidades
{
  public partial class Cliente
  {
    public string Cpf;
    public string Nome;
    public int Idade;
    public EnumSexo Sexo;
    public int CarteiraMotorista;
    public int CarteiraReservista;
    public Endereco Endereco;

    public Cliente()
    {
    }
    public Cliente(string cpf, string nome, int idade, EnumSexo sexo)
    {
      Cpf = cpf;
      Nome = nome;
      Idade = idade;
      Sexo = sexo;
    }

    public Cliente(string cpf, string nome, int idade, EnumSexo sexo, Endereco endereco) : this(cpf, nome, idade, sexo)
    {
      Endereco = endereco;
    }

    public void InformarCpf(string cpf)
    {
      Cpf = cpf;
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

    public void InformarEndereco(Endereco endereco)
    {
      Endereco = endereco;
    }

    public bool PossuiMaioridade()
    {
      return (Idade >= 18);
    }

    public EnumSexo retornarSexo()
    {
      return Sexo;
    }

    public bool InformarCarteiraReservista(int carteiraReservista)
    {
      if (!PossuiMaioridade())
        return false;

      if (Sexo == EnumSexo.Feminino)
        return false;

      CarteiraReservista = carteiraReservista;

      return true;
    }

    public bool InformarCarteiraMotorista(int carteiraMotorista)
    {
      if (!PossuiMaioridade())
        return false;

      CarteiraMotorista = carteiraMotorista;

      return true;
    }

    public string RetornarDados()
    {
      var enderecoCliente = Endereco != null ?
        $"{Endereco.RetornarDados()}\n" :
        "";

      var carteiraMotoristaCliente = CarteiraMotorista > 0 ?
        $"Carteira de Motorista: {CarteiraMotorista}\n" :
        "";

      var carteiraReservistaCliente = CarteiraReservista > 0 ?
        $"Carteira de Reservista: {CarteiraReservista}\n" :
        "";

      var info = $"Nome: {Nome} - ({Cpf})\nIdade: {Idade} ({RetornarAnoNascimento()})\nSexo: {Sexo.ToString()}\n{enderecoCliente}{carteiraMotoristaCliente}{carteiraReservistaCliente}";

      return info;
    }

    public int RetornarAnoNascimento()
    {
      var anoNascimento = DateTime.Now.Year - Idade;

      return anoNascimento;
    }

    public string RetornarCpfNome()
    {
      return $"Cpf: {Cpf} - Nome: {Nome}";
    }

    public string RetornarCpf()
    {
      return Cpf;
    }
  }
}
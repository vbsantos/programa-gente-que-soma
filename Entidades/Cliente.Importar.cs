namespace aula_exe.Entidades
{
  public partial class Cliente : IImportarDados
  {
    public void ImportarCsv(string dados)
    {
      var clienteArr = dados.Split(";");

      Cpf = clienteArr[0];
      Nome = clienteArr[1];
      Idade = int.Parse(clienteArr[2]);
      Sexo = clienteArr[3] == "Masculino" ? EnumSexo.Masculino : EnumSexo.Feminino;

      if (Idade > 18)
      {
        CarteiraMotorista = int.Parse(clienteArr[4]);
        if (Sexo == EnumSexo.Masculino)
        {
          CarteiraReservista = int.Parse(clienteArr[5]);
        }
      }

      bool existeEndereco = clienteArr.Length > 10 && clienteArr[7] != "";

      if (existeEndereco)
      {
        Endereco endereco = new Endereco();
        endereco.ImportarCsv(dados);
        Endereco = endereco;
      }
    }
  }
}
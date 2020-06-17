namespace aula_exe.Entidades
{
  public partial class Endereco : IImportarDados
  {
    public void ImportarCsv(string dados)
    {
      var enderecoArr = dados.Split(";");
      Rua = enderecoArr[7];
      Numero = enderecoArr[8];
      Cep = enderecoArr[6];
      Cidade = enderecoArr[9];
      Estado = enderecoArr[10];
    }
  }
}
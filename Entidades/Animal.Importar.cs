namespace aula_exe.Entidades
{
  public partial class Animal : IImportarDados
  {
    public void ImportarCsv(string dados)
    {
      var animalArr = dados.Split(";");

      Nome = animalArr[0];
      Idade = int.Parse(animalArr[1]);
      Sexo = animalArr[2] == "Masculino" ? EnumSexo.Masculino : EnumSexo.Feminino;
    }
  }
}

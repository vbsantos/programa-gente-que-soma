using System.Collections.Generic;

namespace aula_exe.Input
{
  public interface IImportar
  {
    string NomeArquivo { get; set; }
    List<string> Importar();
  }
}

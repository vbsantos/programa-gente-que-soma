using System.Collections.Generic;
using System.IO;

namespace aula_exe.Input
{
  public class ImportarCsv : IImportar
  {
    public string NomeArquivo { get; set; }
    public ImportarCsv(string nomeArquivo)
    {
      NomeArquivo = nomeArquivo + ".csv";
    }

    public List<string> Importar()
    {
      var dadosArr = new List<string>();
      if (File.Exists(NomeArquivo))
      {
        using (var file = new StreamReader(NomeArquivo))
        {
          while (file.Peek() >= 0)
          {
            var dadosStr = file.ReadLine();

            dadosArr.Add(dadosStr);

          }
        }
      }
      return dadosArr;
    }
  }
}

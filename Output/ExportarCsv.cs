using System.Collections.Generic;
using System.IO;
using aula_exe.Entidades;

namespace aula_exe.Output
{
  public class ExportarCsv : IExportar
  {
    public string NomeArquivo { get; set; }
    public ExportarCsv(string nomeDoArquivo)
    {
      NomeArquivo = nomeDoArquivo + ".csv";
    }

    public void Exportar(List<IExportarDados> dados)
    {
      using (var file = new StreamWriter(NomeArquivo))
      {
        foreach (var dado in dados)
        {
          file.WriteLine(dado.ExportarCsv());
        }
      }
    }
  }
}

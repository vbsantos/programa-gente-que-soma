using System.Collections.Generic;
using aula_exe.Entidades;

namespace aula_exe.Output
{
  public interface IExportar
  {
    string NomeArquivo { get; set; }
    void Exportar(List<IExportarDados> dados);
  }
}

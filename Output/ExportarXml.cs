using System.Collections.Generic;
using System.Xml;
using aula_exe.Entidades;

namespace aula_exe.Output
{
  public class ExportarXml : IExportar
  {
    public string NomeArquivo { get; set; }
    public ExportarXml(string nomeArquivo)
    {
      NomeArquivo = nomeArquivo + ".xml";
    }

    public void Exportar(List<IExportarDados> dados)
    {
      var doc = new XmlDocument();

      var xmlDado = doc.CreateElement("dados");
      doc.AppendChild(xmlDado);

      foreach (var item in dados)
      {
        xmlDado.AppendChild(item.ExportarXml(doc));
      }

      doc.Save(NomeArquivo);
    }
  }
}

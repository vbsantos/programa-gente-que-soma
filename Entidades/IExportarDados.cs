using System.Xml;

namespace aula_exe.Entidades
{
  public interface IExportarDados
  {
    string ExportarCsv();

    XmlElement ExportarXml(XmlDocument doc);
  }
}

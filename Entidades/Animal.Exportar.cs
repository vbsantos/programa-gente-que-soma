using System.Xml;

namespace aula_exe.Entidades
{
  public partial class Animal : IExportarDados
  {
    public string ExportarCsv()
    {
      var sexoStr = Sexo.ToString();
      var csvAnimal = $"{Nome};{Idade};{sexoStr}";

      return csvAnimal;
    }

    public XmlElement ExportarXml(XmlDocument doc)
    {
      var xmlCliente = doc.CreateElement("animal");

      var xmlNome = doc.CreateElement("nome");
      xmlNome.InnerText = Nome;
      xmlCliente.AppendChild(xmlNome);

      var xmlIdade = doc.CreateElement("Idade");
      xmlIdade.InnerText = Idade.ToString();
      xmlCliente.AppendChild(xmlIdade);

      var xmlSexo = doc.CreateElement("Sexo");
      xmlSexo.InnerText = Sexo.ToString();
      xmlCliente.AppendChild(xmlSexo);

      return xmlCliente;
    }
  }
}
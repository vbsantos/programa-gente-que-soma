using System.Xml;

namespace aula_exe.Entidades
{
  public partial class Endereco : IExportarDados
  {
    public string ExportarCsv()
    {
      return $"{Cep};{Rua};{Numero};{Cidade};{Estado}";
    }

    public XmlElement ExportarXml(XmlDocument doc)
    {
      var xmlEndereco = doc.CreateElement("endereco");

      var xmlCep = doc.CreateElement("Cep");
      xmlCep.InnerText = Cep;
      xmlEndereco.AppendChild(xmlCep);

      var xmlRua = doc.CreateElement("Rua");
      xmlRua.InnerText = Rua;
      xmlEndereco.AppendChild(xmlRua);

      var xmlNumero = doc.CreateElement("Numero");
      xmlNumero.InnerText = Numero;
      xmlEndereco.AppendChild(xmlNumero);

      var xmlCidade = doc.CreateElement("Cidade");
      xmlCidade.InnerText = Cidade;
      xmlEndereco.AppendChild(xmlCidade);

      var xmlEstado = doc.CreateElement("Estado");
      xmlEstado.InnerText = Estado;
      xmlEndereco.AppendChild(xmlEstado);

      return xmlEndereco;
    }
  }
}

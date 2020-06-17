using System.Xml;

namespace aula_exe.Entidades
{
  public partial class Cliente : IExportarDados
  {
    public string ExportarCsv()
    {
      var sexoStr = Sexo == EnumSexo.Masculino ? "Masculino" : "Feminino";
      var csvCliente = $"{Cpf};{Nome};{Idade};{sexoStr};{CarteiraMotorista};{CarteiraReservista}";

      if (Endereco != null)
      {
        csvCliente += ";" + Endereco.ExportarCsv();
      }

      return csvCliente;
    }

    public XmlElement ExportarXml(XmlDocument doc)
    {
      var xmlCliente = doc.CreateElement("cliente");

      var xmlCpf = doc.CreateElement("cpf");
      xmlCpf.InnerText = Cpf;
      xmlCliente.AppendChild(xmlCpf);

      var xmlNome = doc.CreateElement("nome");
      xmlNome.InnerText = Nome;
      xmlCliente.AppendChild(xmlNome);

      var xmlIdade = doc.CreateElement("Idade");
      xmlIdade.InnerText = Idade.ToString();
      xmlCliente.AppendChild(xmlIdade);

      var xmlSexo = doc.CreateElement("Sexo");
      xmlSexo.InnerText = Sexo == EnumSexo.Masculino ? "Masculino" : "Feminino";
      xmlCliente.AppendChild(xmlSexo);

      var xmlCarteiraMotorista = doc.CreateElement("CarteiraMotorista");
      xmlCarteiraMotorista.InnerText = CarteiraMotorista.ToString();
      xmlCliente.AppendChild(xmlCarteiraMotorista);

      var xmlCarteiraReservista = doc.CreateElement("CarteiraReservista");
      xmlCarteiraReservista.InnerText = CarteiraReservista.ToString();
      xmlCliente.AppendChild(xmlCarteiraReservista);

      if (Endereco != null)
      {
        xmlCliente.AppendChild(Endereco.ExportarXml(doc));
      }

      return xmlCliente;
    }
  }
}
namespace aula_exe.Output
{
  public class FactoryExportar
  {
    public static IExportar RetornarExportador(EnumTipoExportacao tipo, string nomeArquivo)
    {
      IExportar exporta;

      switch (tipo)
      {
        case EnumTipoExportacao.Csv:
          exporta = new ExportarCsv(nomeArquivo);
          break;
        default:
          exporta = new ExportarXml(nomeArquivo);
          break;
      }

      return exporta;
    }
  }
}

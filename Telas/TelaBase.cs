using System;

namespace aula_exe.Telas {
  /// <summary>
  /// Classe base para a construção de novas telas 
  /// </summary>
  public class TelaBase {
    protected ConsoleColor FontColor { set; get; }
    protected ConsoleColor RelevantColor { set; get; }
    protected ConsoleColor AlertColor { set; get; }
    public TelaBase () {
      FontColor = ConsoleColor.Blue;
      AlertColor = ConsoleColor.Yellow;
      RelevantColor = ConsoleColor.Red;
      Console.ForegroundColor = FontColor;
    }

    public TelaBase (ConsoleColor fontColor, ConsoleColor alertColor, ConsoleColor relevantColor) {
      FontColor = fontColor;
      AlertColor = alertColor;
      RelevantColor = relevantColor;
      Console.ForegroundColor = FontColor;
    }

    /// <summary>
    /// Método que escreve em tela as informações
    /// </summary>
    /// <param name="mensagem"></param>
    protected void Escrever (string mensagem) {
      Console.WriteLine (mensagem);
    }

    protected void EscreverEnfase (string mensagem) {
      Console.ForegroundColor = RelevantColor; // Cor da letra
      Escrever (mensagem);
      Console.ForegroundColor = FontColor; // Cor da letra
    }

    protected void EscreverAlerta (string mensagem) {
      Console.ForegroundColor = AlertColor; // Cor da letra
      Escrever (mensagem);
      Console.ForegroundColor = FontColor; // Cor da letra
    }

    /// <summary>
    /// Método que retorna dado em formato string
    /// </summary>
    /// <returns></returns>
    protected string LerString () {
      return Console.ReadLine ();
    }

    /// <summary>
    /// Metodo que retorna dados
    /// </summary>
    /// <returns></returns>
    protected int LerInt () {
      var retorno = 0;
      var mensagem = "";
      var executando = true;
      do {
        if (!string.IsNullOrWhiteSpace (mensagem)) {
          EscreverAlerta (mensagem);
          mensagem = "";
        }

        try {
          retorno = int.Parse (Console.ReadLine ());
          executando = false;
        } catch {
          mensagem = "Número inválido, tente novamente.";
        }
      } while (executando);

      return retorno;
    }

    /// <summary>
    /// Limpa a tela
    /// </summary>
    protected void LimparTela () {
      Console.Clear ();
    }

    /// <summary>
    /// Aguarda qualquer tela ser pressionada 
    /// </summary>
    protected void AguardarTecla () {
      Console.ReadKey ();
    }

    protected void AguardarTecla (string mensagem) {
      EscreverAlerta (mensagem);

      AguardarTecla ();
    }

    protected string EscreverLerString (string mensagem) {
      Escrever (mensagem);

      return LerString ();
    }

    protected int EscreverLerInt (string mensagem) {
      Escrever (mensagem);

      return LerInt ();
    }
  }
}
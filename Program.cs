using System;
using aula_exe.Telas;

namespace aula_exe
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("1 - Clientes\n2 - Animais\n");
      if (Console.ReadLine() == "1")
      {
        var tela1 = new TelaCliente(ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Blue);
        tela1.Executar();
      }
      else
      {
        var tela2 = new TelaAnimal(ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.White);
        tela2.Executar();
      }
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using aula_exe.Entidades;
using aula_exe.Input;
using aula_exe.Output;

namespace aula_exe.Telas
{
  public class TelaAnimal : TelaBase
  {
    private List<Animal> Animais;

    /// <summary>
    /// Constructor sem as cores do display
    /// </summary>
    public TelaAnimal() : base()
    {
      Animais = new List<Animal>();
      CarregarDados();
    }

    /// <summary>
    /// Constructor com as cores do display
    /// </summary>
    public TelaAnimal(ConsoleColor fontColor, ConsoleColor alertColor, ConsoleColor relevantColor) : base(fontColor, alertColor, relevantColor)
    {
      Animais = new List<Animal>();
      CarregarDados();
    }

    /// <summary>
    /// Salva os dados
    /// </summary>
    public void SalvarDados()
    {
      var exportador = FactoryExportar.RetornarExportador(EnumTipoExportacao.Csv, "animais");
      exportador.Exportar(Animais.Cast<IExportarDados>().ToList());
    }

    /// <summary>
    /// Carrega os dados
    /// </summary>
    public void CarregarDados()
    {
      var importa = new ImportarCsv("animais");
      var dados = importa.Importar();
      foreach (var dado in dados)
      {
        var animal = new Animal();
        animal.ImportarCsv(dado);
        Animais.Add(animal);
      }
    }

    /// <summary>
    /// Executa a tela inicial
    /// </summary>
    public void Executar()
    {
      var executando = true;
      do
      {
        LimparTela();

        EscreverEnfase("Menu de opções");
        Escrever("1 - Listar");
        Escrever("2 - Listar Todos os Dados");
        Escrever("3 - Inserir");
        Escrever("4 - Alterar");
        Escrever("5 - Remover");
        Escrever("6 - Exportar");
        Escrever("7 - Sair");

        int opcao = LerInt();
        switch (opcao)
        {
          case 1: // Listar
            Listar();
            break;

          case 2: // Listar Compelto
            ListarCompleto();
            break;

          case 3: // Inserir
            Inserir();
            break;

          case 4: // Alterar
            Alterar();
            break;

          case 5: // Remover
            Remover();
            break;

          case 6: // Exportar
            Exportar();
            break;

          case 7: // Sair
            executando = false;
            break;

          default:
            OpcaoInvalida();
            break;
        }

      } while (executando);
    }

    /// <summary>
    /// Exporta os animais
    /// </summary>
    private void Exportar()
    {
      LimparTela();
      int opcao;
      do
      {
        EscreverEnfase("Exportação de dados");
        Escrever("1 - Exportar para CSV");
        Escrever("2 - Exportar para XML");
        Escrever("3 - Cancelar");
        opcao = LerInt();
        if (opcao < 1 || opcao > 3)
        {
          EscreverEnfase("Opção inválida, tente novamente.");
        }
      } while (opcao < 1 || opcao > 3);

      if (opcao == 3) return;

      LimparTela();
      int opcao2;
      do
      {
        EscreverEnfase("Exportação de dados");
        Escrever("1 - Salvar Em Disco");
        Escrever("2 - Enviar por E-mail");
        Escrever("3 - Cancelar");
        opcao2 = LerInt();
        if (opcao2 < 1 || opcao2 > 3)
        {
          EscreverEnfase("Opção inválida, tente novamente.");
        }
      } while (opcao2 < 1 || opcao2 > 3);

      if (opcao2 == 3) return;

      #region Exportação do Arquivo

      string extensaoArquivo = (EnumTipoExportacao)opcao == EnumTipoExportacao.Csv ? "csv" : "xml";
      string nomeArquivo = EscreverLerString("Com qual o nome deseja salvar o arquivo?");
      var exportador = FactoryExportar.RetornarExportador((EnumTipoExportacao)opcao, nomeArquivo);
      exportador.Exportar(Animais.Cast<IExportarDados>().ToList());

      #endregion

      if (opcao2 == 2)
      {
        #region Envio do E-mail

        var email = new Email("xxxxxxxx@gmail.com", "xxxxxxxx", "smtp.gmail.com", 587);
        string destinatario = EscreverLerString("Para qual e-mail deseja enviar?");
        string nomeCompletoArquivo = $"{nomeArquivo}.{extensaoArquivo}";
        bool statusEmail = email.Enviar(destinatario, "xxxxxxxx@gmail.com", "Animais", "Arquivo em anexo.", nomeCompletoArquivo);

        #endregion

        if (statusEmail)
        {
          EscreverEnfase("E-mail enviado!");
        }
        else
        {
          EscreverEnfase("Falha ao enviar e-mail!");
        }
      }
      else
      {
        EscreverEnfase("Arquivo salvo!");
      }

      AguardarTecla();
    }

    /// <summary>
    /// Altera os animais
    /// </summary>
    private void Alterar()
    {
      var executando = true;
      do
      {
        LimparTela();

        EscreverEnfase("Alterar animal");
        ListarAnimais();
        var opcaoAnimal = EscreverLerString("Informe o Nome para alterar o animal (deixe em branco caso queira voltar)");
        if (string.IsNullOrWhiteSpace(opcaoAnimal))
        {
          executando = false;
        }
        else
        {
          var animal = Animais
            .Where(x => x.RetornarNome() == opcaoAnimal)
            .FirstOrDefault();
          if (animal != null)
          {
            var executando2 = true;
            do
            {
              LimparTela();
              EscreverEnfase("Campos disponíveis");
              Escrever("1 - Nome");
              Escrever("2 - Idade");
              Escrever("3 - Sexo");
              Escrever("4 - Sair");
              var opcaoCampo = EscreverLerString("Informe o campo do animal que deseja alterar");
              switch (opcaoCampo)
              {
                case "1":

                  var opt2 = EscreverLerString("Informe o novo valor de 'Nome': ");
                  animal.InformarNome(opt2);
                  AguardarTecla("Animal alterado com sucesso.");
                  break;

                case "2":

                  var opt3 = EscreverLerInt("Informe o novo valor de 'Idade': ");
                  animal.InformarIdade(opt3);
                  AguardarTecla("Animal alterado com sucesso.");
                  break;

                case "3":

                  var sexoStr = EscreverLerString("Informe o sexo (m/f):");
                  var sexo = (sexoStr == "m") ?
                    EnumSexo.Masculino :
                    EnumSexo.Feminino;
                  animal.InformarSexo(sexo);
                  AguardarTecla("Animal alterado com sucesso.");
                  break;

                case "4":

                  executando2 = false;
                  break;

                default:

                  OpcaoInvalida();
                  break;
              }
            } while (executando2);
            SalvarDados();
          }
          else
          {
            AguardarTecla("Animal não encontrado.");
          }
        }
      }
      while (executando);
    }

    /// <summary>
    /// Remove os animais
    /// </summary>
    private void Remover()
    {
      var executando = true;
      do
      {
        LimparTela();

        EscreverEnfase("Remover animal");

        ListarAnimais();

        var opcao = EscreverLerString("Informe o Nome para remover o animal (deixe em branco caso queira voltar)");
        if (string.IsNullOrWhiteSpace(opcao))
        {
          executando = false;
        }
        else
        {
          var resutl = Animais
            .Where(x => x.RetornarNome() == opcao)
            .FirstOrDefault();
          if (resutl != null)
          {
            Animais.Remove(resutl);
            AguardarTecla("Animal removido com sucesso.");
          }
          else
          {
            AguardarTecla("Animal não encontrado.");
          }
        }

      } while (executando);
      SalvarDados();
    }

    /// <summary>
    /// Efetua a listagem dos animais
    /// </summary>
    private void ListarAnimais()
    {
      foreach (var animal in Animais)
      {
        Escrever(animal.RetornarDadosSimples());
      }
    }

    /// <summary>
    /// Efetua a listagem de todos os dados dos animais
    /// </summary>
    private void ListarAnimaisCompletos()
    {
      foreach (var animal in Animais)
      {
        Escrever(animal.RetornarDados());
      }
    }

    /// <summary>
    /// Lista os animais
    /// </summary>
    private void Listar()
    {
      LimparTela();

      EscreverEnfase("Listagem de animais");

      ListarAnimais();

      AguardarTecla("Pressione qualquer tecla para voltar");
    }

    /// <summary>
    /// Lista os animais com todos os dados
    /// </summary>
    private void ListarCompleto()
    {
      LimparTela();

      EscreverEnfase("Listagem de todas as informações do animal");

      ListarAnimaisCompletos();

      AguardarTecla("Pressione qualquer tecla para voltar");
    }

    /// <summary>
    /// Insere os animais
    /// </summary>
    private void Inserir()
    {
      var executando = true;
      do
      {
        LimparTela();
        EscreverEnfase("Inserir animais");

        var nome = EscreverLerString("Informe o nome:");
        var exists = Animais
          .Where(x => x.RetornarNome() == nome)
          .FirstOrDefault();
        if (exists != null)
        {
          AguardarTecla("Animal com esse nome já registrado!");
          // executando = false;
          break;
        }
        var idade = EscreverLerInt("Informe a idade:");
        var sexoStr = "";
        do
        {
          sexoStr = EscreverLerString("Informe o sexo (m/f):");
          if (sexoStr != "m" && sexoStr != "f")
          {
            EscreverAlerta("Tente novamente utilizando 'm'" +
              " para Macho ou 'f' para Fêmea.");
          }
        } while (sexoStr != "m" && sexoStr != "f");
        var sexo = (sexoStr == "m") ?
          EnumSexo.Masculino :
          EnumSexo.Feminino;

        var animal = new Animal(nome, idade, sexo);

        Animais.Add(animal);

        EscreverAlerta("Animal inserido com sucesso\n\n");

        var opcao = EscreverLerInt("Você gostaria de adicionar outro animal? (1 - sim / 2 - Não)");
        if (opcao == 2)
          executando = false;

      } while (executando);
      SalvarDados();
    }

    private void OpcaoInvalida()
    {
      AguardarTecla("Opção inválida");
    }
  }
}
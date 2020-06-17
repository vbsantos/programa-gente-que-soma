using System;
using System.Collections.Generic;
using System.Linq;
using aula_exe.Entidades;
using aula_exe.Input;
using aula_exe.Output;

namespace aula_exe.Telas
{
  public class TelaCliente : TelaBase
  {
    private List<Cliente> Clientes;

    /// <summary>
    /// Constructor sem as cores do display
    /// </summary>
    public TelaCliente() : base()
    {
      Clientes = new List<Cliente>();
      CarregarDados();
    }

    /// <summary>
    /// Constructor com as cores do display
    /// </summary>
    public TelaCliente(ConsoleColor fontColor, ConsoleColor alertColor, ConsoleColor relevantColor) : base(fontColor, alertColor, relevantColor)
    {
      Clientes = new List<Cliente>();
      CarregarDados();
    }

    /// <summary>
    /// Salva os dados
    /// </summary>
    public void SalvarDados()
    {
      var exportador = FactoryExportar.RetornarExportador(EnumTipoExportacao.Csv, "clientes");
      exportador.Exportar(Clientes.Cast<IExportarDados>().ToList());
    }

    /// <summary>
    /// Carrega os dados
    /// </summary>
    public void CarregarDados()
    {
      var importa = new ImportarCsv("clientes");
      var dados = importa.Importar();
      foreach (var dado in dados)
      {
        var cliente = new Cliente();
        cliente.ImportarCsv(dado);
        Clientes.Add(cliente);
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
    /// Exporta os clientes
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
      exportador.Exportar(Clientes.Cast<IExportarDados>().ToList());

      #endregion

      if (opcao2 == 2)
      {
        #region Envio do E-mail

        var email = new Email("xxxxxxxx@gmail.com", "xxxxxxxx", "smtp.gmail.com", 587);
        string destinatario = EscreverLerString("Para qual e-mail deseja enviar?");
        string nomeCompletoArquivo = $"{nomeArquivo}.{extensaoArquivo}";
        bool statusEmail = email.Enviar(destinatario, "xxxxxxxx@gmail.com", "Clientes", "Arquivo em anexo.", nomeCompletoArquivo);

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
    /// Altera os clientes
    /// </summary>
    private void Alterar()
    {
      var executando = true;
      do
      {
        LimparTela();

        EscreverEnfase("Alterar cliente");
        ListarClientes();
        var opcaoCliente = EscreverLerString("Informe o Cpf para alterar o cliente (deixe em branco caso queira voltar)");
        if (string.IsNullOrWhiteSpace(opcaoCliente))
        {
          executando = false;
        }
        else
        {
          var cliente = Clientes
            .Where(x => x.RetornarCpf() == opcaoCliente)
            .FirstOrDefault();
          if (cliente != null)
          {
            var executando2 = true;
            do
            {
              LimparTela();
              EscreverEnfase("Campos disponíveis");
              Escrever("1 - CPF");
              Escrever("2 - Nome");
              Escrever("3 - Idade");
              Escrever("4 - Sexo");
              Escrever("5 - Endereço");
              Escrever("6 - Carteira de Motorista");
              Escrever("7 - Carteira de Reservista");
              Escrever("8 - Sair");
              var opcaoCampo = EscreverLerString("Informe o campo do cliente que deseja alterar");
              switch (opcaoCampo)
              {
                case "1":

                  var opt1 = EscreverLerString("Informe o novo valor de 'CPF': ");
                  var cliente2 = Clientes
                    .Where(x => x.RetornarCpf() == opt1)
                    .FirstOrDefault();
                  if (cliente2 != null)
                  {
                    AguardarTecla("Não é possível realizar esta ação, CPF já registrado!");
                  }
                  else
                  {
                    cliente.InformarCpf(opt1);
                    AguardarTecla("Cliente alterado com sucesso.");
                  }
                  break;

                case "2":

                  var opt2 = EscreverLerString("Informe o novo valor de 'Nome': ");
                  cliente.InformarNome(opt2);
                  AguardarTecla("Cliente alterado com sucesso.");
                  break;

                case "3":

                  var opt3 = EscreverLerInt("Informe o novo valor de 'Idade': ");
                  cliente.InformarIdade(opt3);
                  AguardarTecla("Cliente alterado com sucesso.");
                  break;

                case "4":

                  var sexoStr = EscreverLerString("Informe o sexo (m/f):");
                  var sexo = (sexoStr == "m") ?
                    EnumSexo.Masculino :
                    EnumSexo.Feminino;
                  cliente.InformarSexo(sexo);
                  AguardarTecla("Cliente alterado com sucesso.");
                  break;

                case "5":

                  EscreverAlerta("Informe o novo endereço:");
                  var rua = EscreverLerString("Informe a rua:");
                  var numeroRua = EscreverLerString("Informe a numero da rua:");
                  var cidade = EscreverLerString("Informe a cidade:");
                  var estado = EscreverLerString("Informe o estado:");
                  var cep = EscreverLerString("Informe o cep:");
                  // criando um novo ao invés de alterar o antigo
                  var endereco = new Endereco
                  {
                    Rua = rua,
                    Cidade = cidade,
                    Cep = cep,
                    Estado = estado,
                    Numero = numeroRua
                  };
                  cliente.InformarEndereco(endereco);
                  AguardarTecla("Cliente alterado com sucesso.");
                  break;

                case "6":

                  if (!cliente.PossuiMaioridade())
                  {
                    AguardarTecla("Não é possível acrescentar esse campo!");
                    break;
                  }
                  var opt6 = EscreverLerInt("Informe o novo valor de 'Carteira de Motorista': ");
                  cliente.InformarCarteiraMotorista(opt6);
                  AguardarTecla("Cliente alterado com sucesso.");
                  break;

                case "7":

                  if (cliente.retornarSexo() == EnumSexo.Feminino || !cliente.PossuiMaioridade())
                  {
                    AguardarTecla("Não é possível acrescentar esse campo!");
                    break;
                  }
                  var opt7 = EscreverLerInt("Informe o novo valor de 'Carteira de Reservista': ");
                  cliente.InformarCarteiraReservista(opt7);
                  AguardarTecla("Cliente alterado com sucesso.");
                  break;

                case "8":

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
            AguardarTecla("Cliente não encontrado.");
          }
        }
      }
      while (executando);
    }

    /// <summary>
    /// Remove os clientes
    /// </summary>
    private void Remover()
    {
      var executando = true;
      do
      {
        LimparTela();

        EscreverEnfase("Remover cliente");

        ListarClientes();

        var opcao = EscreverLerString("Informe o Cpf para remover o cliente (deixe em branco caso queira voltar)");
        if (string.IsNullOrWhiteSpace(opcao))
        {
          executando = false;
        }
        else
        {
          var resutl = Clientes
            .Where(x => x.RetornarCpf() == opcao)
            .FirstOrDefault();
          if (resutl != null)
          {
            Clientes.Remove(resutl);
            AguardarTecla("Cliente removido com sucesso.");
          }
          else
          {
            AguardarTecla("Cliente não encontrado.");
          }
        }

      } while (executando);
      SalvarDados();
    }

    /// <summary>
    /// Efetua a listagem dos clientes
    /// </summary>
    private void ListarClientes()
    {
      foreach (var cliente in Clientes)
      {
        Escrever(cliente.RetornarCpfNome());
      }
    }

    /// <summary>
    /// Efetua a listagem de todos os dados dos clientes
    /// </summary>
    private void ListarClientesCompletos()
    {
      foreach (var cliente in Clientes)
      {
        Escrever(cliente.RetornarDados());
      }
    }

    /// <summary>
    /// Lista os clientes
    /// </summary>
    private void Listar()
    {
      LimparTela();

      EscreverEnfase("Listagem de clientes");

      ListarClientes();

      AguardarTecla("Pressione qualquer tecla para voltar");
    }

    /// <summary>
    /// Lista os clientes com todos os dados
    /// </summary>
    private void ListarCompleto()
    {
      LimparTela();

      EscreverEnfase("Listagem de todas as informações do clientes");

      ListarClientesCompletos();

      AguardarTecla("Pressione qualquer tecla para voltar");
    }

    /// <summary>
    /// Insere os clientes
    /// </summary>
    private void Inserir()
    {
      var executando = true;
      do
      {
        LimparTela();
        EscreverEnfase("Inserir clientes");

        var cpf = EscreverLerString("Informe o cpf:");
        var exists = Clientes
          .Where(x => x.RetornarCpf() == cpf)
          .FirstOrDefault();
        if (exists != null)
        {
          AguardarTecla("CPF já registrado!");
          // executando = false;
          break;
        }
        var nome = EscreverLerString("Informe o nome:");
        var idade = EscreverLerInt("Informe a idade:");
        var sexoStr = "";
        do
        {
          sexoStr = EscreverLerString("Informe o sexo (m/f):");
          if (sexoStr != "m" && sexoStr != "f")
          {
            EscreverAlerta("Tente novamente utilizando 'm'" +
              " para Masculino ou 'f' para Feminino." +
              "\nPedimos desculpa pelo inconveniente!");
          }
        } while (sexoStr != "m" && sexoStr != "f");
        var sexo = (sexoStr == "m") ?
          EnumSexo.Masculino :
          EnumSexo.Feminino;

        #region Endereco

        var escreverEndereco = EscreverLerInt("Você gostaria de adicionar endereço do cliente? (1 - sim / 2 - Não)");
        Cliente cliente;
        if (escreverEndereco == 1)
        {
          Escrever("Informe o endereço:");

          var rua = EscreverLerString("Informe a rua:");
          var numeroRua = EscreverLerString("Informe a numero da rua:");
          var cidade = EscreverLerString("Informe a cidade:");
          var estado = EscreverLerString("Informe o estado:");
          var cep = EscreverLerString("Informe o cep:");

          var endereco = new Endereco
          {
            Rua = rua,
            Cidade = cidade,
            Cep = cep,
            Estado = estado,
            Numero = numeroRua
          };

          cliente = new Cliente(cpf, nome, idade, sexo, endereco);
        }
        else
        {
          cliente = new Cliente(cpf, nome, idade, sexo);
        }

        #endregion

        #region Carteira de Motorista

        if (cliente.PossuiMaioridade())
        {
          var escreverCarteiraMotorista = EscreverLerInt("Você gostaria de adicionar Carteira de Motorista do cliente? (1 - sim / 2 - Não)");
          if (escreverCarteiraMotorista == 1)
          {
            var carteiraMotorista = EscreverLerInt("Informe a Carteira de Motorista:");
            cliente.InformarCarteiraMotorista(carteiraMotorista);
          }
        }

        #endregion

        #region Carteira de Reservista

        if (cliente.PossuiMaioridade() && cliente.retornarSexo() == EnumSexo.Masculino)
        {
          var escreverCarteiraReservista = EscreverLerInt("Você gostaria de adicionar Carteira de Reservista do cliente? (1 - sim / 2 - Não)");
          if (escreverCarteiraReservista == 1)
          {
            var carteiraReservista = EscreverLerInt("Informe a Carteira de Reservista:");
            cliente.InformarCarteiraReservista(carteiraReservista);
          }
        }

        #endregion

        Clientes.Add(cliente);

        EscreverAlerta("Cliente inserido com sucesso\n\n");

        var opcao = EscreverLerInt("Você gostaria de adicionar outro cliente? (1 - sim / 2 - Não)");
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
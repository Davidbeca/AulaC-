
using ImoveisApp.Models;
using ImoveisApp.Services;

namespace ImoveisApp;

class Program
{
    static void Main()
    {
        var service = new ImovelService();

        service.RegistrarImovel(new Casa
        {
            Localizacao = "Lisboa, Parque das Nações",
            Quartos = 3,
            Area = 120,
            TipoNegocio = TipoNegocio.Venda,
            PrecoVenda = 350000,
            Estado = EstadoDisponibilidade.Disponivel,
            TemJardim = false,
            NumeroAndares = 2
        });
        service.RegistrarImovel(new Casa
        {
            Localizacao = "Porto, Foz do Douro",
            Quartos = 4,
            Area = 200,
            TipoNegocio = TipoNegocio.Aluguer,
            RendaMensal = 1800,
            Estado = EstadoDisponibilidade.Disponivel,
            TemJardim = true,
            NumeroAndares = 3
        });
        service.RegistrarImovel(new Apartamento
        {
            Localizacao = "Lisboa, Chiado",
            Quartos = 2,
            Area = 85,
            Piso = 3,
            TemElevador = true,
            TemGaragem = false,
            TipoNegocio = TipoNegocio.Ambos,
            PrecoVenda = 250000,
            RendaMensal = 1200,
            Estado = EstadoDisponibilidade.Disponivel
        });
        service.RegistrarImovel(new Apartamento
        {
            Localizacao = "Faro, Centro",
            Quartos = 1,
            Area = 55,
            Piso = 1,
            TemElevador = false,
            TemGaragem = true,
            TipoNegocio = TipoNegocio.Venda,
            PrecoVenda = 140000,
            Estado = EstadoDisponibilidade.Indisponivel
        });

        bool sair = false;

        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("¦   ?? SISTEMA DE VENDA E ALUGUER       ¦");
            Console.WriteLine("¦           DE IMÓVEIS                  ¦");
            Console.WriteLine("¦----------------------------------------¦");
            Console.WriteLine("¦  1. Registar imóvel                   ¦");
            Console.WriteLine("¦  2. Listar todos os imóveis           ¦");
            Console.WriteLine("¦  3. Listar imóveis disponíveis        ¦");
            Console.WriteLine("¦  4. Listar imóveis para venda         ¦");
            Console.WriteLine("¦  5. Listar imóveis para aluguer       ¦");
            Console.WriteLine("¦  6. Pesquisar imóveis                 ¦");
            Console.WriteLine("¦  7. Alterar estado de disponibilidade ¦");
            Console.WriteLine("¦  8. Remover imóvel                    ¦");
            Console.WriteLine("¦  0. Sair                              ¦");
            Console.WriteLine("+----------------------------------------+");
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    RegistarImovel(service);
                    break;
                case "2":
                    ListarTodos(service);
                    break;
                case "3":
                    ListarDisponiveis(service);
                    break;
                case "4":
                    ListarParaVenda(service);
                    break;
                case "5":
                    ListarParaAluguer(service);
                    break;
                case "6":
                    PesquisarImoveis(service);
                    break;
                case "7":
                    AlterarEstado(service);
                    break;
                case "8":
                    RemoverImovel(service);
                    break;
                case "0":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("? Opção inválida!");
                    break;
            }

            if (!sair)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        Console.WriteLine("?? Obrigado por utilizar o Sistema de Imóveis! Até breve.");
    }

    static void RegistarImovel(ImovelService service)
    {
        Console.WriteLine("--- Registar Novo Imóvel ---");
        Console.Write("Tipo (1=Casa, 2=Apartamento): ");
        string? tipoInput = Console.ReadLine();

        Console.Write("Localização: ");
        string? localizacao = Console.ReadLine();

        Console.Write("Número de quartos: ");
        int quartos = LerInteiro(Console.ReadLine());

        Console.Write("Área (m²): ");
        double area = LerDouble(Console.ReadLine());

        Console.WriteLine("Tipo de negócio (1=Venda, 2=Aluguer, 3=Ambos): ");
        string? negocioInput = Console.ReadLine();
        TipoNegocio tipoNegocio = negocioInput switch
        {
            "1" => TipoNegocio.Venda,
            "2" => TipoNegocio.Aluguer,
            "3" => TipoNegocio.Ambos,
            _ => TipoNegocio.Venda
        };

        double? precoVenda = null;
        double? rendaMensal = null;

        if (tipoNegocio == TipoNegocio.Venda || tipoNegocio == TipoNegocio.Ambos)
        {
            Console.Write("Preço de venda: ");
            precoVenda = LerDoubleNulo(Console.ReadLine());
        }

        if (tipoNegocio == TipoNegocio.Aluguer || tipoNegocio == TipoNegocio.Ambos)
        {
            Console.Write("Renda mensal: ");
            rendaMensal = LerDoubleNulo(Console.ReadLine());
        }

        Imovel imovel;

        if (tipoInput == "2")
        {
            var apto = new Apartamento
            {
                Localizacao = localizacao ?? string.Empty,
                Quartos = quartos,
                Area = area,
                TipoNegocio = tipoNegocio,
                PrecoVenda = precoVenda,
                RendaMensal = rendaMensal,
                Estado = EstadoDisponibilidade.Disponivel
            };
            Console.Write("Piso: ");
            apto.Piso = LerInteiro(Console.ReadLine());
            Console.Write("Tem elevador? (s/n): ");
            apto.TemElevador = LerSimNao(Console.ReadLine());
            Console.Write("Tem garagem? (s/n): ");
            apto.TemGaragem = LerSimNao(Console.ReadLine());
            imovel = apto;
        }
        else
        {
            var casa = new Casa
            {
                Localizacao = localizacao ?? string.Empty,
                Quartos = quartos,
                Area = area,
                TipoNegocio = tipoNegocio,
                PrecoVenda = precoVenda,
                RendaMensal = rendaMensal,
                Estado = EstadoDisponibilidade.Disponivel
            };
            Console.Write("Tem jardim? (s/n): ");
            casa.TemJardim = LerSimNao(Console.ReadLine());
            Console.Write("Número de andares: ");
            casa.NumeroAndares = LerInteiro(Console.ReadLine());
            imovel = casa;
        }

        service.RegistrarImovel(imovel);
        Console.WriteLine("\n? Imóvel registado com sucesso! ID: {imovel.Id}");
    }

    static void ListarTodos(ImovelService service)
    {
        Console.WriteLine("--- Todos os Imóveis ---");
        var imoveis = service.ListarTodos();
        if (imoveis.Count == 0)
        {
            Console.WriteLine("Nenhum imóvel registado.");
            return;
        }

        foreach (var imovel in imoveis)
        {
            Console.WriteLine($"[{imovel.Id}] {imovel.DescricaoDetalhada}");
            Console.WriteLine();
        }
    }

    static void ListarDisponiveis(ImovelService service)
    {
        Console.WriteLine("--- Imóveis Disponíveis ---");
        var imoveis = service.ListarDisponiveis();
        if (imoveis.Count == 0)
        {
            Console.WriteLine("Nenhum imóvel disponível.");
            return;
        }

        foreach (var imovel in imoveis)
        {
            Console.WriteLine($"[{imovel.Id}] {imovel.DescricaoDetalhada}");
            Console.WriteLine();
        }
    }

    static void ListarParaVenda(ImovelService service)
    {
        Console.WriteLine("--- Imóveis para Venda ---");
        var imoveis = service.ListarParaVenda();
        if (imoveis.Count == 0)
        {
            Console.WriteLine("Nenhum imóvel disponível para venda.");
            return;
        }

        foreach (var imovel in imoveis)
        {
            Console.WriteLine($"[{imovel.Id}] {imovel.DescricaoDetalhada}");
            Console.WriteLine();
        }
    }

    static void ListarParaAluguer(ImovelService service)
    {
        Console.WriteLine("--- Imóveis para Aluguer ---");
        var imoveis = service.ListarParaAluguer();
        if (imoveis.Count == 0)
        {
            Console.WriteLine("Nenhum imóvel disponível para aluguer.");
            return;
        }

        foreach (var imovel in imoveis)
        {
            Console.WriteLine($"[{imovel.Id}] {imovel.DescricaoDetalhada}");
            Console.WriteLine();
        }
    }

    static void PesquisarImoveis(ImovelService service)
    {
        Console.WriteLine("--- Pesquisar Imóveis ---");
        Console.Write("Localização (deixe vazio para ignorar): ");
        string? localizacao = Console.ReadLine();

        Console.Write("Tipo (1=Casa, 2=Apartamento, Enter=qualquer): ");
        string? tipoInput = Console.ReadLine();
        TipoImovel? tipo = tipoInput switch
        {
            "1" => TipoImovel.Casa,
            "2" => TipoImovel.Apartamento,
            _ => null
        };

        Console.Write("Quartos mínimos (Enter=qualquer): ");
        int? quartos = LerInteiroNulo(Console.ReadLine());

        Console.Write("Preço máximo (Enter=qualquer): ");
        double? precoMax = LerDoubleNulo(Console.ReadLine());

        var resultados = service.Pesquisar(localizacao, tipo, precoMax, quartos);

        Console.WriteLine("\n?? {resultados.Count} resultado(s) encontrado(s):");
        foreach (var imovel in resultados)
        {
            Console.WriteLine($"[{imovel.Id}] {imovel.DescricaoDetalhada}");
            Console.WriteLine();
        }
    }

    static void AlterarEstado(ImovelService service)
    {
        Console.WriteLine("--- Alterar Estado de Disponibilidade ---");
        Console.Write("ID do imóvel: ");
        int id = LerInteiro(Console.ReadLine()) ;

        var imovel = service.BuscarPorId(id);
        if (imovel is null)
        {
            Console.WriteLine("? Imóvel não encontrado.");
            return;
        }

        Console.WriteLine("Imóvel atual: {imovel.Localizacao} - Estado: {imovel.Estado}");
        Console.WriteLine("Novo estado (1=Disponível, 2=Indisponível): ");
        string? estadoInput = Console.ReadLine();

        var novoEstado = estadoInput == "2"
            ? EstadoDisponibilidade.Indisponivel
            : EstadoDisponibilidade.Disponivel;

        if (service.AlterarEstado(id, novoEstado))
        {
            Console.WriteLine("? Estado alterado para {novoEstado}.");
        }
        else
        {
            Console.WriteLine("? Não foi possível alterar o estado.");
        }
    }

    static void RemoverImovel(ImovelService service)
    {
        Console.WriteLine("--- Remover Imóvel ---");
        Console.Write("ID do imóvel: ");
        int id = LerInteiro(Console.ReadLine()) ;

        if (service.RemoverImovel(id))
        {
            Console.WriteLine("? Imóvel removido com sucesso.");
        }
        else
        {
            Console.WriteLine("? Imóvel não encontrado.");
        }
    }

    static int LerInteiro(string? texto)
    {
        return int.TryParse(texto, out var resultado) ? resultado : 0;
    }

    static int? LerInteiroNulo(string? texto)
    {
        return int.TryParse(texto, out var resultado) ? resultado : (int?)null;
    }

    static double LerDouble(string? texto)
    {
        return double.TryParse(texto, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var resultado) ? resultado : 0;
    }

    static double? LerDoubleNulo(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return null;
        return double.TryParse(texto, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var resultado)
            ? resultado : (double?)null;
    }

    static bool LerSimNao(string? texto)
    {
        return !string.IsNullOrWhiteSpace(texto) &&
               (texto.Trim().ToLower() == "s" || texto.Trim().ToLower() == "sim");
    }
}


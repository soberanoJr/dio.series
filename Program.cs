using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main()
        {
            string opcaoUsuario = ObterOpcaoUsuario();
            while (opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSeries();
                        break;
                    case "3":
                        AtualizarSeries();
                        break;
                    case "4":
                        ExcluirSeries();
                        break;
                    case "5":
                        VisualizarSeries();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                    }
                    opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado!");
        }

        private static void VisualizarSeries()
        {
            Console.WriteLine("> Digite o ID da série:");
            int indiceSerie = int.Parse(Console.ReadLine());
            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.Write(serie);
        }

        private static void ExcluirSeries()
        {
            Console.WriteLine("> Digite o ID da série:");
            int indiceSerie = int.Parse(Console.ReadLine());
            Console.WriteLine($"Confirma a exclusão da série #{indiceSerie}? Digite 'S' ou 'N': ");
            char confirmaExclusao = char.Parse(Console.ReadLine().ToUpper());
            if (confirmaExclusao == 'S') repositorio.Exclui(indiceSerie);
        }

        private static void AtualizarSeries()
        {
            Console.WriteLine("> Digite o ID da série:");
            int indiceSerie = int.Parse(Console.ReadLine());
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i}: {Enum.GetName(typeof(Genero), i)}");
            }

            Console.WriteLine("Selecione o gênero:");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o título:");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano:");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira a descrição:");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie
            (
                id: indiceSerie,
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
            );

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void InserirSeries()
        {
            Console.WriteLine("> Inserir nova série:");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i}: {Enum.GetName(typeof(Genero), i)}");
            }

            Console.WriteLine("Selecione o gênero:");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o título:");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano:");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira a descrição:");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie
            (
                id: repositorio.ProximoId(),
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
            );

            repositorio.Insere(novaSerie);
        }

        private static void ListarSeries()
        {
            Console.WriteLine("> Lista de séries:");
            var lista = repositorio.Lista();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            foreach(var serie in lista)
            {
                if (!serie.retornaExcluido()) Console.WriteLine($"ID#{serie.retornaId()}: {serie.retornaTitulo()}");
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("> Cadastro de séries:");
            Console.WriteLine("1. Listar");
            Console.WriteLine("2. Inserir");
            Console.WriteLine("3. Atualizar");
            Console.WriteLine("4. Excluir");
            Console.WriteLine("5. Visualizar");
            Console.WriteLine("C. Limpar tela");
            Console.WriteLine("X. Sair");
            Console.WriteLine();
            string opcaoUsuario = Console.ReadLine().ToUpper();
            return opcaoUsuario;
        }
    }
}

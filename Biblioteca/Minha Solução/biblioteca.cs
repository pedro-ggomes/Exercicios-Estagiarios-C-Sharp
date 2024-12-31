using System;
using System.Collections.Generic;

namespace Biblioteca
{
    // Classe base
    class Livro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnoPublicacao { get; set; }

        public Livro(string titulo, string autor, int anoPublicacao)
        {
            this.Titulo = titulo;
            this.Autor = autor;
            this.AnoPublicacao = anoPublicacao;
        }

        public virtual void ExibirDetalhes()
        {
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Autor: {Autor}");
            Console.WriteLine($"Ano: {AnoPublicacao}");
        }
    }

    // Classe derivada
    class LivroDigital : Livro
    {
        public double TamanhoArquivoMB { get; set; }

        public LivroDigital(string titulo, string autor, int anoPublicacao, double tamanhoArquivoMB)
            : base(titulo, autor, anoPublicacao)
        {
            this.TamanhoArquivoMB = tamanhoArquivoMB;
        }

        public override void ExibirDetalhes()
        {
            base.ExibirDetalhes();
            Console.WriteLine($"Tamanho do arquivo: {TamanhoArquivoMB} MB (Livro Digital)");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Livro> biblioteca = new List<Livro>();
            int opcao;

            do
            {
                Console.WriteLine("\n==== Sistema de Biblioteca ====");
                Console.WriteLine("1. Cadastrar um novo livro");
                Console.WriteLine("2. Mostrar todos os livros cadastrados");
                Console.WriteLine("3. Sair");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Write("O livro é digital? (s/n): ");
                        string resposta = Console.ReadLine().ToLower();

                        Console.Write("Digite o título do livro: ");
                        string titulo = Console.ReadLine();

                        Console.Write("Digite o autor do livro: ");
                        string autor = Console.ReadLine();

                        Console.Write("Digite o ano de publicação: ");
                        int ano = int.Parse(Console.ReadLine());

                        if (resposta == "s")
                        {
                            Console.Write("Digite o tamanho do arquivo (MB): ");
                            double tamanho = double.Parse(Console.ReadLine());

                            LivroDigital livroDigital = new LivroDigital(titulo, autor, ano, tamanho);
                            biblioteca.Add(livroDigital);
                        }
                        else
                        {
                            Livro livro = new Livro(titulo, autor, ano);
                            biblioteca.Add(livro);
                        }
                        Console.WriteLine("Livro cadastrado com sucesso!");
                        break;

                    case 2:
                        Console.WriteLine("\nLista de Livros:");
                        foreach (var livro in biblioteca)
                        {
                            livro.ExibirDetalhes();
                            Console.WriteLine();
                        }
                        break;

                    case 3:
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            } while (opcao != 3);
        }
    }
}

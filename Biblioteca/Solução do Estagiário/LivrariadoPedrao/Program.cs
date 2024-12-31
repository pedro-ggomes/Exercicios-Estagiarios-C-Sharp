using System;

namespace LivrariadoPedrao
{

    class Program
    {
        static void Main(string[] args)
        {

            List<Livro> biblioteca = new List<Livro>();
            List<LivroDigital> eBiblioteca = new List<LivroDigital>();

            while (true)
            {
                Console.WriteLine("Deseja iniciar o programa?(S/N)");
                string inputUser = Console.ReadLine() ?? "valornulo";

                if (inputUser.ToUpper() == "S")
                {

                    while (true)
                    {

                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("Digite os números para seguirmos");
                        Console.WriteLine("Cadastrar um novo livro (1)");
                        Console.WriteLine("Mostrar todos os livros cadastrados (2)");
                        Console.WriteLine("Sair (3)");
                        Console.WriteLine("Deletar livro (4)");
                        Console.WriteLine("Atualizar um livro informando o titulo (5)");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("");

                        string inputUser2 = Console.ReadLine() ?? "valornulo";
                        if (inputUser2 == "1")
                        {
                            Console.WriteLine("Cadastre um novo livro: ");
                            while (true)
                            {
                                Console.WriteLine("Escolha o modelo do livro: ");
                                Console.WriteLine("Digital(1), Fisico(2): ");
                                string modelo = Console.ReadLine() ?? "valornulo";
                                if (modelo == "1")
                                {
                                    Console.WriteLine("Nos forneça informações sobre o livro, por gentileza: ");
                                    Console.WriteLine("As informações são salvas como digitadas: ");
                                    Console.WriteLine("Qual o titulo do livro: ");
                                    string titulo = Console.ReadLine() ?? "valornulo";
                                    Console.WriteLine("Qual o nome do Autor: ");
                                    string autor = Console.ReadLine() ?? "valornulo";
                                    Console.WriteLine("Qual o ano da publicacao: ");
                                    string ano = Console.ReadLine() ?? "valornulo";
                                    int anopublica = int.Parse(ano);
                                    Console.WriteLine("Qual o tamanho do Livro: ");
                                    string tam = Console.ReadLine() ?? "valornulo";
                                    double tamanholivro = double.Parse(tam);

                                    LivroDigital livreta = new LivroDigital(titulo, autor, anopublica, tamanholivro);

                                    eBiblioteca.Add(livreta);
                                    Console.WriteLine("");
                                    Console.WriteLine("---------------------------------------");
                                    Console.WriteLine("Livro cadastrado com sucesso");
                                    livreta.ExibirDetalhes();
                                    Console.WriteLine("---------------------------------------");
                                    Console.WriteLine("");
                                    break;

                                }
                                if (modelo == "2")
                                {

                                    Console.WriteLine("Nos forneça informações sobre o livro, por gentileza: ");
                                    Console.WriteLine("As informações são salvas como digitadas: ");
                                    Console.WriteLine("Qual o titulo do livro: ");
                                    string titulo = Console.ReadLine() ?? "valornulo";
                                    Console.WriteLine("Qual o nome do Autor: ");
                                    string autor = Console.ReadLine() ?? "valornulo";
                                    Console.WriteLine("Qual o ano da publicacao: ");
                                    string ano = Console.ReadLine() ?? "valornulo";
                                    int anopublica = int.Parse(ano);

                                    Livro livreta = new Livro(titulo, autor, anopublica);


                                    biblioteca.Add(livreta);
                                    Console.WriteLine("");
                                    Console.WriteLine("---------------------------------------");
                                    Console.WriteLine("Livro cadastrado com sucesso");
                                    livreta.ExibirDetalhes();
                                    Console.WriteLine("---------------------------------------");
                                    Console.WriteLine("");
                                    break;
                                }
                            }
                        }
                        if (inputUser2 == "2")
                        {
                            Console.WriteLine("Estes são todos os livros cadastrados:");
                            Console.WriteLine("Digitais:");
                            foreach (LivroDigital livrin in eBiblioteca)
                            {
                                livrin.ExibirDetalhes();
                            }
                            Console.WriteLine("Físicos:");
                            foreach (Livro livrin in biblioteca)
                            {
                                livrin.ExibirDetalhes();
                            }

                        }
                        if (inputUser2 == "3")
                        {
                            Console.WriteLine("De volta ao menu inicial, para sair digite N");
                            break;
                        }
                        if (inputUser2 == "4")
                        {
                            Console.WriteLine("deletar o Livro:");
                            Console.WriteLine("Seu livro é fisico ou Digital?: ");
                            Console.WriteLine("Digite (1)Fisico ou (2)Digital?: ");
                            string tip = Console.ReadLine() ?? "valornulo";
                            if (tip == "1")
                            {
                                int i = 0;
                                foreach (Livro e in biblioteca)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("Informe o titulo: ");
                                    string titleDelete = Console.ReadLine() ?? "valornulo";
                                    if (biblioteca[i].Titulo == titleDelete)
                                    {
                                        biblioteca.Remove(biblioteca[i]);
                                        Console.WriteLine("Livro removido com sucesso");
                                        break;
                                    }


                                }
                            }
                            if (tip == "2")
                            {
                                int i = 0;
                                foreach (LivroDigital livro in eBiblioteca)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("Informe o titulo: ");
                                    string titleDelete = Console.ReadLine() ?? "valornulo";
                                    if (eBiblioteca[i].Titulo == titleDelete)
                                    {
                                        eBiblioteca.Remove(eBiblioteca[i]);
                                        Console.WriteLine("Livro removido com sucesso");
                                        break;
                                    }


                                }
                            }

                        }
                        if (inputUser2 == "5")
                        {
                            Console.WriteLine("Atualizar o Livro:");
                            Console.WriteLine("Seu livro é fisico ou Digital?: ");
                            Console.WriteLine("Digite (1)Fisico ou (2)Digital?: ");
                            string ver = Console.ReadLine() ?? "valornulo";
                            if (ver == "1")
                            {
                                Console.WriteLine("Informe o titulo do livro: ");
                                string tit = Console.ReadLine() ?? "valornulo";
                                foreach (Livro livros in biblioteca)
                                {
                                    int i = 0;
                                    if (biblioteca[i].Titulo == tit)
                                    {
                                        Console.WriteLine("Nos forneça informações sobre o livro, por gentileza: ");
                                        Console.WriteLine("As informações são salvas como digitadas: ");
                                        Console.WriteLine("Qual o titulo do livro: ");
                                        string titulo = Console.ReadLine() ?? "valornulo";
                                        biblioteca[i].Titulo = titulo;
                                        Console.WriteLine("Qual o nome do Autor: ");
                                        string autor = Console.ReadLine() ?? "valornulo";
                                        biblioteca[i].Autor = autor;
                                        Console.WriteLine("Qual o ano da publicacao: ");
                                        string ano = Console.ReadLine() ?? "valornulo";
                                        int anopublica = int.Parse(ano);
                                        biblioteca[i].AnoPublicacao = anopublica;
                                    }
                                    i = i++;
                                }
                            }
                            if (ver == "2")
                            {
                                Console.WriteLine("Informe o titulo do livro: ");
                                string tit = Console.ReadLine() ?? "valornulo";
                                foreach (LivroDigital livros in eBiblioteca)
                                {
                                    int i = 0;
                                    if (eBiblioteca[i].Titulo == tit)
                                    {
                                        Console.WriteLine("Nos forneça informações sobre o livro, por gentileza: ");
                                        Console.WriteLine("As informações são salvas como digitadas: ");
                                        Console.WriteLine("Qual o titulo do livro: ");
                                        string titulo = Console.ReadLine() ?? "valornulo";
                                        eBiblioteca[i].Titulo = titulo;
                                        Console.WriteLine("Qual o nome do Autor: ");
                                        string autor = Console.ReadLine() ?? "valornulo";
                                        eBiblioteca[i].Autor = autor;
                                        Console.WriteLine("Qual o ano da publicacao: ");
                                        string ano = Console.ReadLine() ?? "valornulo";
                                        int anopublica = int.Parse(ano);
                                        eBiblioteca[i].AnoPublicacao = anopublica;
                                        Console.Write("Qual o tamanho do arquovo em MB: ");
                                        string tamMBstr = Console.ReadLine() ?? "valornulo";
                                        double tamMB = double.Parse(tamMBstr);
                                        eBiblioteca[i].TamanhoArquivo = tamMB;
                                    }
                                    i = i++;
                                }

                            }
                        }
                    }
                }
                if (inputUser.ToUpper() == "N")
                {
                    Console.WriteLine("Obrigado por usar a Livraria do Pedrão!");
                    break;
                }

            }
        }

    }
}
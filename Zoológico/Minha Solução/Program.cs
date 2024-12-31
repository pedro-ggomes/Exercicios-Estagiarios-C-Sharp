using System;
using System.Collections.Generic;

namespace Zoologico
{
    // Classe abstrata
    abstract class Animal
    {
        public string Nome { get; set; }
        public int Idade { get; set; }

        public Animal(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public abstract void EmitirSom();

        public virtual void ExibirDetalhes()
        {
            Console.WriteLine($"Nome: {Nome}, Idade: {Idade}");
        }
    }

    // Classe Cachorro
    class Cachorro : Animal
    {
        public Cachorro(string nome, int idade) : base(nome, idade) { }

        public override void EmitirSom()
        {
            Console.WriteLine("O cachorro late: Au au!");
        }
    }

    // Classe Gato
    class Gato : Animal
    {
        public Gato(string nome, int idade) : base(nome, idade) { }

        public override void EmitirSom()
        {
            Console.WriteLine("O gato mia: Miau!");
        }
    }

    // Interface IAquatico
    interface IAquatico
    {
        void Nadar();
    }

    // Classe Peixe
    class Peixe : Animal, IAquatico
    {
        public Peixe(string nome, int idade) : base(nome, idade) { }

        public override void EmitirSom()
        {
            Console.WriteLine("O peixe não emite som audível.");
        }

        public void Nadar()
        {
            Console.WriteLine("O peixe está nadando alegremente.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Animal> zoologico = new List<Animal>();
            int opcao;

            do
            {
                Console.WriteLine("\n==== Sistema de Gerenciamento de Animais ====");
                Console.WriteLine("1. Cadastrar um animal");
                Console.WriteLine("2. Listar todos os animais");
                Console.WriteLine("3. Simular sons dos animais");
                Console.WriteLine("4. Simular comportamento de natação");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Write("Qual animal você deseja cadastrar? (1. Cachorro, 2. Gato, 3. Peixe): ");
                        int tipo = int.Parse(Console.ReadLine());

                        Console.Write("Digite o nome do animal: ");
                        string nome = Console.ReadLine();

                        Console.Write("Digite a idade do animal: ");
                        int idade = int.Parse(Console.ReadLine());

                        if (tipo == 1)
                            zoologico.Add(new Cachorro(nome, idade));
                        else if (tipo == 2)
                            zoologico.Add(new Gato(nome, idade));
                        else if (tipo == 3)
                            zoologico.Add(new Peixe(nome, idade));
                        else
                            Console.WriteLine("Tipo de animal inválido.");
                        break;

                    case 2:
                        Console.WriteLine("\nLista de Animais:");
                        foreach (var animal in zoologico)
                        {
                            animal.ExibirDetalhes();
                        }
                        break;

                    case 3:
                        Console.WriteLine("\nSimulando sons dos animais:");
                        foreach (var animal in zoologico)
                        {
                            animal.EmitirSom();
                        }
                        break;

                    case 4:
                        Console.WriteLine("\nSimulando comportamento de natação:");
                        foreach (var animal in zoologico)
                        {
                            if (animal is IAquatico aquatico)
                            {
                                aquatico.Nadar();
                            }
                        }
                        break;

                    case 5:
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            } while (opcao != 5);
        }
    }
}



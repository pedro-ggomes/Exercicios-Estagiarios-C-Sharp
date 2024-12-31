using System;
using zoologico;

namespace zoologico
{
    interface IAquatico
    {
        void Nadar();
    }

    class Animal
    {
        // Atributos
        public string nome;

        public int idade;


        // Construtor
        public Animal(string nome, int idade)
        {
            this.nome = nome;
            this.idade = idade;
        }

        // Métodos
        public virtual void EmitirSom()
        {
            Console.WriteLine("Animais fazem barulho.");
        }
        public virtual void ExibirDetalhes()
        {
            Console.WriteLine($"O {nome} tem {idade} anos.");
        }

    }

    class Cachorro : Animal
    {
        // Atributos
        public Cachorro(string nome, int idade) : base(nome, idade)
        {
        }

        //Métodos

        public override void EmitirSom()
        {
            Console.WriteLine($"O {nome} faz auau");
        }
        public override void ExibirDetalhes()
        {
            base.ExibirDetalhes();
        }
    }
    class Will : Animal
    {
        // Atributos
        public Will(string nome, int idade) : base(nome, idade)
        {
        }

        //Métodos

        public override void EmitirSom()
        {
            Console.WriteLine("Vai !$ #@!&@ #!¨&@%! seu &¨@!@ ");
        }
        public override void ExibirDetalhes()
        {
            base.ExibirDetalhes();
        }

        public void Nadar()
        {
            Console.WriteLine($"Wilss não sabem nadar.");
            Console.WriteLine($"o {nome} se afoga.");
        }
    }
    class Cr7 : Animal, IAquatico
    {
        // Atributos
        public Cr7(string nome, int idade) : base(nome, idade)
        {
        }

        //Métodos

        public override void EmitirSom()
        {
            Console.WriteLine("Eu sou o milhor!");
            Console.WriteLine("Siiiiiiiiiuuuuu");
        }
        public override void ExibirDetalhes()
        {
            base.ExibirDetalhes();
        }

        public void Nadar()
        {
            Console.WriteLine($"O {nome} é o milhor no nado.");
        }
    }

    class Gato : Animal
    {
        public Gato(string nome, int idade) : base(nome, idade)
        {

        }
        public override void EmitirSom()
        {
            Console.WriteLine($"O {nome} faz miau");
        }
        public override void ExibirDetalhes()
        {
            base.ExibirDetalhes();
        }
    }

    class Peixe : Animal, IAquatico
    {
        // Construtor
        public Peixe(string nome, int idade) : base(nome, idade)
        {

        }
        public void Nadar()
        {
            Console.WriteLine($"O {nome} está nadando alegremente.");
        }
        public override void EmitirSom()
        {
            Console.WriteLine("Peixes não fazem barulo só glub glub.");
        }

    }
}

class Program
{

    static void Main(string[] args)
    {
        List<Cachorro> canil = new List<Cachorro>();
        List<Gato> gatiada = new List<Gato>();
        List<Will> cambada = new List<Will>();
        List<Cr7> portugal = new List<Cr7>();
        List<Peixe> cardume = new List<Peixe>();
        string opcao;
        do
        {
            Console.WriteLine("\n==== Zoologico do Pedrão ====");
            Console.WriteLine("1. Cadastrar um animal");
            Console.WriteLine("2. Listar todos os animais");
            Console.WriteLine("3. Simular sons dos animais");
            Console.WriteLine("4. Simular comportamento de natação");
            Console.WriteLine("5. Sair");
            Console.WriteLine("=============================");
            Console.WriteLine("");


            Console.Write("Escolha uma opção: ");

            opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    Console.WriteLine("");
                    Console.WriteLine("Cadastro de animais: ");
                    Console.WriteLine("Escolha o tipo do seu Animal");
                    Console.WriteLine("(1)Cachorro, (2)Gato, (3)Peixe, (4)Will e (5)Robo");
                    string animalTipo = Console.ReadLine()!;
                    switch (animalTipo)
                    {
                        case "1":
                            Console.WriteLine("Qual o nome do seu Cacorro? ");
                            string cNome = Console.ReadLine()!;
                            Console.WriteLine($"Quantos anos o {cNome} tem? ");
                            int cIdade = int.Parse(Console.ReadLine()!);

                            Cachorro perro = new Cachorro(cNome, cIdade);
                            canil.Add(perro);
                            Console.WriteLine("Cachorro cadastrado com sucesso");
                            break;

                        case "2":
                            Console.WriteLine("Qual o nome do seu Gato? ");
                            string gNome = Console.ReadLine()!;
                            Console.WriteLine($"Quantos anos o {gNome} tem? ");
                            int gIdade = int.Parse(Console.ReadLine()!);

                            Gato gatito = new Gato(gNome, gIdade);
                            gatiada.Add(gatito);
                            Console.WriteLine("Gato cadastrado com sucesso");
                            break;

                        case "3":
                            Console.WriteLine("Qual o nome do seu Peixe? ");
                            string pNome = Console.ReadLine()!;
                            Console.WriteLine($"Quantos anos o {pNome} tem? ");
                            int pIdade = int.Parse(Console.ReadLine()!);

                            Peixe nemo = new Peixe(pNome, pIdade);
                            cardume.Add(nemo);
                            Console.WriteLine("Peixe cadastrado com sucesso");
                            break;

                        case "4":
                            Console.WriteLine("Qual o nome do Will");
                            string wNome = Console.ReadLine()!;
                            Console.WriteLine($"Qual a idade do {wNome}");
                            int wIdade = int.Parse(Console.ReadLine()!);
                            Console.WriteLine("ta véio hein");

                            Will papai = new Will(wNome, wIdade);
                            cambada.Add(papai);
                            Console.WriteLine("Will cadastrado com sucesso(infelizmente)");
                            break;

                        case "5":
                            Console.WriteLine("Qual o nome do Robozao");
                            string rNome = Console.ReadLine()!;
                            Console.WriteLine($"Qual a idade do {rNome}");
                            int rIdade = int.Parse(Console.ReadLine()!);

                            Cr7 maquina = new Cr7(rNome, rIdade);
                            portugal.Add(maquina);
                            Console.WriteLine("Robo cadastrado com sucesso(infelizmente)");
                            break;
                    }
                    break;

                case "2":
                    Console.WriteLine("Cachorros: ");
                    foreach (Cachorro dog in canil)
                    {
                        dog.ExibirDetalhes();
                    }
                    Console.WriteLine("Gatos: ");
                    foreach (Gato cat in gatiada)
                    {
                        cat.ExibirDetalhes();
                    }
                    Console.WriteLine("Peixes: ");
                    foreach (Peixe fish in cardume)
                    {
                        fish.ExibirDetalhes();
                    }
                    Console.WriteLine("Wills: ");
                    foreach (Will daddy in cambada)
                    {
                        daddy.ExibirDetalhes();
                    }
                    Console.WriteLine("R0B0: ");
                    foreach (Cr7 robot in portugal)
                    {
                        robot.ExibirDetalhes();
                    }

                    break;
                case "3":
                    Console.WriteLine("Cachorros: ");
                    foreach (Cachorro dog in canil)
                    {
                        dog.EmitirSom();
                    }
                    Console.WriteLine("Gatos: ");
                    foreach (Gato cat in gatiada)
                    {
                        cat.EmitirSom();
                    }
                    Console.WriteLine("Peixes: ");
                    foreach (Peixe fish in cardume)
                    {
                        fish.EmitirSom();
                    }
                    Console.WriteLine("Wills: ");
                    foreach (Will daddy in cambada)
                    {
                        daddy.EmitirSom();
                    }
                    Console.WriteLine("R0B0: ");
                    foreach (Cr7 robot in portugal)
                    {
                        robot.EmitirSom();
                    }

                    break;
                case "4":
                    Console.WriteLine("Peixes: ");
                    foreach (Peixe fish in cardume)
                    {
                        fish.Nadar();
                    }
                    Console.WriteLine("Wills: ");
                    foreach (Will daddy in cambada)
                    {
                        daddy.Nadar();
                    }
                    Console.WriteLine("R0B0: ");
                    foreach (Cr7 robot in portugal)
                    {
                        robot.Nadar();
                    }
                    break;
            }




        }
        while (opcao != "5");
    }

}

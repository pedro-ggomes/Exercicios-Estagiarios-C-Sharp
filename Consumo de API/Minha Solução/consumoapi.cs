using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumindoApiComPOO
{
    // Classe Endereço
    public class Endereco
    {
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }

        public override string ToString()
        {
            return $"{Rua}, {Cidade}, {CEP}";
        }
    }

    // Classe Usuario
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }

        public virtual void ExibirDetalhes()
        {
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Telefone: {Telefone}");
            Console.WriteLine($"Endereço: {Endereco}");
        }
    }

    // Classe UsuarioDetalhado
    public class UsuarioDetalhado : Usuario
    {
        public string Empresa { get; set; }

        public override void ExibirDetalhes()
        {
            base.ExibirDetalhes();
            Console.WriteLine($"Empresa: {Empresa}");
        }
    }

    // Interface IConsumidorApi
    public interface IConsumidorApi
    {
        Task<List<Usuario>> BuscarUsuarios();
    }

    // Implementação do ConsumidorApi
    public class ConsumidorApi : IConsumidorApi
    {
        private const string Url = "https://jsonplaceholder.typicode.com/users";

        public async Task<List<Usuario>> BuscarUsuarios()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao buscar dados da API.");
            }

            string json = await response.Content.ReadAsStringAsync();
            var usuariosApi = JsonSerializer.Deserialize<List<dynamic>>(json);

            List<Usuario> usuarios = new List<Usuario>();

            foreach (var user in usuariosApi)
            {
                usuarios.Add(new UsuarioDetalhado
                {
                    Id = (int)user.id,
                    Nome = (string)user.name,
                    Email = (string)user.email,
                    Telefone = (string)user.phone,
                    Empresa = (string)user.company.name,
                    Endereco = new Endereco
                    {
                        Rua = (string)user.address.street,
                        Cidade = (string)user.address.city,
                        CEP = (string)user.address.zipcode
                    }
                });
            }

            return usuarios;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            IConsumidorApi api = new ConsumidorApi();
            List<Usuario> usuarios = await api.BuscarUsuarios();

            int opcao;
            do
            {
                Console.WriteLine("\n==== Sistema de Usuários ====");
                Console.WriteLine("1. Listar usuários");
                Console.WriteLine("2. Exibir detalhes de um usuário");
                Console.WriteLine("3. Sair");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("\nLista de Usuários:");
                        foreach (var usuario in usuarios)
                        {
                            Console.WriteLine($"{usuario.Id}. {usuario.Nome}");
                        }
                        break;

                    case 2:
                        Console.Write("\nDigite o ID do usuário que deseja visualizar: ");
                        int id = int.Parse(Console.ReadLine());
                        Usuario selecionado = usuarios.Find(u => u.Id == id);

                        if (selecionado != null)
                        {
                            Console.WriteLine("\nDetalhes do Usuário:");
                            selecionado.ExibirDetalhes();
                        }
                        else
                        {
                            Console.WriteLine("Usuário não encontrado.");
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



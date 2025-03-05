using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api
{
    public class IConsumidorApi
    {
        protected static readonly HttpClient httpClient = new HttpClient();

        public virtual async Task<List<Usuario>> BuscarUsuarios()
        {
            try
            {
                var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
                if (response.IsSuccessStatusCode)
                {
                    var stringJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Usuario>>(stringJson)!;
                }

                Console.WriteLine($"Erro ao buscar usuários: {response.StatusCode}");
                return new List<Usuario>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a API: {ex.Message}");
                return new List<Usuario>();
            }
        }
    }

    public class ConsumidorApi : IConsumidorApi
    {
        public virtual async Task<List<UsuarioDetalhado>> BuscarUsuariosDetalhados()
        {
            try
            {
                var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
                if (response.IsSuccessStatusCode)
                {
                    var stringJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<UsuarioDetalhado>>(stringJson)!;
                }

                Console.WriteLine($"Erro ao buscar usuários detalhados: {response.StatusCode}");
                return new List<UsuarioDetalhado>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a API: {ex.Message}");
                return new List<UsuarioDetalhado>();
            }
        }
    }

    public class Usuario
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Nome { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Telefone { get; set; }

        [JsonPropertyName("address")]
        public Endereco? Endereco { get; set; }

        public virtual void ExibirDetalhes()
        {
            Console.WriteLine($"Nome: {Nome}, Email: {Email}, Telefone: {Telefone}");
        }
    }

    public class UsuarioDetalhado : Usuario
    {
        [JsonPropertyName("company")]
        public Empresa? Empresa { get; set; }

        public override void ExibirDetalhes()
        {
            Console.WriteLine($"Nome: {Nome}, Email: {Email}, Telefone: {Telefone}, Empresa: {Empresa?.Nome}");
        }
    }

    public class Endereco
    {
        [JsonPropertyName("street")]
        public string? Rua { get; set; }

        [JsonPropertyName("city")]
        public string? Cidade { get; set; }

        [JsonPropertyName("zipcode")]
        public string? Cep { get; set; }
    }

    public class Empresa
    {
        [JsonPropertyName("name")]
        public string? Nome { get; set; }
    }

    class Program
    {
        public static async Task Main(string[] args)
        {
            var api = new ConsumidorApi();
            string opcao;

            do
            {
                Console.WriteLine("==== Sistema de Usuários ====");
                Console.WriteLine("1. Listar usuários");
                Console.WriteLine("2. Exibir detalhes de um usuário");
                Console.WriteLine("3. Sair");
                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine()!;

                switch (opcao)
                {
                    case "1":
                        var usuarios = await api.BuscarUsuarios();
                        if (usuarios.Any())
                        {
                            Console.WriteLine("Lista de Usuários:");
                            for (int i = 0; i < usuarios.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {usuarios[i].Nome}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum usuário encontrado.");
                        }

                        break;

                    case "2":
                        Console.Write("Digite o ID do usuário que deseja ver detalhadamente: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var usuariosDetalhados = await api.BuscarUsuariosDetalhados();
                            var usuario = usuariosDetalhados.FirstOrDefault(u => u.Id == id);
                            if (usuario != null)
                            {
                                usuario.ExibirDetalhes();
                            }
                            else
                            {
                                Console.WriteLine("Usuário não encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }

                        break;

                    case "3":
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (opcao != "3");
        }
    }
}

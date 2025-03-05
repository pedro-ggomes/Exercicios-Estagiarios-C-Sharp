using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

//converter c#object to json => jsonserializer.serialize(C#object)
//mudar as urls

namespace ApiComAutenticacao
{
    public class Response
    {
        
        [JsonProperty("data")]
        public List<dynamic> Data { get; set; }

    }
    public class Usuario
    {
        [JsonProperty("username")]
        public string username { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string password { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Uemail { get; set; } = string.Empty;

    }
    public class Profile
    {
        [JsonProperty("first_name")]
        public string Pfirst_name { get; set; } = string.Empty;

        [JsonProperty("last_name")]
        public string Plast_name { get; set; } = string.Empty;

        [JsonProperty("dob")]
        public string Pdob { get; set; } = string.Empty;

        [JsonProperty("avatar")]
        public string Pavatar { get; set; } = string.Empty;
    }
    public class Address
    {
        [JsonProperty("street")]
        public string Astreet { get; set; } = string.Empty;

        [JsonProperty("city")]
        public string Acity { get; set; } = string.Empty;

        [JsonProperty("state")]
        public string Astate { get; set; } = string.Empty;

        [JsonProperty("zip")]
        public string Azip { get; set; } = string.Empty;

    }
    public class Tokens
    {
        [JsonProperty("access_token")]
        public string token { get; set; } = string.Empty;

        [JsonProperty("token_type")]
        public string token_type { get; set; } = string.Empty;

    }
    public class ApiClient
    {
        public string access_token = string.Empty;
        public string token_type = string.Empty;
        HttpClient http1 = new HttpClient();
        private const string BaseUrl = "http://127.0.0.1:8000";
        


        public async Task<string> SignUp(string Uusername, string Upassword)
        {
            var user_information = new Usuario
            {
                username = Uusername,
                password = Upassword
            };
            var json = System.Text.Json.JsonSerializer.Serialize(user_information);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await http1.PostAsync($"{BaseUrl}/signup", content);
                response.EnsureSuccessStatusCode();
                string responsebody = await response.Content.ReadAsStringAsync();
                return responsebody;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return ex.Message;
            }

        }

        public async Task<string> Authenticate(string Uusername, string Upassword)
        {
            var user_information = new Usuario
            {
                username = Uusername,
                password = Upassword
            };
            var json = System.Text.Json.JsonSerializer.Serialize(user_information);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                
                var response = await http1.PostAsync($"{BaseUrl}/login", content);
                response.EnsureSuccessStatusCode();
                string reponsestring = await response.Content.ReadAsStringAsync();
                Tokens newtoken = JsonConvert.DeserializeObject<Tokens>(reponsestring)!;

                access_token = newtoken.token;
                token_type = newtoken.token_type;
                //teste
                Console.WriteLine($"{access_token}, {token_type}");

                return "Sucessfuly authenticated";
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return ex.Message;
            }
        }
        public async Task<List<dynamic>> FetchProtectedData(string access_token, string token_type)
        {
            http1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_type, access_token);
            try
            {
                var response = await http1.GetAsync($"{BaseUrl}/protected");
                string responsebody = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<Response>(responsebody);
                List<dynamic> resposta = new List<dynamic>();
                foreach(var usuario in usuarios.Data)
                {
                    resposta.Add(usuario);
                }
                return resposta;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<dynamic>();
            }
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            ApiClient apiClient1 = new ApiClient();


            string option;
            do
            {
                Console.WriteLine("Pedron's program: ");
                Console.WriteLine("Chose one option to start");
                Console.WriteLine("1. Sign up User");
                Console.WriteLine("2. Autentication");
                Console.WriteLine("3. Acess the project database");
                Console.WriteLine("4. Exit");
                option = Console.ReadLine()!;
                switch (option)
                {
                    case "1":
                        Console.WriteLine("Signing up requires some information.");
                        Console.WriteLine("Please, enter the username: ");
                        string username_sign = Console.ReadLine()!;
                        Console.WriteLine("Now, the password: ");
                        string password_sign = Console.ReadLine()!;
                        Console.WriteLine("Signing up, just a moment... ");
                        string result_sign = await apiClient1.SignUp(username_sign, password_sign);
                        Console.WriteLine($"Result: {result_sign}");
                        break;

                    case "2":
                        //Console.Clear();
                        Console.WriteLine("Signing up requires some information.");
                        Console.WriteLine("Please, enter the username: ");
                        string username_auth = Console.ReadLine()!;
                        Console.WriteLine("Now, the password: ");
                        string password_auth = Console.ReadLine()!;
                        Console.WriteLine("Signing up, just a moment... ");
                        string result_auth = await apiClient1.Authenticate(username_auth, password_auth);
                        Console.WriteLine($"Result: {result_auth}");
                        break;

                    case "3":
                        Console.WriteLine("Acessing protected... ");
                        List<dynamic> usuarios = new List<dynamic>();
                        usuarios = await apiClient1.FetchProtectedData(apiClient1.access_token,apiClient1.token_type);
                        foreach(var usuario in usuarios)
                        {
                            Console.WriteLine(usuario.profile.first_name+" "+usuario.profile.last_name);
                            Console.WriteLine(usuario.id);
                            Console.WriteLine(usuario.username);
                            Console.WriteLine(usuario.email);
                            Console.WriteLine(usuario.address.street);
                            Console.WriteLine(usuario.address.city);
                            Console.WriteLine(usuario.address.state);
                            Console.WriteLine(usuario.address.zip);
                            Console.WriteLine("");
                            Console.WriteLine("======================");
                        }
                        break;
                }
            } while (option != "4");
        }
    }
}

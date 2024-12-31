using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiComAutenticacao
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required Profile Profile { get; set; }
        public required Address Address { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\nUsername: {Username}\nEmail: {Email}\n" +
                   $"Name: {Profile.FirstName} {Profile.LastName}\n" +
                   $"DOB: {Profile.Dob}\nAvatar: {Profile.Avatar}\n" +
                   $"Address: {Address.Street}, {Address.City}, {Address.State}, {Address.Zip}";
        }
    }

    public class Profile
    {
        [JsonProperty("first_name")]
        public required string FirstName { get; set; }
        [JsonProperty("last_name")]
        public required string LastName { get; set; }
        public required string Dob { get; set; }
        public required string Avatar { get; set; }
    }

    public class Address
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Zip { get; set; }
    }

    public class ApiClient
    {
        private const string BaseUrl = "http://127.0.0.1:8000";

        public async Task<string> SignUp(string username, string password)
        {
            using HttpClient client = new HttpClient();
            var content = new StringContent(
                JsonConvert.SerializeObject(new { username, password }),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/signup", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"SignUp failed: {response.ReasonPhrase}");
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Authenticate(string username, string password)
        {
            using HttpClient client = new HttpClient();
            var content = new StringContent(
                JsonConvert.SerializeObject(new { username, password }),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/login", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Authentication failed. Check username and password.");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

            return data["access_token"] ?? "";
        }

        public async Task<List<Usuario>> FetchProtectedData(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/protected");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch protected data.");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Dictionary<string, List<Usuario>>>(jsonResponse);

            return data["data"];
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            ApiClient apiClient = new ApiClient();

            try
            {
                // Sign up a user
                Console.Write("Sign Up\nUsername: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                Console.WriteLine("\nSigning up...");
                string signUpResponse = await apiClient.SignUp(username, password);
                Console.WriteLine($"SignUp Response: {signUpResponse}");

                // Authenticate and get token
                Console.WriteLine("\nAuthenticating...");
                string token = await apiClient.Authenticate(username, password);
                Console.WriteLine("Authentication successful! Token received.");

                // Fetch protected data
                Console.WriteLine("\nFetching protected data...");
                var usuarios = await apiClient.FetchProtectedData(token);

                Console.WriteLine("\nProtected Data:");
                foreach (var usuario in usuarios)
                {
                    Console.WriteLine(usuario);
                    Console.WriteLine(new string('-', 30));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

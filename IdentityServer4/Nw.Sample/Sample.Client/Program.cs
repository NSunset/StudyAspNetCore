using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Sample.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var client = new HttpClient();
            var dicro = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (dicro.IsError)
            {
                Console.WriteLine(dicro.Error);
                return;
            }

            //使用客户端模式获取token
            {
                //var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                //new ClientCredentialsTokenRequest
                //{
                //    Address = dicro.TokenEndpoint,
                //    ClientId = "sample_client",
                //    ClientSecret = "sample_client_secret"
                //}
                //);

                //if (tokenResponse.IsError)
                //{
                //    Console.WriteLine(tokenResponse.Error);
                //    return;
                //}

                //Console.WriteLine(tokenResponse.Json);
            }

            //使用资源拥有者凭证模式获取token

            var tokenResponse = await client.RequestPasswordTokenAsync(
            new PasswordTokenRequest
            {
                Address = dicro.TokenEndpoint,
                ClientId = "sample_client",
                ClientSecret = "sample_secret",
                UserName = "admin",
                Password = "123"
            }
            );

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);



            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync(" https://localhost:6001/Identity");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return;
            }
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));


        }
    }
}

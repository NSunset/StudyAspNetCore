using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nw.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var client = new HttpClient();
                var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }

                // request token
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,

                    ClientId = "client",
                    ClientSecret = "secret",
                    Scope = "api1"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);


                // call api
                var apiClient = new HttpClient();
                apiClient.SetBearerToken(tokenResponse.AccessToken);

                var response = await apiClient.GetAsync("https://localhost:6001/api/identity/get");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }

                var response1 = await apiClient.GetAsync("https://localhost:6001/api/identity/get1");
                if (!response1.IsSuccessStatusCode)
                {
                    Console.WriteLine(response1.StatusCode);
                }
                else
                {
                    var content = await response1.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

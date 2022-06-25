using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nw.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                HttpClient client = new HttpClient();

                //拿到说明文档
                DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }



                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,

                    ClientId = "mvc",
                    ClientSecret = "secret",
                    Scope = "openid profile"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);

                //访问api
                HttpClient apiClient = new HttpClient();
                apiClient.SetBearerToken(tokenResponse.AccessToken);

                HttpResponseMessage response = await apiClient.GetAsync("https://localhost:5003/api/identity/get");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                    return;
                }
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

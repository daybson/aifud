using Aifud.Models;

using System.Text.Json;

namespace Aifud.Services
{
    public class ConsultaCepService
    {
        private HttpClient httpClient;

        public ConsultaCepService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CEP> ConsultaCep(string cep)
        { 
            httpClient.BaseAddress = new Uri("https://brasilapi.com.br");
            var response = await httpClient.GetAsync($"/api/cep/v2/{cep}");
            try
            {
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                var cepresponse = JsonSerializer.Deserialize<CEP>(content);
                
                Console.WriteLine($"Rua: {cepresponse.Street}");
                Console.WriteLine($"Bairro: {cepresponse.Neighborhood}");
                Console.WriteLine($"Cidade: {cepresponse.City}");
                Console.WriteLine($"Estado: {cepresponse.State}");

                return cepresponse;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar CEP: {ex.Message}");
                return null;
            }
        }
    }
}

using Clima.Data;
using Clima.Dto;
using Clima.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Clima.Services
{
    public class ClimaService : IClima
    {
        string apiKey = "a265d39db01d86cf3c81ab93b02f72d6";
        private const string baseUrl = "https://api.openweathermap.org/data/2.5/weather";

        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public ClimaService(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<double?> GetMediaTemperatura(string cidade, DateTime dataInicio, DateTime dataFim)
        {
            var mediaTemperatura = await _context.Clima
                .Where(c => c.Cidade == cidade && 
                            c.Date >= dataInicio &&
                            c.Date <= dataFim)
                .AverageAsync(c => (double?)c.Temperatura);

            return mediaTemperatura;
        }
        public async Task<List<ClimaDto>> GetClimaMultiplasCidades()
        {
           

            var cidades = new List<string> { "Londrina", "São Paulo", "Curitiba" };

            var tasks = cidades.Select(cidade => GetClimaPorCidade(cidade)).ToList();

            var resultados = await Task.WhenAll(tasks);

            _context.Clima.AddRange(resultados.Select(dto => new ClimaModel 
            {
                Cidade = dto.Cidade,
                Temperatura = dto.Temperatura,
                CondicaoTempo = dto.CondicaoTempo,
                Date = DateTime.Now
            }));

            await _context.SaveChangesAsync();
            return resultados.ToList();
        }

        public async Task<ClimaDto> GetClimaPorCidade(string cidade)
        {
                
            string url = $"{baseUrl}?q={cidade}&appid={apiKey}&units=metric&lang=pt";

            HttpResponseMessage resposta = await _httpClient.GetAsync(url);

            if (!resposta.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro ao buscar clima: {resposta.StatusCode}");
            }

            string json = await resposta.Content.ReadAsStringAsync();

            var respostaClima = JsonSerializer.Deserialize<ResponseModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ClimaDto()
            {
                Cidade = respostaClima.Name,
                Temperatura = respostaClima.Main.Temp,
                CondicaoTempo = respostaClima.Weather[0].Description,
                Date = DateTime.Now
            };
        }

    }
}

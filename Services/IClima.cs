using Clima.Dto;

namespace Clima.Services
{
    public interface IClima
    {
        Task<ClimaDto> GetClimaPorCidade(string cidade);
        Task<List<ClimaDto>> GetClimaMultiplasCidades();
        Task<double?> GetMediaTemperatura(string cidade, DateTime dataInicio, DateTime dataFim);
    }
}

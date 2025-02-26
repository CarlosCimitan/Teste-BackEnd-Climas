using Clima.Dto;
using Clima.Models;
using Clima.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimaController : ControllerBase
    {
        private readonly IClima _Clima;

        public ClimaController(IClima clima)
        {
            _Clima = clima;
        }

        [HttpGet("GetClima")]
        public async Task<ActionResult<IEnumerable<ClimaDto>>> GetClima()
        {
            var climas = await _Clima.GetClimaMultiplasCidades();
            return Ok(climas);
        }

        [HttpGet("GetMediaClima")]
        public async Task<double?> GetMediaTemperatura(string cidade, DateTime dataInicio, DateTime dataFim)
        {
            var climaMedio = await _Clima.GetMediaTemperatura(cidade, dataInicio, dataFim);
            return climaMedio;
        }
    }
}

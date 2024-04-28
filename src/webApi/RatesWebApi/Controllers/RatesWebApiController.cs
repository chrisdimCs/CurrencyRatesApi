using Microsoft.AspNetCore.Mvc;
using RatesInterfaces;
using RatesModels;

namespace RatesWebApi.Controllers
{
    [Route("api/rates/[controller]")]
    [ApiController]
    public class RatesWebApiController : ControllerBase
    {
        private readonly IHttpClientService _httpClientService;
        public RatesWebApiController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        [HttpGet("getLatestEcbRates")]
        public async Task<IActionResult> GetLatestEcbRates()
        {
            try
            {
                var latestEcbRates = await _httpClientService.GetLatestEcbRates();

                return Ok(latestEcbRates);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("convertRates")]
        public async Task<IActionResult> ConvertRates([FromQuery] ConvertRequest convertRequest)
        {
            try
            {
                var convert = await _httpClientService.ConvertRates(convertRequest);
                return Ok(convert);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RatesDataCommand.Models;
using RatesInterfaces;
using RatesModels;

namespace RatesApi.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IConvertUrlHelper _convertUrlHelper;

        public HttpClientService(HttpClient httpClient, IConfiguration configuration, IConvertUrlHelper convertUrlHelper)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _convertUrlHelper = convertUrlHelper;
        }

        public async Task<EcbRatesDto> ConvertRates(ConvertRequest convertRequest)
        {
            try
            {
                var convertUrl = _convertUrlHelper.GenerateConvertUrl(convertRequest);

                HttpResponseMessage response = await _httpClient.GetAsync($"{convertUrl}");

                string jsonResponse = await response.Content.ReadAsStringAsync();
                EcbRatesDto convertResponse = JsonConvert.DeserializeObject<EcbRatesDto>(jsonResponse)!; // ! for the null warning (fix it)

                return convertResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Get the Json response from RatesWebApi, convert it to the LatestEcbRatesResponce object and return it. 
        /// </summary>
        /// <returns></returns>
        public async Task<EcbRatesDto> GetLatestEcbRates()
        {
            try
            {
                var apiKey = _configuration["AppSettings:ApiKey"];
                var latestEcbRatesBaseUrl = _configuration["AppSettings:GetLatestBaseUrl"];

                HttpResponseMessage response = await _httpClient.GetAsync($"{latestEcbRatesBaseUrl}apiKey={apiKey}");

                string jsonResponse = await response.Content.ReadAsStringAsync();
                EcbRatesDto latestEcbRatesResponse = JsonConvert.DeserializeObject<EcbRatesDto>(jsonResponse)!; // ! for the null warning (fix it)

                return latestEcbRatesResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                throw;
            }
        }
    }
}

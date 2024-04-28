using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RatesInterfaces;
using RatesModels;

namespace RatesApi.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<EcbRatesDto> ConvertRates(ConvertRequest convertRequest)
        {
            try
            {
                var apiKey = _configuration["AppSettings:ApiKey"];
                string currenciesString = string.Join(",", convertRequest.Currencies);
                var convertBaseUrl = _configuration["AppSettings:ConvertBaseUrl"];
                var currenciesbaseUrl = _configuration["AppSettings:CurrenciesBaseUrl"];
                var latestEcbRatesBaseUrl = _configuration["AppSettings:GetLatestBaseUrl"];

                // Create a list of currencies from the ApiCall response so we can check for wrong user input
                HttpResponseMessage response = await _httpClient.GetAsync($"{currenciesbaseUrl}apiKey={apiKey}");
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Currencies> currenciesList = JsonConvert.DeserializeObject<List<Currencies>>(jsonResponse)!; // ! for the null warning (fix it)

                bool isValidInput = currenciesList.Any(cl => cl.Symbol == convertRequest.From) && convertRequest.Currencies.All(c => currenciesList.Any(cl => cl.Symbol == c));
                if(!isValidInput) 
                {
                    Console.WriteLine("Wrong Input");
                }

                // Call getLatestecbRates to get the latest date
                response = await _httpClient.GetAsync($"{latestEcbRatesBaseUrl}apiKey={apiKey}");
                jsonResponse = await response.Content.ReadAsStringAsync();
                EcbRatesDto latestEcbRatesResponse = JsonConvert.DeserializeObject<EcbRatesDto>(jsonResponse)!; // ! for the null warning (fix it)
                var date = latestEcbRatesResponse.Date;

                string convertUrl = $"{convertBaseUrl}apiKey={apiKey}&from={convertRequest.From}&amount={convertRequest.Amount}&date={date}&currencies={currenciesString}";

                response = await _httpClient.GetAsync($"{convertUrl}");

                jsonResponse = await response.Content.ReadAsStringAsync();
                latestEcbRatesResponse = JsonConvert.DeserializeObject<EcbRatesDto>(jsonResponse)!; // ! for the null warning (fix it)

                return latestEcbRatesResponse;
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

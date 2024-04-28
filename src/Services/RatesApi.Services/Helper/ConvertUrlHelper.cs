using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RatesDataCommand.Models;
using RatesInterfaces;
using RatesModels;

namespace RatesApi.Services.Helper
{
    public class ConvertUrlHelper : IConvertUrlHelper
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ConvertUrlHelper(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> GenerateConvertUrl(ConvertRequest convertRequest)
        {
            List<Currencies> currenciesList = await FetchCurrencies();

            ValidateInput(convertRequest, currenciesList);

            var date = await GetlatestEcbRatesDate();

            return GenerateUrl(convertRequest, date);
        }
        private void ValidateInput(ConvertRequest convertRequest, List<Currencies> currenciesList)
        {
            bool isValidFrom = currenciesList.Any(cl => cl.Symbol == convertRequest.From);
            bool isValidCurrencies = convertRequest.Currencies.All(c => currenciesList.Any(cl => cl.Symbol == c));
            if (!isValidFrom)
            {
                throw new ArgumentException($"Invalid input: The From currency {convertRequest.From} is not valid!");
            }
            else if (!isValidCurrencies)
            {
                var invalidCurrencies = convertRequest.Currencies.Except(currenciesList.Select(cl => cl.Symbol));
                var invalidCurrenciesString = string.Join(", ", invalidCurrencies);
                throw new ArgumentException($"Invalid input: One or more 'to' currencies are not valid: {invalidCurrenciesString}.");
            }
        }
        private async Task<List<Currencies>> FetchCurrencies()
        {
            var apiKey = _configuration["AppSettings:ApiKey"];
            var currenciesbaseUrl = _configuration["AppSettings:CurrenciesBaseUrl"];

            // Create a list of currencies from the ApiCall response so we can check for wrong user input
            HttpResponseMessage response = await _httpClient.GetAsync($"{currenciesbaseUrl}apiKey={apiKey}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<Currencies> currenciesList = JsonConvert.DeserializeObject<List<Currencies>>(jsonResponse)!; // ! for the null warning (fix it) 

            return currenciesList;
        }
        private async Task<string> GetlatestEcbRatesDate()
        {
            var apiKey = _configuration["AppSettings:ApiKey"];
            var latestEcbRatesBaseUrl = _configuration["AppSettings:GetLatestBaseUrl"];

            // Create a list of currencies from the ApiCall response so we can check for wrong user input
            HttpResponseMessage response = await _httpClient.GetAsync($"{latestEcbRatesBaseUrl}apiKey={apiKey}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            EcbRatesDto latestEcbRatesResponse = JsonConvert.DeserializeObject<EcbRatesDto>(jsonResponse)!; // ! for the null warning (fix it)

            return latestEcbRatesResponse.Date;
        }
        private string GenerateUrl(ConvertRequest convertRequest, string latestDate)
        {
            var apiKey = _configuration["AppSettings:ApiKey"];
            var convertBaseUrl = _configuration["AppSettings:ConvertBaseUrl"];
            string currenciesString = string.Join(",", convertRequest.Currencies);

            string convertUrl = $"{convertBaseUrl}apiKey={apiKey}&from={convertRequest.From}&amount={convertRequest.Amount}&date={latestDate}&currencies={currenciesString}";

            return convertUrl;
        }
    }
}

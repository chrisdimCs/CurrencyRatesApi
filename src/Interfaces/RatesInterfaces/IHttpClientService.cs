using RatesModels;

namespace RatesInterfaces
{
    public interface IHttpClientService
    {
        Task<EcbRatesDto> GetLatestEcbRates();
        Task<EcbRatesDto> ConvertRates(string from, List<string> currencies, decimal amount);
    }
}

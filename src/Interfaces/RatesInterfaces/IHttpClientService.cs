using RatesModels;

namespace RatesInterfaces
{
    public interface IHttpClientService
    {
        Task<LatestEcbRatesResponce> GetLatestEcbRates();
        Task<LatestEcbRatesResponce> ConvertRates(string from, List<string> currencies, decimal amount);
    }
}

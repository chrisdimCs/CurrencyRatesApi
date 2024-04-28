using RatesDataCommand.Models;
using RatesModels;

namespace RatesInterfaces
{
    public interface IHttpClientService
    {
        Task<EcbRatesDto> GetLatestEcbRates();
        Task<EcbRatesDto> ConvertRates(ConvertRequest convertRequest);
    }
}

using RatesDataCommand.Models;

namespace RatesDataCommand.Interfaces
{
    public interface IConvertRatesRepository
    {
        Task AddConvert(EcbRatesDto ecbRatesDto);
    }
}

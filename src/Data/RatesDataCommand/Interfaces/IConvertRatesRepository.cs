using RatesDataCommand.Models;

namespace RatesDataCommand.Interfaces
{
    public interface IConvertRatesRepository
    {
        Task InsertConvert(EcbRatesDto ecbRatesDto);
    }
}

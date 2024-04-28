using RatesModels;

namespace RatesInterfaces
{
    public interface IConvertUrlHelper
    {
        Task<string> GenerateConvertUrl(ConvertRequest convertRequest);
    }
}

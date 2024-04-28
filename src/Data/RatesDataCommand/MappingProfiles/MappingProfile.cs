using AutoMapper;
using RatesData.Entities;
using RatesDataCommand.Models;
using System.Text;

namespace RatesDataCommand.MappingProfiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<EcbRatesDto, EcbRate>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Base, opt => opt.MapFrom(source => source.Base))
                .ForMember(dest => dest.Rates, opt => opt.MapFrom(source => SerializeRates(source.Rates)))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow.ToString()));
        }

        private string SerializeRates(Dictionary<string, decimal>? rates)
        {
            if (rates == null || rates.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder serializedRates = new StringBuilder();
            foreach (var rate in rates) 
            {
                serializedRates.Append($"{rate.Key}:{rate.Value},");
            }

            return serializedRates.ToString().TrimEnd(',');
        }
    }
}

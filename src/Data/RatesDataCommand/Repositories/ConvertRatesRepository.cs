using RatesData.Data;
using RatesData.Entities;
using AutoMapper;
using RatesDataCommand.Interfaces;
using RatesDataCommand.MappingProfiles;
using RatesDataCommand.Models;

namespace RatesDataCommand.Repositories
{
    public class ConvertRatesRepository : IConvertRatesRepository
    {
        private readonly RatesDbContext _context;
        private readonly IMapper _mapper;

        public ConvertRatesRepository(RatesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ecbRatesDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task InsertConvert(EcbRatesDto ecbRatesDto)
        {
            try
            {
                var ecbRatesEntity = _mapper.Map<EcbRate>(ecbRatesDto);

                await _context.AddAsync<EcbRate>(ecbRatesEntity);

                var result = _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }   

        }
    }
}

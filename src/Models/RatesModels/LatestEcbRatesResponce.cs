using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesModels
{
    public class LatestEcbRatesResponce
    {
        public string? Base { get; set; }
        public string? Date { get; set; }
        public Dictionary<string, decimal>? Rates { get; set; }
    }
}

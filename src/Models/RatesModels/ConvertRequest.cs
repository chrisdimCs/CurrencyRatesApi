using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesModels
{
    public class ConvertRequest
    {
        public string From { get; set; }
        public List<string> Currencies { get; set; }
        public decimal Amount { get; set; }
    }
}

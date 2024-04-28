namespace RatesDataCommand.Models
{
    public class EcbRatesDto
    {
        public string? Base { get; set; }
        public string? Date { get; set; }
        public Dictionary<string, decimal>? Rates { get; set; }
    }
}

namespace RatesModels
{
    public class ConvertRequest
    {
        public string From { get; set; }
        public List<string> Currencies { get; set; }
        public decimal Amount { get; set; }
    }
}

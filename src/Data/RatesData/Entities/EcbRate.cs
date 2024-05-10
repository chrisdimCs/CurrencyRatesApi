namespace RatesData.Entities;

public partial class EcbRate
{
    public int Id { get; set; }

    public string? Base { get; set; }

    public string? Date { get; set; }

    public string? Rates { get; set; }

    public string? CreatedAt { get; set; }
}

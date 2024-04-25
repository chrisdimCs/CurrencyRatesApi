using System;
using System.Collections.Generic;

namespace RatesData.Entities;

public partial class ConvertRate
{
    public int Id { get; set; }

    public string? Base { get; set; }

    public string? Rates { get; set; }

    public string? CreatedAt { get; set; }
}

using System.Text.Json.Serialization;

namespace Health;

public class TestHealthRecord
{
    public int? Id { get; set; }

    public string? RecordName { get; set; }

    public bool? IsComplete { get; set; }
}


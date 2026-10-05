namespace AILLM.Models;

public class GrobResponse
{
    public string Id { get; set; } = string.Empty;

    public string Object { get; set; } = string.Empty;

    public long Created { get; set; }

    public string Model { get; set; } = string.Empty;

    public List<GrobChoice> Choices { get; set; } = [];

    public GrobUsage? Usage { get; set; }
}
namespace AILLM.Models;

public class GrobChoice
{
    public int Index { get; set; }

    public GrobChatMessage Message { get; set; } = new();

    public string? FinishReason { get; set; }
}
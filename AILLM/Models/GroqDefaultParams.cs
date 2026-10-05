namespace AILLM.Models;

public class GroqDefaultParams
{
    public string Model { get; set; } = string.Empty;

    public GrobChatMessage[] Messages { get; set; } = [];
}
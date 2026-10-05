namespace AILLM.Models
{
    public class AgentPlan
    {
        public string Intent { get; set; } = string.Empty;
        public List<string> Concepts { get; set; } = [];
    }

    public class SqlPlan
    {
        public string Sql { get; set; } = string.Empty;
        public string Intent { get; set; } = string.Empty;
    }
}

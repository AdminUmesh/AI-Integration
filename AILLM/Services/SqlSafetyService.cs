using System.Text.RegularExpressions;

namespace AILLM.Services
{
    public class SqlSafetyService
    {
        public void Validate(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new InvalidOperationException("Empty SQL.");

            var normalized = Regex.Replace(sql, @"\s+", " ").Trim();

            if (!normalized.StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase) &&
                !normalized.StartsWith("WITH ", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Only SELECT/CTE queries are allowed.");

            var forbidden = new[]
            {
            " INSERT ", " UPDATE ", " DELETE ", " DROP ", " ALTER ",
            " TRUNCATE ", " CREATE ", " EXEC ", " EXECUTE ", " MERGE ",
            " GRANT ", " REVOKE ", " DENY ", " DBCC ", " BACKUP ", " RESTORE "
        };

            var padded = $" {normalized.ToUpperInvariant()} ";

            if (forbidden.Any(x => padded.Contains(x)) ||
                normalized.Contains(';') ||
                normalized.Contains("--") ||
                normalized.Contains("/*") ||
                normalized.Contains("*/"))
                throw new InvalidOperationException("Unsafe SQL generated.");
        }
    }
}

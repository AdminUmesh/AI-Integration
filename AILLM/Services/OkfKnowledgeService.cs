using System.Text;

namespace AILLM.Services;

public class OkfKnowledgeService
{
    private readonly string _okfRoot;

    public OkfKnowledgeService(IWebHostEnvironment environment)
    {
        _okfRoot = Path.Combine(
            environment.ContentRootPath,
            "Knowledge",
            "OKF");
    }

    public async Task<string> GetIndexAsync()
    {
        var indexPath = Path.Combine(
            _okfRoot,
            "index.md");

        if (!File.Exists(indexPath))
        {
            throw new FileNotFoundException(
                "OKF index.md was not found.",
                indexPath);
        }

        return await File.ReadAllTextAsync(indexPath);
    }

    public async Task<string> GetConceptsAsync(
        IEnumerable<string> conceptPaths)
    {
        var result = new StringBuilder();

        foreach (var relativePath in conceptPaths.Distinct())
        {
            var safePath = relativePath.Replace(
                '/',
                Path.DirectorySeparatorChar);

            var fullPath = Path.GetFullPath(
                Path.Combine(
                    _okfRoot,
                    safePath));

            // Prevent path traversal
            if (!fullPath.StartsWith(
                    Path.GetFullPath(_okfRoot),
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Invalid OKF document path.");
            }

            if (!File.Exists(fullPath))
            {
                continue;
            }

            var content =
                await File.ReadAllTextAsync(fullPath);

            result.AppendLine(
                $"### OKF Document: {relativePath}");

            result.AppendLine(content);

            result.AppendLine();
            result.AppendLine("---");
            result.AppendLine();
        }

        return result.ToString();
    }
}
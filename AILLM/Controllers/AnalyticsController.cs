using AILLM.Services;
using Microsoft.AspNetCore.Mvc;

namespace AILLM.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly SqlAgentService _agent;

    public AnalyticsController(SqlAgentService agent)
    {
        _agent = agent;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] UserQuestion request)
    {
        if (string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest("Question cannot be empty.");
        }

        var answer =
            await _agent.AskAsync(request.Question);

        return Ok(new
        {
            answer
        });
    }
}

public class UserQuestion
{
    public string Question { get; set; } = string.Empty;
}
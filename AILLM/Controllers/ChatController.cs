using AILLM.Models;
using AILLM.Services;
using Microsoft.AspNetCore.Mvc;

namespace AILLM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Chat(
        //    [FromBody] UserPrompt prompt)
        //{
        //    if (string.IsNullOrWhiteSpace(prompt.Prompt))
        //    {
        //        return BadRequest("Prompt cannot be empty.");
        //    }

        //    var response =
        //        await _chatService.StartNewChat(prompt);

        //    return Ok(response);
        //}

    }
}

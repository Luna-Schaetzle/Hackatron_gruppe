using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ChatBotController : ControllerBase
{
    [HttpPost]
    public IActionResult GetResponse([FromBody] ChatMessage message)
    {
        if (message == null || string.IsNullOrEmpty(message.Content))
        {
            return BadRequest("Message content cannot be null or empty.");
        }

        // Einfache Logik für den Chatbot
        string botResponse = "Ich bin ein einfacher Chatbot. Du hast gesagt: " + message.Content;

        return Ok(new ChatMessage { Sender = "Chatbot", Content = botResponse });
    }
}

public class ChatMessage
{
    public string Sender { get; set; }
    public string Content { get; set; }
}

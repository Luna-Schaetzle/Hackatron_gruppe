using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Tasks;
using VitualReception.Domain.Model;

namespace VirtualReception.Server.Controllers
{
    [Route("chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        #region dependencies

        private readonly IChatRepository _chatRepository;

        #endregion

        #region ctors

        /// <summary>
        /// Constructs a <see cref="ChatController"/> instance.
        /// </summary>
        /// <param name="chatRepository">A collection of <see cref="Chat"/> instances.</param>
        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        #endregion

        [HttpPost]
        [Route("chat")]
        public async Task<IActionResult> PostChat(string chatString)
        {
            var chat = JsonSerializer.Deserialize<Chat>(chatString);
            await _chatRepository.AddAsync(chat);
            return Ok();
        }

        [HttpGet]
        [Route("chats")]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatRepository.FindAllAsync();
            var chatsString = JsonSerializer.Serialize<IEnumerable<Chat>>(chats);
            var result = Ok(chatsString);
            //result.ContentTypes.Add("application/json");

            //return Ok(chats);
            return result;
        }

        [HttpGet("{id}")]
        [Route("chat/{id}")]
        public async Task<IActionResult> GetChat(Guid id)
        {
            var chat = await _chatRepository.FindAsync(id);
            var chatString = JsonSerializer.Serialize<Chat>(chat);

            //Console.WriteLine("chat");
            //Console.WriteLine(chat);
            //var chat2 = JsonSerializer.Deserialize<Chat>(chatString);
            //Console.WriteLine("chat2");
            //Console.WriteLine(chat2);

            var result = Ok(chatString);
            //result.ContentTypes.Add("application/json");

            //return Ok(chat);
            return result;
        }

        [HttpDelete("{id}")]
        [Route("chat/{id}")]
        public async Task<IActionResult> RemoveChat(Guid id)
        {
            var chat = await _chatRepository.FindAsync(id);
            await _chatRepository.RemoveAsync(chat);
            return Ok();
        }

        [HttpPut]
        [Route("chat")]
        public async Task<IActionResult> UpdateChat(string chatString)
        {
            var chat = JsonSerializer.Deserialize<Chat>(chatString);
            await _chatRepository.UpdateAsync(chat);
            return Ok();
        }

    }


    
}

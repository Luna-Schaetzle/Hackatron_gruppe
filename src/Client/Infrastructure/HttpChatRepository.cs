using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using VitualReception.Domain.Model;
using System.Net.Http;
using System.Text.Json;
using System.Linq;
using System.Net.Http.Json;

namespace VirtualReception.Client.Infrastructure
{
    public class HttpChatRepository : IChatRepository
    {

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public HttpChatRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task AddAsync(Chat chat)
        {
            Console.WriteLine("!!! AddAsync");
            var chatString = JsonSerializer.Serialize<Chat>(chat);
            var content = new StringContent(chatString, System.Text.Encoding.UTF8, "application/json");
            
            var response = await _client.PostAsync($"chat", content);
        }

        public async Task<Chat> FindAsync(Guid id)
        {
            var response = await _client.GetAsync($"chat/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var chat = JsonSerializer.Deserialize<Chat>(content, _options);
            return chat;
        }

        public async Task<IEnumerable<Chat>> FindAllAsync()
        {
            HttpResponseMessage response = await _client.GetAsync($"chat/chats");
            Console.WriteLine("!!! FindAllAsync");
            Console.WriteLine(response);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var chats = JsonSerializer.Deserialize<IEnumerable<Chat>>(content, _options); // TODO -  this doesnt work

            for (int i = 0; i < chats.Count(); i++)
            {
                Console.WriteLine(chats.ElementAt(i));
            }

            return chats;
        }

        public async Task RemoveAsync(Chat chat)
        {
            Console.WriteLine("!!! RemoveAsync");
            var chatString = JsonSerializer.Serialize<Chat>(chat);
            var content = new StringContent(chatString, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.DeleteAsync($"chat/{chat.Id}");
        }

        public async Task UpdateAsync(Chat chat)
        {
            Console.WriteLine("!!! UpdateAsync");
            var chatString = JsonSerializer.Serialize<Chat>(chat);
            var content = new StringContent(chatString, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"chat", content);
        }

        public async Task<IEnumerable<Chat>> FindAllByMemberAsync(Guid memberId)
        {
            var chats = await FindAllAsync();
            return chats.Where(chat => chat.Members.Select(x => x.Id).Contains(memberId));
        }

    }
}

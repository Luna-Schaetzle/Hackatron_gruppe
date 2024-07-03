using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VitualReception.Domain.Model
{
    /// <summary>
    /// Represents conversations between different members.
    /// </summary>
    public class Chat
    {
        #region ctors

        /// <summary>
        /// Constructs a new <see cref="Chat"/> instance.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="name">The name of this chat.</param>
        public Chat(Guid id, string name)
        {
            Id = id;
            Name = name;

            Messages = new();
            Members = new();
        }

        [JsonConstructor]
        public Chat(Guid id, string name, List<Message> messages, HashSet<Member> members)
        {
            Id = id;
            Name = name;
            Messages = messages;
            Members = members;
        }

        #endregion

        /// <summary>
        /// The unique identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; }

        /// <summary>
        /// The name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// A collection of <see cref="Message"/> instances.
        /// </summary>
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; init; }

        /// <summary>
        /// A collection of <see cref="Member"/> instances.
        /// </summary>
        [JsonPropertyName("members")]
        public HashSet<Member> Members { get; init; }

        /// <summary>
        /// Creates a new <see cref="Message"/> instance.
        /// </summary>
        /// <param name="memberId">The unique identifier of a <see cref="Member"/> instance.</param>
        /// <param name="content">The content of the <see cref="Message"/> instance.</param>
        /// <returns>The newly created <see cref="Message"/> instance.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Message CreateMessage(Guid memberId, string content)
        {
            var member = Members.SingleOrDefault(member => member.Id == memberId);
            if (member == null)
            { throw new InvalidOperationException(); }

            var message = new Message(Guid.NewGuid(), content, member.Id);
            Messages.Add(message);

            return message;
        }

        public override string ToString()
        {
            string msgs = string.Join(",", Messages.Select(x => x.ToString()).ToArray());
            string mbrs = string.Join(",", Members.Select(x => x.ToString()).ToArray());
            return $"Chat: {Id} - {Name} - {msgs} - {mbrs}";
            //return JsonSerializer.Serialize<Chat>(this);
        }

        #region equals & hashcode

        public override bool Equals(object obj)
        {
            return obj is Chat chat &&
                   Id.Equals(chat.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        #endregion
    }
}

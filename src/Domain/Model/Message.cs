﻿using System;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace VitualReception.Domain.Model
{
    /// <summary>
    /// Represents a chat message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Construcs a new <see cref="Message"/> instance.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="content">The content.</param>
        /// <param name="createdByMember">The identifier of the member who created this message.</param>
        public Message(Guid id, string content, Guid createdByMember)
        {
            Id = id;
            Content = content;
            CreatedDateTime = DateTime.Now;
            CreatedByMember = createdByMember;
        }

        [JsonConstructor]
        public Message(Guid id, string content, DateTime createdDateTime, Guid createdByMember)
        {
            Id = id;
            Content = content;
            CreatedDateTime = createdDateTime;
            CreatedByMember = createdByMember;
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; }

        /// <summary>
        /// The content.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; }

        /// <summary>
        /// The timestamp when this <see cref="Message"/> instance was created.
        /// </summary>
        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; }

        /// <summary>
        /// The identity which created this <see cref="Message"/> instance.
        /// </summary>
        [JsonPropertyName("createdByMember")]
        public Guid CreatedByMember { get; }

        override public string ToString()
        {
            return $"Member: {Id} - {Content} - {CreatedDateTime} - {CreatedByMember}";
        }

        #region equals & hash

        public override bool Equals(object obj)
        {
            return obj is Message message &&
                   Id.Equals(message.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        #endregion
    }
}

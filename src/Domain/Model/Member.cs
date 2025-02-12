﻿using System;
using System.Text.Json.Serialization;

namespace VitualReception.Domain.Model
{
    /// <summary>
    /// Represents an identity that is associated with a <see cref="Chat"/> instance.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Construcs a new <see cref="Member"/> instance.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="name">The name.</param>
        public Member(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// The unqiue identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; }

        /// <summary>
        /// The name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        override public string ToString()
        {
            return $"Member: {Id} - {Name}";
        }

        #region equals & hashcode

        public override bool Equals(object obj)
        {
            return obj is Member member &&
                   Id.Equals(member.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        #endregion
    }
}

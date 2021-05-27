using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.API
{
    internal class MessageInteraction
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("application_id")]
        public ulong ApplicationId { get; set; }
        [JsonProperty("type")]
        public MessageInteractionType Type { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
        [JsonProperty("channel_id")]
        public Optional<ulong> ChannelId { get; set; }
        [JsonProperty("member")]
        public Optional<GuildMember> Member { get; set; }
        [JsonProperty("user")]
        public Optional<User> User { get; set; }
        [JsonProperty("message")]
        public Optional<Message> Message { get; set; }
        [JsonProperty("data")]
        public Optional<ApplicationCommandInteractionData> Data { get; set; }
    }

    public enum MessageInteractionType
    {
        PING = 1,
        APPLICATION_COMMAND = 2,
        MESSAGE_COMPONENT = 3,
    }

    public class ApplicationCommandInteractionData
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("custom_id")]
        public string CustomId { get; set; }
        [JsonProperty("component_type")]
        public int ComponentType { get; set; }
    }
}

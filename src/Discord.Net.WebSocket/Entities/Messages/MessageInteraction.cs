using Discord.WebSocket;
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
        /*[JsonProperty("resolved")]
        public Optional<> Resolved { get; set; }*/
        [JsonProperty("options")]
        public Optional<ApplicationCommandInteractionDataOption[]> Options { get; set; }

        public static async Task Respond(DiscordSocketClient client, ulong interactionId, string interactionToken, string content, bool isPrivate, EmbedBuilder embed = null, MessageComponent[] components = default)
        {
            await client.ApiClient.SendJsonAsync("POST",
            () => $"interactions/{interactionId}/{interactionToken}/callback",
            new
            {
                type = 4,
                data = new
                {
                    content = content,
                    flags = isPrivate ? 64 : 0,
                    components = components,
                    embed = embed?.Build()
                }
            },
            new Discord.API.DiscordRestApiClient.BucketIds(webhookId: interactionId), Discord.Net.Queue.ClientBucketType.SendEdit);
        }
        public static async Task EditResponse(DiscordSocketClient client, string interactionToken, string content, params EmbedBuilder[] embeds)
        {
            ulong id = (await client.GetApplicationInfoAsync()).Id;
            await client.ApiClient.SendJsonAsync("PATCH",
            () => $"webhooks/{id}/{interactionToken}/messages/@original",
            new
            {
                content = content,
                embed = embeds?.Select(i => i.Build())
            },
            new Discord.API.DiscordRestApiClient.BucketIds(webhookId: id), Discord.Net.Queue.ClientBucketType.SendEdit);
        }
        public static async Task Wait(DiscordSocketClient client, ulong interactionId, string interactionToken, bool isPrivate)
        {
            await client.ApiClient.SendJsonAsync("POST",
            () => $"interactions/{interactionId}/{interactionToken}/callback",
            new
            {
                type = 5,
                data = new
                {
                    flags = isPrivate ? 64 : 0
                }
            },
            new Discord.API.DiscordRestApiClient.BucketIds(webhookId: interactionId), Discord.Net.Queue.ClientBucketType.SendEdit);
        }
        public static async Task EditOriginal(DiscordSocketClient client, ulong interactionId, string interactionToken, string content, EmbedBuilder embed = null, MessageComponent[] components = default)
        {
            await client.ApiClient.SendJsonAsync("POST",
            () => $"interactions/{interactionId}/{interactionToken}/callback",
            new
            {
                type = 7,
                data = new
                {
                    content = content,
                    components = components,
                    embed = embed?.Build()
                }
            },
            new Discord.API.DiscordRestApiClient.BucketIds(webhookId: interactionId), Discord.Net.Queue.ClientBucketType.SendEdit);
        }
    }

    public class ApplicationCommandInteractionDataOption
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public ApplicationCommandOptionType Type { get; set; }
        [JsonProperty("value")]
        public Optional<object> Value { get; set; }
        [JsonProperty("options")]
        public Optional<ApplicationCommandInteractionDataOption[]> Options { get; set; }
    }

    public enum ApplicationCommandOptionType
    {
        SUB_COMMAND = 1,
        SUB_COMMAND_GROUP = 2,
        STRING = 3,
        INTEGER = 4,
        BOOLEAN = 5,
        USER = 6,
        CHANNEL = 7,
        ROLE = 8,
        MENTIONABLE = 9
    }
}

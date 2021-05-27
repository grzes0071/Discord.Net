using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageComponent
    {
        public static readonly Dictionary<string, Action<ITextChannel, IMessage, IUser, bool, ulong, string>> Callbacks = new Dictionary<string, Action<ITextChannel, IMessage, IUser, bool, ulong, string>>();

        [JsonProperty("type")]
        public int Type { get; }

        [JsonProperty("components", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Optional<MessageComponent[]> Components { get; set; }
        [JsonProperty("style")]
        public Optional<int> Style { get; set; }
        [JsonProperty("label")]
        public Optional<string> Label { get; set; }
        [JsonProperty("emoji", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Optional<string> Emoji { get; set; }
        [JsonProperty("custom_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Optional<string> CustomId { get; set; }
        [JsonProperty("url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Optional<string> URL { get; set; }
        [JsonProperty("disabled", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Optional<bool> Disabled { get; set; }

        /// Channel, Message, User, IsFromGuild, InteractionId, InteractionToken
        public Action<ITextChannel, IMessage, IUser, bool, ulong, string> Callback { get; }

        public MessageComponent(int type)
        {
            Type = type;
        }

        private MessageComponent(int style, string lable, Emote emoji, string customId, Action<ITextChannel, IMessage, IUser, bool, ulong, string> callback, string url = "", bool disabled = false)
        {
            Type = 2;
            Style = style;
            Label = lable;
            if(emoji != null)
                Emoji = emoji.Id.ToString();
            if(customId != null)
                CustomId = customId;
            if (!string.IsNullOrWhiteSpace(url))
                URL = url;
            Disabled = disabled;
            Callback = callback;
        }

        public static MessageComponent CreateButton(ButtonType style, string lable, Emote emoji, string customId, Action<ITextChannel, IMessage, IUser, bool, ulong, string> callback, bool disabled = false)
        {
            return new MessageComponent((int)style, lable, emoji, customId, callback, null, disabled);
        }

        public static MessageComponent CreateButtonLink(string lable, Emote emoji, string url, bool disabled = false)
        {
            return new MessageComponent(5, lable, emoji, null, null, url, disabled);
        }
        public static MessageComponent CreateActionRow(MessageComponent[] components)
        {
            return new MessageComponent(1)
            {
                Components = components
            };
        }

        public enum ButtonType
        {
            PRIMARY = 1,
            SECONDARY = 2,
            SUCCESS = 3,
            DANGER = 4,
        }
    }
}

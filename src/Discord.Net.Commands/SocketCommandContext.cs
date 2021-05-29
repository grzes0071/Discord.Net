using System;
using System.Threading.Tasks;

namespace Discord.Commands
{
    public class SlashCommandContext : ICommandContext
    {
        public IDiscordClient Client { get; }
        public IGuild Guild { get; }
        public IMessageChannel Channel { get; }
        public IUser User { get; }
        public IUserMessage Message => throw new System.Exception("THIS IS SLAHS COMMAND | No Message");
        public bool IsPrivate => Channel is IPrivateChannel;
        public ulong InteractionId { get; }
        public string InteractionToken { get; }
        public object InteractionData { get; }

        public virtual Task Respond(string content, bool isPrivate, EmbedBuilder embed = null, MessageComponent[] components = default)
            => throw new NotImplementedException("To use this method it has to overrided by child");
        public virtual Task EditResponse(string content, params EmbedBuilder[] embeds)
            => throw new NotImplementedException("To use this method it has to overrided by child");
        public virtual Task Wait(bool isPrivate)
            => throw new NotImplementedException("To use this method it has to overrided by child");
        public virtual Task EditOriginal(string content, EmbedBuilder embed = null, MessageComponent[] components = default)
            => throw new NotImplementedException("To use this method it has to overrided by child");

        public SlashCommandContext(IDiscordClient client, IMessageChannel channel, IUser user, (ulong interactionId, string interactionToken) interactionCredentials, object interactionData)
        {
            Client = client;
            Guild = (channel as IGuildChannel)?.Guild;
            Channel = channel;
            User = user;
            InteractionId = interactionCredentials.interactionId;
            InteractionToken = interactionCredentials.interactionToken;
            InteractionData = interactionData;
        }
    }
}

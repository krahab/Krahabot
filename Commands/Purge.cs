using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Krahabot
{
    public class Purge
    {
        const int MAX_VALUE = 20;

        public async void ProcessPurge(SocketMessage message, int nbOfMessage)
        {
            if (nbOfMessage <= 0)
            {
                await ReplyAsync(message, "The amount of messages to remove must be positive.");
                return;
            }
            else if(nbOfMessage > MAX_VALUE)
            {
                await ReplyAsync(message, "The amount of messages to remove must be under " + MAX_VALUE);
                return;
            }
            await message.Channel.DeleteMessageAsync(message);

            var messages = await message.Channel.GetMessagesAsync(message, Direction.Before, nbOfMessage).FlattenAsync();

            var filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14);

            var count = filteredMessages.Count();

            if (count == 0)
                await ReplyAsync(message, "Nothing to delete.");
            else
            {
                await (message.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);
                await ReplyAsync(message, $"Done. Removed {count} {(count > 1 ? "messages" : "message")}.");
            }
        }

        async Task ReplyAsync(SocketMessage message, string newMessage)
        {
            message.Channel.SendMessageAsync(newMessage);
        }
    }
}
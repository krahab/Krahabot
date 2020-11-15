using Discord;
using Discord.WebSocket;
using Krahabot;
using Krahabot.Commands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Krahabot
{
    public class Command
    {
        private const string prefix = "!";
        readonly Member member = new Member();
        Purge purge = new Purge();
        TachireShame tachireShame = new TachireShame();
        public Task CommandHandler(SocketMessage message)
        {
            //Message filter
            if (message.Author.IsBot) return Task.CompletedTask;
            SocketGuildChannel guild = message.Channel as SocketGuildChannel;
            member.AddMember(message, guild);
            
            if (!message.Content.StartsWith(prefix)) return Task.CompletedTask;


            //Command creation
            string command;
            string subMessage;
            int value;
            int lengthOfCommand;
            if (message.Content.Contains(" "))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;
            command = message.Content.Substring(1, lengthOfCommand - 1);
            subMessage = message.Content.Substring(message.Content.IndexOf(command) + command.Length);
            int.TryParse(subMessage, out value);

            //Commands
            switch (command)
            {
                case "ping":
                case "Ping":
                case "PING":
                    message.Channel.SendMessageAsync($@"You said something {message.Author.Mention}");
                    break;

                case "test":
                case "Test":
                case "TEST":
                    message.Channel.SendMessageAsync("Testing c#");
                    break;

                case "uno":
                case "Uno":
                case "UNO":
                    message.Channel.SendMessageAsync("Sudoku");
                    break;

                case "addchannel":
                case "AddChannel":
                case "ADDCHANNEL":
                    message.Channel.SendMessageAsync("You want to add this channel to the notification system");
                    break;

                case "reboot":
                case "Reboot":
                case "REBOOT":
                    member.VerifyIfUserIsAdmin(message);
                    break;

                case "purge":
                case "Purge":
                case "PURGE":
                    if (member.VerifyIfUserIsAdmin(message))purge.ProcessPurge(message, value);
                    break;

                case "addadmin":
                case "AddAdmin":
                case "ADDADMIN":
                    if (member.VerifyIfUserIsAdmin(message)) new AddAdmin(message, guild, subMessage);
                    break;

                default:
                    message.Channel.SendMessageAsync(message.Content + " is not an available command, please try again");
                    break;
            }

            return Task.CompletedTask;
        }

        public Task ShameTachire(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
        {
            TachireShame tachireShame = new TachireShame();
            tachireShame.ShameTachire(user, oldState, newState);
            return Task.CompletedTask;
        }
    }
}

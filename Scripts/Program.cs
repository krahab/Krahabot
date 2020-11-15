using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Krahabot
{
    public class Program
    {
        readonly FileFinder fileFinder = new FileFinder();
        public static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        Command command = new Command();
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.MessageReceived += command.CommandHandler;
            _client.UserVoiceStateUpdated += HandleUserVoiceStateUpdated;
            _client.Log += Log;


            var token = File.ReadAllText(fileFinder.GetToken());
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task HandleUserVoiceStateUpdated(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
        {
            command.ShameTachire(user, oldState, newState);
            return Task.CompletedTask;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}

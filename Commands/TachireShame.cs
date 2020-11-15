using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Discord.Audio;
using Discord.WebSocket;


namespace Krahabot.Commands
{
    class TachireShame
    {
        String tachireId = "375435321489358850";
        String temporaryId = "252487218340429834";
        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();

        public void ShameTachire(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
        {
            if(oldState.IsStreaming == false)
            {
                if (newState.IsStreaming && user.Id.ToString() == temporaryId)
                {
                    ConnectToVoiceChannel(newState.VoiceChannel);
                }
            }
        }

        private async Task ConnectToVoiceChannel(SocketVoiceChannel voiceChannel)
        {
            Console.WriteLine("Connection to voice channel");
            var test = Task.Run(async() => {
                var audioClient = await voiceChannel.ConnectAsync();
                FileFinder fileFinder = new FileFinder();

                await SendAsync((IAudioClient)audioClient, fileFinder.GetApplauseMp3());
            });
            Console.WriteLine("Connected to voice channel");
            //await DisconnectFromVoiceChannel(voiceChannel);
        }


        private async Task SendAsync(IAudioClient client, string path)
        {
            Console.WriteLine("Initiating message and outputting it");
            // Create FFmpeg using the previous example
            using (var ffmpeg = CreateStream(path))
            using (var output = ffmpeg.StandardOutput.BaseStream)
            using (var discord = client.CreatePCMStream(AudioApplication.Mixed))
            {
                try { await output.CopyToAsync(discord); }
                finally { await discord.FlushAsync(); }
            }
        }
        private Process CreateStream(string filePath)
        {
            Console.WriteLine("Creating stream");
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-hide_banner -loglevel panic -i \"{filePath}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
        }
        private async Task DisconnectFromVoiceChannel(SocketVoiceChannel voiceChannel)
        {
            Console.WriteLine("Disconnecting from voice channel");

            var connection = voiceChannel.DisconnectAsync();
        }
    }
}

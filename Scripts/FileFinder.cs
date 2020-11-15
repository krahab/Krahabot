using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Krahabot
{
    public class FileFinder
    {
        //Important
        readonly string token = @"C:\Users\remys\Desktop\DiscordBots\Krahabot\Krahabot\Krahabot\Data\token.txt";
        readonly string adminData = @"C:\Users\remys\Desktop\DiscordBots\Krahabot\Krahabot\Krahabot\Data\adminData.txt";

        //Command related
        readonly string serverFolder = @"C:\Users\remys\Desktop\DiscordBots\Krahabot\Krahabot\Krahabot\Data\Servers\";
        readonly string member = @"C:\Users\remys\Desktop\DiscordBots\Krahabot\Krahabot\Krahabot\Data\Servers\";
        readonly string applauseMp3 = @"C:\Users\remys\Desktop\DiscordBots\Krahabot\Krahabot\Krahabot\Data\Sound\Applause.mp3";

        public string GetToken()
        {
            return token;
        }
        public string GetAdminData()
        {
            return adminData;
        }
        public string GetServerFolder()
        {
            return serverFolder;
        }
        public string GetMember(string serverId)
        {
            string fullMemberPath = @""+ member + serverId + ".txt";
            return fullMemberPath;
        }

        public string GetApplauseMp3()
        {
            return applauseMp3;
        }
    }
}

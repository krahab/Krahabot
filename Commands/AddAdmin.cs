using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace Krahabot
{
    class AddAdmin
    {
        readonly private string user;
        readonly Member member = new Member();
        readonly FileFinder fileFinder = new FileFinder();
        public AddAdmin(SocketMessage message, SocketGuildChannel guild,  string newSubMessage)
        {
            if (newSubMessage[1] != '<') 
            {
                message.Channel.SendMessageAsync("You need to give a proper name tag, please try again");
                return;
            }
            var charsToRemove = new string[] { "@", "!", "<", ">", " "};
            foreach (var c in charsToRemove)
            {
                newSubMessage = newSubMessage.Replace(c, string.Empty);
            }
            user = newSubMessage;
            SetNewAdmin(message, guild);
        }
         
        private void SetNewAdmin(SocketMessage message, SocketGuildChannel guild)
        {
            string path = fileFinder.GetMember(guild.Guild.Id.ToString() + "M");
            string[] membersByLine = File.ReadAllLines(path);
            foreach (string line in membersByLine)
            {
                if (line != "")
                {
                    if (user == line.Split(' ', ' ')[member.ID_MARKER])
                    {
                        line.Replace("Member", "Admin");
                        if (line.Split(' ', ' ')[member.IS_ADMIN] == "Admin")
                        {
                            message.Channel.SendMessageAsync("You have set the level");
                        }
                        else
                        {
                            message.Channel.SendMessageAsync("Didn't work, try again ;)");
                        }
                        break;
                    }
                }
            }
        }
    }
}

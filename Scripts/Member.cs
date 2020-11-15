using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Krahabot
{
    class Member
    {
        readonly FileFinder fileFinder = new FileFinder();
        string path;
        public int ID_MARKER = 0;
        public int IS_ADMIN = 1;
        public int XP_MARKER = 2;
        public int LVL_MARKER = 3;
        public int NAME_MARKER = 4;

        public void AddMember(SocketMessage message, SocketGuildChannel guild)
        {
            path = fileFinder.GetServerFolder() +  guild.Guild.Id.ToString() + "M" + ".txt";
            if (!File.Exists(path))
            {
                message.Channel.SendMessageAsync("Adding a database for the server's ID " + guild.Guild.Id.ToString());
                string serverFolder = fileFinder.GetServerFolder();
                using (StreamWriter sw = File.CreateText(path));
            }
            if (!VerifyIfMemberExists(message))
            {
                string databaseInfo = message.Author.Id.ToString() + " ";
                if(guild.Guild.OwnerId == message.Author.Id) databaseInfo += "Admin ";
                else databaseInfo += "Member ";
                databaseInfo += "0 ";
                databaseInfo += "0 ";
                databaseInfo += message.Author.Username.Replace(" ", "") + " ";
                File.AppendAllText(path, databaseInfo + Environment.NewLine);
            }
        }

        private bool VerifyIfMemberExists(SocketMessage message)
        {
            string[] membersByLine = File.ReadAllLines(path);
            string memberId = message.Author.Id.ToString();
            bool exists = false;
            foreach (string line in membersByLine)
            {
                if(line != "")
                {
                    if (memberId == line.Split(' ', ' ')[ID_MARKER])
                    {
                        exists = true;
                        break;
                    }
                }
            }
            if (!exists)
            {
                message.Channel.SendMessageAsync("Welcome in the database " + message.Author.Username + " ! *now let me have all your private informations hehe*");
            }
            return exists;
        }
        public bool VerifyIfUserIsAdmin(SocketMessage message)
        {
            string[] membersByLine = File.ReadAllLines(path);
            string memberId = message.Author.Id.ToString();
            bool isAdmin = false;
            foreach (string line in membersByLine)
            {
                if (line != "")
                {
                    if (memberId == line.Split(' ', ' ')[ID_MARKER] && line.Split(' ', ' ')[IS_ADMIN] == "Admin")
                    {
                        isAdmin = true;
                        break;
                    }
                }
            }
            if(isAdmin) message.Channel.SendMessageAsync("You are an admin " + message.Author.Username + "!");
            if (!isAdmin) message.Channel.SendMessageAsync("You are not an admin " + message.Author.Username + "! (Please ask an admin to do this command)");
            return isAdmin;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCPGoBang.Model
{
    public class GamePlayer
    {
        private IPAddress ip;
        private string name;
        private Session session;

        public GamePlayer(string ip, string name)
        {
            this.ip = IPAddress.Parse(ip);
            this.name = name;
        }

        public IPAddress Ip { get => ip; set => ip = value; }
        public string Name { get => name; set => name = value; }
        public Session Session { get => session; set => session = value; }

        public override bool Equals(object Player)
        {
            if (Player is GamePlayer)
            {
                GamePlayer player = (GamePlayer)Player;
                if (player.Ip == this.Ip && player.Name == this.Name)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public string ConvertToMsg()
        {
            return ip.ToString() + "," + Name;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}

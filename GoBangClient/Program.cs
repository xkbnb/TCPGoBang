using GoBangClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPGoBang;
using TCPGoBang.Model;

namespace GoBangClient
{
    static class Program
    {
        public static GameController gameController;
        public static GamePlayer gamePlayer;
        public static GameHall gameHall;
        public static GamePlaying gamePlaying;
        public static void ServerClose(object sender,NetEventArgs e)
        {
            
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}

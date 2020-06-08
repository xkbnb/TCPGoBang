using GoBangClient.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPGoBang;
using TCPGoBang.Model;

namespace GoBangClient
{
    public partial class Login : Form
    {
        private int clientPort = 10001;
        string ip;
        public Login()
        {
            InitializeComponent();
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("www.baidu.com", 80);
                ip = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            }
            catch
            {

            }
            finally
            {
                tcpClient.Close();
            }
        }

        /// <summary>
        /// 点击登录按钮连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_login_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(tb_serverIP.Text.Trim(' '));
                if(tb_userName.Text.Trim() == "")
                {
                    MessageBox.Show("请输入玩家名称！");
                    return;
                }
                Program.gameController = new GameController(6);
                Program.gameController.TcpClienter.ConnectedServer += new TcpClienter.NetEvent(Connected);
                Program.gameController.TcpClienter.ReceiveDataGram += new TcpClienter.NetEvent(DealMsg);
                Program.gameController.TcpClienter.Connect(ip, clientPort);
            }
            catch(FormatException)
            {
                MessageBox.Show("服务器IP输入的格式有误");
            }
            catch
            {
                MessageBox.Show("无法连接服务器");
            }
        }


        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DealMsg(object sender, NetEventArgs e)
        {
            MessagePackage messagePackage = new MessagePackage(e.Session.Datagram);
            switch (messagePackage.MsgType)
            {
                case MsgType.Connect:
                    {
                        if (messagePackage.Data.Equals("success"))
                        {
                            if (IsDisposed || !this.IsHandleCreated) return;
                            this.Invoke((Action)delegate ()
                            {
                                Program.gameController.TcpClienter.ReceiveDataGram += null;
                                Program.gameHall = new GameHall();
                                Program.gameHall.Show();
                                this.Visible = false;
                            });
                        }
                        else
                        {
                            MessageBox.Show(messagePackage.Data);
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connected(object sender, NetEventArgs e)
        {
            Program.gamePlayer = new TCPGoBang.Model.GamePlayer(ip, tb_userName.Text.Trim());
            MessagePackage messagePackage = new MessagePackage(MsgType.Connect, Program.gamePlayer.ConvertToMsg(), Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
            Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_cancle_Click(object sender, EventArgs e)
        {
            if(Program.gameController != null)
            {
                Program.gameController.Close();
            }
            Application.Exit();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPGoBang;
using TCPGoBang.Model;

namespace GoBangClient
{
    public partial class CreatRoom : Form
    {
        private string roomName;
        public CreatRoom()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击创建房间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_create_Click(object sender, EventArgs e)
        {
            roomName = tb_room.Text.ToString().Trim();
            if(roomName == "" || roomName == null)
            {
                MessageBox.Show("请输入房间名称！");
            }
            else
            {
                MessagePackage messagePackage = new MessagePackage(MsgType.CreateRoom, roomName, Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, "");
                Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
            }
        }

        private void CreatRoom_Load(object sender, EventArgs e)
        {
            Program.gameController.TcpClienter.ReceiveDataGram += new TcpClienter.NetEvent(DealMsg);
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
                case MsgType.CreateRoom:
                    {
                        if (messagePackage.Data.Equals("success")){
                            if (IsDisposed || !this.IsHandleCreated) return;
                            this.Invoke((Action)delegate ()
                            {
                                Program.gameController.TcpClienter.ReceiveDataGram += null;
                                Program.gamePlaying = new GamePlaying(roomName, Program.gamePlayer.Name);
                                Program.gamePlaying.Show();
                                Program.gameHall.Visible = false;
                                this.Close();
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
    }
}

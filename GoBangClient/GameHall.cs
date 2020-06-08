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
    public partial class GameHall : Form
    {
        string clickedRoomName;
        public GameHall()
        {
            InitializeComponent();
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
                case MsgType.UserList:
                    {
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            lv_player.Items.Clear();
                            string[] players = messagePackage.Data.Split(',');
                            foreach (string s in players)
                            {
                                ListViewItem lvi = new ListViewItem(s);
                                lv_player.Items.Add(lvi);
                            }
                        });
                        break;
                    }
                case MsgType.RoomList:
                    {
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            lv_room.Items.Clear();
                            string[] rooms = messagePackage.Data.Split(',');
                            if (rooms.Length == 1)     //没有房间
                            {
                                return;
                            }
                            foreach (string r in rooms)
                            {
                                string[] names = r.Split(';');
                                if (names.Length == 1)
                                {
                                    return;
                                }
                                ListViewItem lvi = new ListViewItem(names[0]);
                                lvi.SubItems.Add(names[1]);
                                lvi.SubItems.Add(names[2] + "/2");
                                lv_room.Items.Add(lvi);
                            }
                        });
                        break;
                    }
                case MsgType.JoinRoom:
                    {
                        if (messagePackage.Data.Equals("success"))
                        {
                            if (IsDisposed || !this.IsHandleCreated) return;
                            this.Invoke((Action)delegate ()
                            {
                                ListViewItem lvi = lv_room.SelectedItems[0];
                                string roomName = lvi.SubItems[0].Text;
                                string roomMasterName = lvi.SubItems[1].Text;
                                string roomParticipantName = Program.gamePlayer.Name;
                                Program.gameController.TcpClienter.ReceiveDataGram += null;

                                Program.gamePlaying = new GamePlaying(roomName, roomMasterName, roomParticipantName);
                                Program.gamePlaying.Show();
                                this.Visible = false;
                            });
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_createRoom_Click(object sender, EventArgs e)
        {
            CreatRoom creatRoom = new CreatRoom();
            creatRoom.ShowDialog();
        }

        private void GameHall_Load(object sender, EventArgs e)
        {
            if (this.IsHandleCreated && this.lv_room.IsHandleCreated && this.lv_player.IsHandleCreated)
            {
                //监听房间和玩家信息
                AskRoomListToServer();
                AskUserListToServer();
            }
        }

        /// <summary>
        /// 监听玩家信息
        /// </summary>
        private void AskUserListToServer()
        {

            MessagePackage mp = new MessagePackage(MsgType.UserList, "", Program.gamePlayer.Name, Program.gamePlayer.Ip.ToString(), DateTime.Now.ToString());
            Program.gameController.TcpClienter.Send(mp.ConvertToString());
        }

        /// <summary>
        /// 监听房间信息
        /// </summary>
        private void AskRoomListToServer()
        {
            MessagePackage messagePackage = new MessagePackage(MsgType.RoomList, "", Program.gamePlayer.Name, Program.gamePlayer.Ip.ToString(), DateTime.Now.ToString());
            Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
        }

        /// <summary>
        /// 房间列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_room_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.lv_room.FocusedItem != null)
            {
                //选取了其中一行，避免第二次选择一行时count值出现先归0再置1的情况
                if (lv_room.SelectedItems.Count == 1)
                {
                    ListViewItem lvi = lv_room.SelectedItems[0];
                    clickedRoomName = lvi.SubItems[0].Text;
                }
            }
        }

        /// <summary>
        /// 加入房间点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (clickedRoomName != "" ||clickedRoomName != null)
            {
                MessagePackage messagePackage = new MessagePackage(MsgType.JoinRoom, clickedRoomName, Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
            }
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameHall_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDisposed || !this.IsHandleCreated) return;
            this.Invoke((Action)delegate
            {
                this.FormClosing -= new FormClosingEventHandler(this.GameHall_FormClosing);
                if (MessageBox.Show("退出游戏？", "退出", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MessagePackage messagePackage = new MessagePackage(MsgType.Exit, "", "", Program.gamePlayer.Name, DateTime.Now.ToString());
                    Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                    this.Close();
                }
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPGoBang;
using TCPGoBang.Model;

namespace GoBangServer
{
    public partial class MainForm : Form
    {
        public TcpServer tcpServer;
        public ushort port = 10001;

        Stopwatch sw = new Stopwatch();

        List<GamePlayer> userList = new List<GamePlayer>();
        List<GameRoom> roomList = new List<GameRoom>();


        public MainForm()
        {
            InitializeComponent();
            string ip;
            lb_Port.Text = port.ToString();
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("www.baidu.com", 80);
                ip = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
                lb_IP.Text = ip;
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
        /// 点击开启服务按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_start_Click(object sender, EventArgs e)
        {
            try
            {
                tcpServer = new TcpServer(port);
                tcpServer.ClientClose += new TcpServer.NetEvent(ClientClose);
                tcpServer.RecvData += new TcpServer.NetEvent(RecvData);
                tcpServer.Start();
                bt_start.Enabled = false;
                bt_stop.Enabled = true;
                sw.Start();
                timer1.Enabled = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入0-65536以内的整数数字");
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接网络，或服务器端口被占用");
            }
        }

        /// <summary>
        /// 点击关闭服务按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_stop_Click(object sender, EventArgs e)
        {
            tcpServer.Stop();
            bt_start.Enabled = true;
            bt_stop.Enabled = false;
            sw.Stop();
            sw.Reset();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lb_OpenTime.Text = sw.Elapsed.ToString("G");
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="s"></param>
        private void UserExit(Session s)
        {
            GamePlayer gamePlayer = userList.Where(o => o.Session == s).ToList<GamePlayer>()[0];
            int index = userList.IndexOf(gamePlayer); 
            this.Invoke((Action)delegate ()
            {
                listView1.Items.RemoveAt(index);
                userList.Remove(gamePlayer);
                lb_PlayerCount.Text = userList.Count.ToString();
            });
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecvData(object sender, NetEventArgs e)
        {
            Task task = new Task((Action)delegate ()
            {
                MessagePackage messagePackage = new MessagePackage(e.Session.Datagram);
                Session session = (Session)tcpServer.SessionTable[e.Session.SessionId];
                DoGet(messagePackage, session);
            });
            task.Start();
        }

        /// <summary>
        /// 获取玩家列表
        /// </summary>
        /// <returns></returns>
        private string GetUserList()
        {
            string msg = "";
            foreach (GamePlayer gamePlayer in userList)
            {
                msg = string.Concat(msg, gamePlayer.Name, ",");
            }
            return msg;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientClose(object sender, NetEventArgs e)
        {
            UserExit(e.Session);
            MessagePackage messagePackage = new MessagePackage(MsgType.UserList, GetUserList(), "", "", DateTime.Now.ToString());
            foreach (Session session in tcpServer.SessionTable.Values)
            {
                tcpServer.Send(session, messagePackage.ConvertToString());
            }
        }

        /// <summary>
        /// 处理消息类型
        /// </summary>
        /// <param name="messagePackage"></param>
        /// <param name="session"></param>
        private void DoGet(MessagePackage messagePackage, Session session)
        {
            switch (messagePackage.MsgType)
            {
                case MsgType.Connect:
                    {
                        string[] msg = messagePackage.Data.Split(',');
                        if (ConnNewUser(msg[0], msg[1], session))
                        {
                            messagePackage = new MessagePackage(MsgType.Connect, "success", "", "", DateTime.Now.ToString());
                            tcpServer.Send(session, messagePackage.ConvertToString());
                            messagePackage = new MessagePackage(MsgType.UserList, GetUserList(), "", "", DateTime.Now.ToString());
                            foreach(Session s in tcpServer.SessionTable.Values)
                            {
                                tcpServer.Send(s, messagePackage.ConvertToString());
                            }
                        }
                        else
                        {
                            messagePackage = new MessagePackage(MsgType.Connect, "该用户已存在", "", "", DateTime.Now.ToString());
                            tcpServer.Send(session, messagePackage.ConvertToString());
                        }
                        break;
                    }
                case MsgType.RoomList:
                    {
                        messagePackage =new MessagePackage(MsgType.RoomList,GetRoomList(), "", "", DateTime.Now.ToString());
                        tcpServer.Send(session, messagePackage.ConvertToString());
                        break;
                    }
                case MsgType.UserList:
                    {
                        messagePackage = new MessagePackage(MsgType.UserList, GetUserList(), "", "", DateTime.Now.ToString());
                        tcpServer.Send(session, messagePackage.ConvertToString());
                        break;
                    }
                case MsgType.CreateRoom:
                    {
                        if (CreateNewRoom(messagePackage.SenderName, messagePackage.Data))
                        {
                            messagePackage = new MessagePackage(MsgType.CreateRoom, "success", "", "", DateTime.Now.ToString());
                            tcpServer.Send(session, messagePackage.ConvertToString());
                            messagePackage = new MessagePackage(MsgType.RoomList, GetRoomList(), "", "", DateTime.Now.ToString());
                            foreach(Session s in tcpServer.SessionTable.Values)
                            {
                                tcpServer.Send(s, messagePackage.ConvertToString());
                            }
                        }
                        else
                        {
                            messagePackage = new MessagePackage(MsgType.CreateRoom, "房间名已被占用", "", "", DateTime.Now.ToString());
                            tcpServer.Send(session, messagePackage.ConvertToString());
                        }
                        break;
                    }
                case MsgType.JoinRoom:
                    {
                        GameRoom gameRoom = SearchRoomByRoomName(messagePackage.Data);
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        if(gameRoom != null)   //可以找到该房间
                        {
                            if(gameRoom.PlayerNumber == 1)//该房间只有房主一人
                            {
                                gameRoom.JoinRoom(gamePlayer);
                                messagePackage = new MessagePackage(MsgType.JoinRoom, "success", "", "", "");
                                tcpServer.Send(session, messagePackage.ConvertToString());
                                messagePackage = new MessagePackage(MsgType.JoinRoom, gamePlayer.Name, "", "", "");
                                tcpServer.Send(gameRoom.GamePlayerMaseter.Session, messagePackage.ConvertToString());
                                messagePackage = new MessagePackage(MsgType.RoomList, GetRoomList(), "", "", DateTime.Now.ToString());
                                foreach(Session s in tcpServer.SessionTable.Values)
                                {
                                    tcpServer.Send(session, messagePackage.ConvertToString());
                                }
                            }
                        }
                        break;
                    }
                case MsgType.Start:
                    {
                        GameRoom gameRoom = SearchRoomBySenderName(messagePackage.SenderName);
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        gameRoom.ReadyNumber++;
                        if(gameRoom.ReadyNumber == 2)
                        {
                            messagePackage = new MessagePackage(MsgType.Start, "", "", "", "");
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                            gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                            gameRoom.ReadyNumber = 0;
                        }
                        else
                        {
                            gamePlayer = gamePlayer == gameRoom.GamePlayerParticipant ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                            messagePackage = new MessagePackage(MsgType.Wait, "", "", "", "");
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        }
                        break;
                    }
                case MsgType.LuoZi:
                    {
                        GameRoom gameRoom = SearchRoomBySenderName(messagePackage.SenderName);
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                        tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        break;
                    }
                case MsgType.IsWin:
                    {
                        GameRoom gameRoom = SearchRoomBySenderName(messagePackage.SenderName);
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                        messagePackage.MsgType = MsgType.IsWin;
                        tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        gameRoom.ReadyNumber = 0;
                        break;
                    }
                case MsgType.Msg:
                    {
                        GameRoom gameRoom = SearchRoomBySenderName(messagePackage.SenderName);
                        if(gameRoom.PlayerNumber == 2)
                        {
                            GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                            gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        }
                        break;
                    }
                case MsgType.Quit:
                    {
                        GameRoom gameRoom = SearchRoomBySenderName(messagePackage.SenderName);
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        gameRoom.QuitRoom(gamePlayer);
                        if(gameRoom.PlayerNumber == 0)
                        {
                            roomList.Remove(gameRoom);
                        }
                        else
                        {
                            messagePackage = new MessagePackage(MsgType.Quit, "", "", "", "");
                            tcpServer.Send(gameRoom.GamePlayerMaseter.Session, messagePackage.ConvertToString());
                        }
                        messagePackage = new MessagePackage(MsgType.RoomList, GetRoomList(), "", "", DateTime.Now.ToString());
                        foreach (Session s in tcpServer.SessionTable.Values)
                        {
                            tcpServer.Send(s, messagePackage.ConvertToString());
                        }
                        break;
                    }
                case MsgType.Restart:
                    {
                        GameRoom gameRoom = SearchRoomBySenderName(messagePackage.SenderName);
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        gameRoom.ReadyNumber++;
                        if(gameRoom.ReadyNumber == 2)
                        {
                            gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerMaseter : gameRoom.GamePlayerParticipant;
                            messagePackage = new MessagePackage(MsgType.Restart, "", "", "", "");
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                            gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                            gameRoom.ReadyNumber = 0;
                        }
                        else
                        {
                            messagePackage = new MessagePackage(MsgType.Wait, "", "", "", "");
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        }
                        break;
                    }
                case MsgType.Reconcile:
                    {
                        GameRoom gameRoom = SearchRoomBySenderName(messagePackage.SenderName);
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        if (messagePackage.Data.Equals("reconcile"))
                        {
                            messagePackage = new MessagePackage(MsgType.Reconcile, "reconcile", "", "", "");
                            gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        }
                        if (messagePackage.Data.Equals("agree"))
                        {
                            messagePackage = new MessagePackage(MsgType.Reconcile, "agree", "", "", "");
                            gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        }
                        if (messagePackage.Data.Equals("disagree"))
                        {
                            messagePackage = new MessagePackage(MsgType.Reconcile, "disagree", "", "", "");
                            gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                            tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        }
                        break;
                    }
                case MsgType.Confess:
                    {
                        GameRoom gameRoom = SearchRoomBySenderName(messagePackage.SenderName);
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        gamePlayer = gamePlayer == gameRoom.GamePlayerMaseter ? gameRoom.GamePlayerParticipant : gameRoom.GamePlayerMaseter;
                        tcpServer.Send(gamePlayer.Session, messagePackage.ConvertToString());
                        break;
                    }
                case MsgType.Exit:
                    {
                        GamePlayer gamePlayer = SearchUserByName(messagePackage.SenderName);
                        tcpServer.ClientClose += new TcpServer.NetEvent(ClientClose);
                        UserExit(gamePlayer.Session);
                        break;
                    }
            }
        }

        /// <summary>
        /// 创建新房间
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CreateNewRoom(string senderName, string roomName)
        {
            foreach(GameRoom r in roomList)
            {
                if (r.RoomName.Equals(roomName))
                {
                    return false;
                }
            }
            GamePlayer gamePlayer = SearchUserByName(senderName);
            GameRoom gameRoom = new GameRoom(roomName, gamePlayer);
            roomList.Add(gameRoom);
            //this.Invoke((Action)delegate ()
            //{
            //    ListViewItem lvi = new ListViewItem(roomName);
            //    lvi.SubItems.Add(senderName);
            //    lvi.SubItems.Add(gameRoom.PlayerNumber.ToString());
            //    listView1.Items.Add(lvi);
            //});
            return true;
        }

        /// <summary>
        /// 通过发送者名称查找发送者
        /// </summary>
        /// <param name="senderName"></param>
        /// <returns></returns>
        private GamePlayer SearchUserByName(string senderName)
        {
            foreach(GamePlayer gamePlayer in userList)
            {
                if (gamePlayer.Name.Equals(senderName))
                {
                    return gamePlayer;
                }
            }
            return null;
        }


        /// <summary>
        /// 通过房间名称查找房间
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private GameRoom SearchRoomByRoomName(string data)
        {
            foreach (GameRoom r in roomList)
            {
                if(r.RoomName.Equals(data))
                {
                    return r;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过发送者名称查找房间
        /// </summary>
        /// <param name="senderName"></param>
        /// <returns></returns>
        private GameRoom SearchRoomBySenderName(string senderName)
        {
            foreach(GameRoom gameRoom in roomList)
            {
                if (gameRoom.GamePlayerMaseter.Name.Equals(senderName) || gameRoom.GamePlayerParticipant.Name.Equals(senderName))
                {
                    return gameRoom;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <returns></returns>
        private string GetRoomList()
        {
            string msg = "";
            foreach(GameRoom room in roomList)
            {
                msg = string.Concat(msg, room.RoomName, ";", room.GamePlayerMaseter.Name, ";", room.PlayerNumber.ToString(), ",");
            }
            return msg;
        }

        /// <summary>
        /// 新玩家连接服务端
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        private bool ConnNewUser(string ip, string name, Session session)
        {
            foreach(GamePlayer gamePlayer in userList)
            {
                if(gamePlayer.Name == name)
                {
                    return false;
                }
            }
            GamePlayer gamePlayer1 = new GamePlayer(ip, name);
            gamePlayer1.Session = session;
            userList.Add(gamePlayer1);
            this.BeginInvoke((Action)delegate ()
            {
                ListViewItem lvi = new ListViewItem(ip);
                lvi.SubItems.Add(port.ToString());
                lvi.SubItems.Add(name);
                lvi.SubItems.Add("在线");
                listView1.Items.Add(lvi);
                lb_PlayerCount.Text = userList.Count.ToString().Trim();
            });
            return true;
        }
    }
}

using GoBangClient.Model;
using GoBangClient.Util;
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
    public partial class GamePlaying : Form
    {
        delegate void LuoziLogic(MouseEventArgs e);
        LuoziLogic Luo;
        bool turnFlag;  //用于标识目前为哪一方下棋
        int flag;   //房主为1，其他玩家为2,同时用于标识棋盘中玩家下的棋子
        bool start; //游戏开始标识
        int masterScore = 0;             //房主初始分数
         int participantScore = 0;   //参与者初始分数
        string roomMaster;
        string roomParticipant;
        Color myColor;
        Color othersColor;
        
        public GamePlaying(string roomName, string roomMasterName)
        {
            InitializeComponent();
            Luo += PlayingLogic;
            InitPlaying(roomName, roomMasterName);
            Program.gameController.TcpClienter.ReceiveDataGram += new TcpClienter.NetEvent(DealMsg);
        }

        public GamePlaying(string roomName, string roomMasterName, string roomParticipantName)
        {
            InitializeComponent();
            Luo += PlayingLogic;
            InitPlaying(roomName, roomMasterName, roomParticipantName);
            Program.gameController.TcpClienter.ReceiveDataGram += new TcpClienter.NetEvent(DealMsg);
        }

        /// <summary>
        /// 处理接收的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DealMsg(object sender, NetEventArgs e)
        {
            MessagePackage messagePackage = new MessagePackage(e.Session.Datagram);
            switch (messagePackage.MsgType)
            {
                case MsgType.JoinRoom:
                    {
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            lb_opponentName.Text = messagePackage.Data;
                            lb_opponentColor.Text = "白色";
                            lb_opponentScore.Text = participantScore.ToString();
                            this.lb_roomStatus.Text = "准备开始"; ;
                        });
                        break;
                    }
                case MsgType.Start:
                    {
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            bt_start.Enabled = false;
                            bt_reconcile.Enabled = true;
                            bt_confess.Enabled = true;
                            this.lb_roomStatus.Text = "开始游戏";
                            start = true;
                            MessageBox.Show("开始游戏");
                        });
                        break;
                    }
                case MsgType.Wait:
                    {
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            lb_roomStatus.Text = "准备开始";
                            bt_start.Enabled = false;
                            start = false;
                        });
                        break;
                    }
                case MsgType.LuoZi:
                    {
                        string[] s = messagePackage.Data.Split(',');
                        int x = int.Parse(s[0]);
                        int y = int.Parse(s[1]);
                        Piece piece = new Piece(x, y, 3 - flag);
                        Point point = CheckBoard.ConvertCoordinatesToPoint(piece);
                        GraphicsPiece(point, othersColor);
                        if (Program.gameController.AddPiece(piece))
                        {
                            start = false;
                            if (IsDisposed || !this.IsHandleCreated) return;
                            this.Invoke((Action)delegate ()
                            {
                                if (messagePackage.SenderName.Equals(roomMaster))
                                {
                                    masterScore++;
                                    participantScore--;
                                    lb_myScore.Text = participantScore.ToString();
                                    lb_opponentScore.Text = masterScore.ToString();
                                }
                                else
                                {
                                    masterScore--;
                                    participantScore++;
                                    lb_myScore.Text = masterScore.ToString();
                                    lb_opponentScore.Text = participantScore.ToString();
                                }
                                lb_roomStatus.Text = "准备开始";
                                bt_start.Text = "再来一局";
                                bt_start.Enabled = true;
                            });
                            if (MessageBox.Show("对方玩家获胜！是否再来一局？", "再来一局", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                messagePackage = new MessagePackage(MsgType.Restart, "", "", Program.gamePlayer.Name, DateTime.Now.ToString());
                                Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                            }
                        }
                        else
                        {
                            turnFlag = true;
                        }
                        break;
                    }
                case MsgType.Msg:
                    {
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            rtb_message.AppendText(messagePackage.Data);
                        });
                        break;
                    }
                case MsgType.Quit:
                    {
                        myColor = Color.Black;
                        othersColor = Color.White;
                        masterScore = 0;
                        participantScore = 0;
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            lb_roomMasterName.Text = Program.gamePlayer.Name;
                            lb_myName.Text = Program.gamePlayer.Name;
                            roomMaster = Program.gamePlayer.Name;
                            lb_myScore.Text = "";
                            lb_opponentName.Text = "";
                            lb_roomStatus.Text = "等待玩家";
                            bt_start.Text = "开始游戏";
                            lb_myCorlor.Text = "黑色";
                            bt_start.Enabled = true;
                            bt_reconcile.Enabled = false;
                            bt_confess.Enabled = false;
                            Program.gameController.Refresh();
                            panel1.Refresh();
                            rtb_message.Text = "";
                        });
                        turnFlag = true;
                        flag = 1;
                        start = false;
                        break;
                    }
                case MsgType.Restart:
                    {
                        Program.gameController.Refresh();
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            panel1.Refresh();
                            bt_start.Enabled = false;
                            bt_reconcile.Enabled = true;
                            bt_confess.Enabled = true;
                            start = true;
                            this.lb_roomStatus.Text = "开始游戏";
                        });
                        break;
                    }
                case MsgType.Reconcile:
                    {
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            if (messagePackage.Data.Equals("agree"))
                            {
                                MessageBox.Show("对方同意和棋");
                                Program.gameController.Refresh();
                                panel1.Refresh();
                                bt_start.Enabled = true;
                                start = false;
                                this.lb_roomStatus.Text = "准备开始";
                            }
                            else if (messagePackage.Data.Equals("disagree"))
                            {
                                MessageBox.Show("对方不同意和棋，请继续游戏");
                            }
                            if (messagePackage.Data.Equals("reconcile"))
                            {
                                if (MessageBox.Show("对方玩家请求和棋？是否同意", "和棋", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                
                                        string s = "agree";
                                        messagePackage = new MessagePackage(MsgType.Reconcile, s, Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                                        Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                                        Program.gameController.Refresh();
                                        panel1.Refresh(); 
                                        bt_start.Enabled = true;
                                        start = false;
                                        this.lb_roomStatus.Text = "准备开始";
                                }
                                else
                                {
                                    string s = "disagree";
                                    messagePackage = new MessagePackage(MsgType.Reconcile, s, Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                                    Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                                }
                            }
                        });
                        break;
                    }
                case MsgType.Confess:
                    {
                        if (IsDisposed || !this.IsHandleCreated) return;
                        this.Invoke((Action)delegate ()
                        {
                            MessageBox.Show("对方认输了","胜利");
                            if (messagePackage.SenderName.Equals(roomMaster))
                            {
                                masterScore--;
                                participantScore++;
                                lb_myScore.Text = participantScore.ToString();
                                lb_opponentScore.Text = masterScore.ToString();
                            }
                            else
                            {
                                masterScore++;
                                participantScore--;
                                lb_myScore.Text = masterScore.ToString();
                                lb_opponentScore.Text = participantScore.ToString();
                            }
                            Program.gameController.Refresh();
                            panel1.Refresh();
                            bt_start.Enabled = true;
                            bt_reconcile.Enabled = false;
                            bt_confess.Enabled = false;
                            start = false;
                            this.lb_roomStatus.Text = "准备开始";
                        });
                        break;
                    }
            }

        }

        /// <summary>
        /// 玩家加入，初始化下棋
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="roomMasterName"></param>
        /// <param name="roomParticipantName"></param>
        private void InitPlaying(string roomName, string roomMasterName, string roomParticipantName)
        {

            myColor = Color.White;
            othersColor = Color.Black;
            turnFlag = false;
            flag = 2;
            roomMaster = roomMasterName;
            roomParticipant = roomParticipantName;
            lb_roomName.Text = roomName;                //房间名称
            lb_roomMasterName.Text = roomMasterName;    //房主名称
            lb_opponentColor.Text = "黑色";             //房主棋子颜色
            lb_myName.Text = roomParticipantName;       //自己名称
            lb_myScore.Text = participantScore.ToString();  //自己分数
            lb_myCorlor.Text = "白色";             //自己棋子颜色
            lb_opponentName.Text = roomMasterName;      //房主名称
            lb_opponentScore.Text = masterScore.ToString();       //房主分数
            lb_roomStatus.Text = "准备开始";
            bt_reconcile.Enabled = false;
            bt_confess.Enabled = false;
        }

        /// <summary>
        /// 初始化下棋
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="roomMasterName"></param>
        private void InitPlaying(string roomName, string roomMasterName)
        {
            myColor = Color.Black;
            othersColor = Color.White;
            turnFlag = true;
            flag = 1;
            roomMaster = roomMasterName;
            lb_roomName.Text = roomName;                  //房间名称
            lb_roomMasterName.Text = roomMasterName;      //房主名称
            lb_myName.Text = roomMasterName;              //自己名称
            lb_myCorlor.Text = "黑色";                    //棋子颜色
            lb_myScore.Text = masterScore.ToString();     //自己分数
            lb_roomStatus.Text = "等待玩家";
            bt_reconcile.Enabled = false;
            bt_confess.Enabled = false;
        }

        /// <summary>
        /// 点击开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_start_Click(object sender, EventArgs e)
        {
            if (bt_start.Text.Trim().Equals("开始游戏"))
            {
                MessagePackage messagePackage = new MessagePackage(MsgType.Start, "", Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
            }
            else if(bt_start.Text.Trim().Equals("再来一局"))
            {
                MessagePackage messagePackage = new MessagePackage(MsgType.Restart, "", Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
            }
        }

        /// <summary>
        /// 绘制棋盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            for (int i = 0; i < CheckBoard.lineNumber; i++)
            {
                graphics.DrawString((i + 1).ToString(), new Font("宋体", 12), new SolidBrush(Color.Black), CheckBoard.leftBorder - 20, CheckBoard.topBorder + i * CheckBoard.distance - 15);
                graphics.DrawLine(pen, CheckBoard.leftBorder, CheckBoard.topBorder + i * CheckBoard.distance, CheckBoard.leftBorder + CheckBoard.distance * (CheckBoard.lineNumber - 1), CheckBoard.topBorder + i * CheckBoard.distance);
            }
            for (int i = 0; i < CheckBoard.lineNumber; i++)
            {
                if (i != 0)
                {
                    graphics.DrawString((i + 1).ToString(), new Font("楷体", 12), new SolidBrush(Color.Black), CheckBoard.leftBorder + i * CheckBoard.distance - 15, CheckBoard.topBorder - 20);
                }
                graphics.DrawLine(pen, CheckBoard.leftBorder + i * CheckBoard.distance, CheckBoard.topBorder, CheckBoard.leftBorder + i * CheckBoard.distance, CheckBoard.topBorder + CheckBoard.distance * (CheckBoard.lineNumber - 1));
            }
            graphics.FillEllipse(new SolidBrush(Color.Black), 7 * CheckBoard.distance + CheckBoard.leftBorder - 5, 7 * CheckBoard.distance + CheckBoard.topBorder - 5, 10, 10);
            graphics.Dispose();
        }

        /// <summary>
        /// 棋盘点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Luo(e);
        }

        /// <summary>
        /// 下棋逻辑
        /// </summary>
        /// <param name="e"></param>
        private void PlayingLogic(MouseEventArgs e)
        {
            if (start) //开始游戏
            {
                if (turnFlag)
                {
                    Piece piece = CheckBoard.ConvertPointToCoordinates(new Point(e.X, e.Y), flag);
                    if (piece.X != -1)
                    {
                        Point point = CheckBoard.ConvertCoordinatesToPoint(piece);
                        MessagePackage messagePackage = new MessagePackage(MsgType.LuoZi, piece.X + "," + piece.Y, Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                        Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                        GraphicsPiece(point, myColor);
                        if (Program.gameController.AddPiece(piece))
                        {
                            messagePackage = new MessagePackage(MsgType.IsWin, "win", Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                            Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                            lb_roomStatus.Text = "准备开始";
                            bt_start.Text = "再来一局";
                            if (Program.gamePlayer.Name.Equals(roomMaster))
                            {
                                masterScore++;
                                participantScore--;
                                lb_myScore.Text = masterScore.ToString();
                                lb_opponentScore.Text = participantScore.ToString();
                            }
                            else
                            {
                                masterScore--;
                                participantScore++;
                                lb_myScore.Text = participantScore.ToString();
                                lb_opponentScore.Text = masterScore.ToString();
                            }
                            bt_start.Enabled = true;
                            if (MessageBox.Show("您已获胜！是否再来一局？", "再来一局", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                bt_start.Enabled = false;
                                messagePackage = new MessagePackage(MsgType.Restart, "", "", Program.gamePlayer.Name, DateTime.Now.ToString());
                                Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                                lb_roomStatus.Text = "准备开始";
                            }
                            return;
                        }
                        else
                        {
                            turnFlag = false;  //换对方下棋
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绘制棋子
        /// </summary>
        /// <param name="point"></param>
        /// <param name="myColor"></param>
        private bool GraphicsPiece(Point point, Color myColor)
        {
            Graphics graphics = this.panel1.CreateGraphics();
            if (point.X != -1 || point.Y != -1)
            {
                graphics.FillEllipse(new SolidBrush(myColor), point.X, point.Y, CheckBoard.chessPiecesSize, CheckBoard.chessPiecesSize);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 点击发送消息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_send_Click(object sender, EventArgs e)
        {
            string message = tb_message.Text.Trim();
            if(message.Length == 0 || message == null)
            {
                return;
            }
            message = Program.gamePlayer.Name + ":" + message + "  -- " + DateTime.Now.ToString().Trim() + "\n";
            MessagePackage messagePackage = new MessagePackage(MsgType.Msg, message, Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, "");
            Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
            tb_message.Text = "";
        }
        
        /// <summary>
        /// 关闭游戏窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GamePlaying_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDisposed || !this.IsHandleCreated) return;
            this.Invoke((Action)delegate
            {
                if(MessageBox.Show("退出该房间？","退出",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (IsDisposed || !this.IsHandleCreated) return;
                    this.Invoke((Action)delegate ()
                    {
                        this.FormClosing -= new FormClosingEventHandler(this.GamePlaying_FormClosing);
                        MessagePackage messagePackage = new MessagePackage(MsgType.Quit, lb_roomName.Text.Trim(), Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                        Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                        Program.gameHall.Visible = true;
                        this.Close();
                    });
                }
            });
        }

        /// <summary>
        /// 点击求和按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_reconcile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否向对方求和？", "求和", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (IsDisposed || !this.IsHandleCreated) return;
                this.Invoke((Action)delegate ()
                {
                    string s = "reconcile";
                    MessagePackage messagePackage = new MessagePackage(MsgType.Reconcile, s, Program.gamePlayer.Ip.ToString(), Program.gamePlayer.Name, DateTime.Now.ToString());
                    Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                });
            }
        }

        /// <summary>
        /// 认输按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_confess_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否向对方认输？", "认输", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (IsDisposed || !this.IsHandleCreated) return;
                this.Invoke((Action)delegate ()
                {
                    if (Program.gamePlayer.Name.Equals(roomMaster))
                    {
                        masterScore--;
                        participantScore++;
                        lb_myScore.Text = masterScore.ToString();
                        lb_opponentScore.Text = participantScore.ToString();
                    }
                    else
                    {
                        masterScore++;
                        participantScore--;
                        lb_myScore.Text = participantScore.ToString();
                        lb_opponentScore.Text = masterScore.ToString();
                    }
                    MessagePackage messagePackage = new MessagePackage(MsgType.Confess, "", "", Program.gamePlayer.Name, DateTime.Now.ToString());
                    Program.gameController.TcpClienter.Send(messagePackage.ConvertToString());
                    Program.gameController.Refresh();
                    panel1.Refresh();
                    bt_start.Enabled = true;
                    bt_reconcile.Enabled = false;
                    bt_confess.Enabled = false;
                    start = false;
                    this.lb_roomStatus.Text = "准备开始";
                });
            }
        }
    }
}

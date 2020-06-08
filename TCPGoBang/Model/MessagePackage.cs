using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGoBang.Model
{
    public enum MsgType
    {
        LuoZi = 0,    //落子
        Connect = 1,  //玩家连接
        Quit = 2,     //玩家退出房间
        IsWin = 3,    //是否胜利
        CreateRoom = 4,   //创建房间
        JoinRoom = 5,     //加入房间
        UserList = 6,     //请求|发送玩家列表
        RoomList,         //请求|发送房间列表 
        Other,            //其他
        Start,            //开始游戏
        Exit,             //断开连接
        Wait,             //等待玩家点击开始游戏
        Restart,          //重新开始
        Msg,              //聊天信息
        Reconcile,        //求和 
        Confess           //认输
    }
    public class MessagePackage
    {
        private MsgType msgType;
        private string data;
        private string senderIP = "";
        private string senderName = "";
        private string sendTime;

        public MessagePackage(string msg)
        {
            if(msg != "" || msg != null)
            {
                string[] msgs = msg.Split('|');
                msgType = (MsgType)int.Parse(msgs[0]);
                data = msgs[1];
                senderIP = msgs[2];
                senderName = msgs[3];
                sendTime = msgs[4];
            }
        }

        public MessagePackage(MsgType msg, string data, string senderIP, string senderName, string sendTime)
        {
            this.msgType = msg;
            this.data = data;
            this.senderIP = senderIP;
            this.senderName = senderName;
            this.sendTime = sendTime;
        }

        
        public string ConvertToString()
        {
            string msg = ((int)msgType).ToString() + "|" + data + "|" + senderIP + "|" + senderName + "|" + sendTime;
            return msg;
        }

        public MsgType MsgType { get => msgType; set => msgType = value; }
        public string Data { get => data; set => data = value; }
        public string SenderIP { get => senderIP; set => senderIP = value; }
        public string SenderName { get => senderName; set => senderName = value; }
        public string SendTime { get => sendTime; set => sendTime = value; }
    }
}

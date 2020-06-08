using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPGoBang
{
    public class Session : ICloneable
    {
        private Socket socket;
        private string datagram;
        private SessionID sessionId;
        private ExitType exitType;

        public Socket Socket { get => socket; set => socket = value; }
        public string Datagram { get => datagram; set => datagram = value; }
        public SessionID SessionId { get => sessionId; set => sessionId = value; }
        public ExitType ExitType1 { get => exitType; set => exitType = value; }
        
        public Session(Socket socket)
        {
            Debug.Assert(socket != null);
            this.socket = socket;
            SessionId = new SessionID((int)socket.Handle);
        }

        /// <summary>
        /// 退出类型
        /// </summary>
        public enum ExitType
        {
            NormalExit,
            ExceptionExit
        };



        public object Clone()
        {
            Session newSession = new Session(socket);
            newSession.Datagram = datagram;
            newSession.ExitType1 = exitType;
            return newSession;
        }

        public override bool Equals(object obj)
        {
            Session right = (Session)obj;
            return (int)socket.Handle == (int)right.socket.Handle;
        }

        public override int GetHashCode()
        {
            return (int)socket.Handle;
        }

        public override string ToString()
        {
            string result = string.Format("Session:{0},IP:{1}", SessionId, socket.RemoteEndPoint.ToString());
            return result;
        }

        public void Close()
        {
            Debug.Assert(socket != null);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}

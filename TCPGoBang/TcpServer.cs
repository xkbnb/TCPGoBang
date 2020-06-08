using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace TCPGoBang
{
    public class TcpServer
    {
        public delegate void NetEvent(object sender, NetEventArgs e);
        private Coder coder;
        public int DefaultBufferSize = 64 * 1024;
        public int MaxDatagramSize = 640 * 1024;     //最大数据报大小
        private DatagramResolver resolver;
        private ushort port;
        private bool isRun;
        private byte[] receiveBuffer;
        private Socket serverSocket;
        private Hashtable sessionTable;
        private ushort clientCount;

        internal DatagramResolver Resolver { get => resolver; set => resolver = value; }
        public bool IsRun { get => isRun; set => isRun = value; }
        public ushort ClientCount { get => clientCount; set => clientCount = value; }
        public Socket ServerSocket { get => serverSocket; set => serverSocket = value; }
        public Hashtable SessionTable { get => sessionTable; set => sessionTable = value; }

        #region 事件
        public event NetEvent ClientConn;
        public event NetEvent ClientClose;
        public event NetEvent RecvData;
        #endregion

        public TcpServer(ushort port)
        {
            this.port = port;
            coder = new Coder(Coder.EncodingMothord.Default);
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (isRun)
            {
                throw new Exception("服务器已在运行");
            }
            sessionTable = new Hashtable(53);
            receiveBuffer = new byte[DefaultBufferSize];
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(6);
            serverSocket.BeginAccept(new AsyncCallback(AcceptConn), serverSocket);
            isRun = true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (!isRun)
            {
                throw new Exception("连接已关闭");
            }
            if (serverSocket.Connected)
            {
                serverSocket.Shutdown(SocketShutdown.Both);
            }
            CloseAllClient();
            serverSocket.Close();
            sessionTable = null;
        }

        /// <summary>
        /// 关闭所有连接
        /// </summary>
        public void CloseAllClient()
        {
            if(sessionTable == null)
            {
                return;
            }
            foreach (Session session in sessionTable.Values)
            {
                session.Close();
            }
            sessionTable.Clear();
        }

        /// <summary>
        /// 异步接收连接
        /// </summary>
        /// <param name="ar"></param>
        protected void AcceptConn(IAsyncResult ar)
        {
            if (!isRun)
            {
                return;
            }
            Socket server = (Socket)ar.AsyncState;
            Socket client = server.EndAccept(ar);

            Session clientSession = new Session(client);
            sessionTable.Add(clientSession.SessionId, clientSession);
            clientCount++;
            clientSession.Socket.BeginReceive(receiveBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(ReceiveData), clientSession.Socket);
            ClientConn?.Invoke(this, new NetEventArgs(clientSession));
            server.BeginAccept(new AsyncCallback(AcceptConn), server);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="client"></param>
        /// <param name="exitType"></param>
        protected virtual void CloseClient(Socket client, Session.ExitType exitType)
        {
            System.Diagnostics.Debug.Assert(client != null);
            Session session = FindSession(client);
            session.ExitType1 = exitType;
            if (session != null)
            {
                CloseSession(session);
            }
            else
            {
                throw new ArgumentNullException("该客户端不存在");
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="closeSession"></param>
        private void CloseSession(Session closeSession)
        {
            Debug.Assert(closeSession != null);
            if (closeSession != null)
            {
                closeSession.Datagram = null;
                sessionTable.Remove(closeSession.SessionId);
                clientCount--;
                ClientClose?.Invoke(this, new NetEventArgs(closeSession));
                closeSession.Close();
            }
        }

        /// <summary>
        /// 查找对应的session信息
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private Session FindSession(Socket client)
        {
            SessionID id = new SessionID((int)client.Handle);
            return (Session)sessionTable[id];
        }

        /// <summary>
        /// 异步接收数据
        /// </summary>
        /// <param name="ar"></param>
        protected virtual void ReceiveData(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                int rec = client.EndReceive(ar);
                if (rec == 0)
                {
                    CloseClient(client, Session.ExitType.NormalExit);
                }
                string receiveData = coder.GetEncodingString(receiveBuffer, rec);
                if (RecvData != null)
                {
                    Session sendDataSession = FindSession(client);
                    Debug.Assert(sendDataSession != null);
                    if (resolver != null)
                    {
                        if (sendDataSession.Datagram != null && sendDataSession.Datagram.Length != 0)
                        {
                            receiveData = sendDataSession.Datagram + receiveData;
                        }
                        string[] datagrams = resolver.Resolve(ref receiveData);
                        foreach (string newDatagram in datagrams)
                        {
                            ICloneable copySession = (ICloneable)sendDataSession;
                            Session clientSession = (Session)copySession.Clone();
                            clientSession.Datagram = newDatagram;
                            RecvData(this, new NetEventArgs(clientSession));
                        }
                        sendDataSession.Datagram = receiveData;
                    }
                    else
                    {
                        ICloneable copySession = (ICloneable)sendDataSession;
                        Session clientSession = (Session)copySession.Clone();
                        clientSession.Datagram = receiveData;
                        RecvData(this, new NetEventArgs(clientSession));
                    }
                }
                if (RecvData != null)
                {
                    client.BeginReceive(receiveBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(ReceiveData), client);
                }
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10054)
                {
                    CloseClient(client, Session.ExitType.ExceptionExit);
                }
                else
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="recvDataClient"></param>
        /// <param name="datagram"></param>
        public void Send(Session recvDataClient,string datagram)
        {
            byte[] data = coder.GetEncodingBytes(datagram);
            recvDataClient.Socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendDataEnd), recvDataClient.Socket);
        }

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="iar"></param>
        protected void SendDataEnd(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            int sent = client.EndSend(iar);
        }
    }
}

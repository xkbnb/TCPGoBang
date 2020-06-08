using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPGoBang
{
    public class TcpClienter
    {
        public delegate void NetEvent(object sender, NetEventArgs e);
        private Coder coder;
        private Session session;
        private bool isConnected;
        private DatagramResolver resolver;
        public const int DefaultBufferSize = 64 * 1024;
        private byte[] receiveBuffer = new byte[DefaultBufferSize];

        internal Coder Coder { get => coder; set => coder = value; }
        public Session Session { get => session; set => session = value; }
        public bool IsConnected { get => isConnected; set => isConnected = value; }
        internal DatagramResolver Resolver { get => resolver; set => resolver = value; }

        public event NetEvent ConnectedServer;
        public event NetEvent ReceiveDataGram;
        public event NetEvent DisConnectedServer;

        public TcpClienter()
        {
            coder = new Coder(Coder.EncodingMothord.Default);
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void Connect(IPAddress ip, int port)
        {
            if (IsConnected)
            {
                Debug.Assert(session != null);
                Close();
            }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEP = new IPEndPoint(ip, port);
            socket.BeginConnect(remoteEP, new AsyncCallback(Connected), socket);
        }

        /// <summary>
        /// 结束挂起的异步连接
        /// </summary>
        /// <param name="ar"></param>
        private void Connected(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                socket.EndConnect(ar);
                session = new Session(socket);
                isConnected = true;
                if (ConnectedServer != null)
                {
                    ConnectedServer(this, new NetEventArgs(session));
                }
                session.Socket.BeginReceive(receiveBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(RecvData), socket);

            }
            catch
            {
                MessageBox.Show("无法连接服务器");
            }
        }

        /// <summary>
        /// 异步接收信息
        /// </summary>
        /// <param name="ar"></param>
        private void RecvData(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                int rec = socket.EndReceive(ar);
                if (rec == 0)
                {
                    session.ExitType1 = Session.ExitType.NormalExit;
                    if (ReceiveDataGram != null)
                    {
                        ReceiveDataGram(this, new NetEventArgs(session));
                    }
                    return;
                }
                string receiveData = coder.GetEncodingString(receiveBuffer, rec);
                if (ReceiveDataGram != null)
                {
                    if (resolver != null)
                    {
                        if (session.Datagram != null && session.Datagram.Length != 0)
                        {
                            receiveData = session.Datagram + receiveData;
                        }
                        string[] datagrams = resolver.Resolve(ref receiveData);
                        foreach (string newDatagrams in datagrams)
                        {
                            ICloneable cloneable = (ICloneable)session;
                            Session clientSession = (Session)cloneable.Clone();
                            clientSession.Datagram = newDatagrams;
                            ReceiveDataGram(this, new NetEventArgs(clientSession));
                        }
                        session.Datagram = receiveData;
                    }
                    else
                    {
                        ICloneable cloneable = (ICloneable)session;
                        Session clientSession = (Session)cloneable.Clone();
                        clientSession.Datagram = receiveData;
                        ReceiveDataGram(this, new NetEventArgs(clientSession));
                    }
                }
                session.Socket.BeginReceive(receiveBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(RecvData), session.Socket);
            }
            catch (SocketException sex)
            {
                if (10054 == sex.ErrorCode)
                {
                    session.ExitType1 = Session.ExitType.ExceptionExit;
                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(session));
                    }
                    else
                    {
                        throw (sex);
                    }
                }
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="datagram"></param>
        public void Send(string datagram)
        {
            if (datagram.Length == 0)
            {
                return;
            }
            if (!isConnected)
            {
                throw (new ApplicationException("未连接服务器,不能发送数据！"));
            }
            //获得报文的编码字节 
            byte[] data = coder.GetEncodingBytes(datagram);
            session.Socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendDataEnd), session.Socket);
        }

        /// <summary>
        /// 结束挂起的异步发送数据
        /// </summary>
        /// <param name="ar"></param>
        private void SendDataEnd(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            int sent = socket.EndSend(ar);
            Debug.Assert(sent != 0);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (!IsConnected)
            {
                return;
            }
            session.Close();
            session = null;
            isConnected = false;
        }
    }
}

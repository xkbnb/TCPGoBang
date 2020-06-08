using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGoBang
{
    public class NetEventArgs
    {
        private Session session;

        public Session Session { get => session; set => session = value; }

        public NetEventArgs(Session session)
        {
            if(session == null)
            {
                throw new ArgumentException();
            }
            this.session = session;
        }
    }
}

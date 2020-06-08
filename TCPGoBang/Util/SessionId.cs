using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGoBang
{
    public class SessionID
    {
        private int id;

        public int Id { get => id; set => id = value; }

        public SessionID(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            if(obj != null)
            {
                SessionID right = (SessionID)obj;
                return id == right.Id;
            }else if(this == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return id;
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}

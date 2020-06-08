using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGoBang
{
    class Coder
    {
        public enum EncodingMothord
        {
            Default = 0,
            Unicode,
            UTF8,
            ASCII,
        }
        private EncodingMothord encodingMothord;

        public Coder(EncodingMothord encodingMothord)
        {
            this.encodingMothord = encodingMothord;
        }

        public virtual string GetEncodingString(byte[] datas, int size)
        {
            switch (encodingMothord)
            {
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetString(datas, 0, size);
                    }
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetString(datas, 0, size); ;
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetString(datas, 0, size);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetString(datas, 0, size); ;
                    }
                default:
                    {
                        throw (new Exception("未定义的编码格式"));
                    }
            }
        }

        public virtual byte[] GetEncodingBytes(string datagram)
        {
            switch (encodingMothord)
            {
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetBytes(datagram);
                    }
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetBytes(datagram); ;
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetBytes(datagram);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetBytes(datagram);
                    }
                default:
                    {
                        throw (new Exception("未定义的编码格式"));
                    }
            }
        }
    }
}

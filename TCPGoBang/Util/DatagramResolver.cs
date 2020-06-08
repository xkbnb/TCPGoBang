using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGoBang
{
    class DatagramResolver
    {
        private string endTag;
        string EndTag
        {
            get
            {
                return endTag;
            }
        }
        protected DatagramResolver()
        {
        }
        public DatagramResolver(string endTag)
        {
            if (endTag == null || endTag == "")
            {
                throw new ArgumentNullException("结束符不能为空");
            }
            this.endTag = endTag;
        }

        public virtual string[] Resolve(ref string dataGram)
        {
            List<string> strings = new List<string>();
            int tagIndex = -1;
            while (true)
            {
                tagIndex = dataGram.IndexOf(endTag, tagIndex);
                if (tagIndex == -1)
                {
                    break;
                }
                else
                {
                    string newDatagram = dataGram.Substring(0, tagIndex + endTag.Length);
                    strings.Add(newDatagram);
                    if (tagIndex + endTag.Length >= dataGram.Length)
                    {
                        dataGram = "";
                        break;
                    }
                    dataGram = dataGram.Substring(tagIndex + endTag.Length);
                    tagIndex = 0;
                }
            }
            return strings.ToArray<string>();
        }
    }
}

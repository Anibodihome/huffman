using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Huffman.Common
{
    public class IdMaker
    {
        private IdMaker() { }
        private static IdMaker _instance = new IdMaker();

        public static string TID
        {
            get
            {
                lock (_instance)
                {
                    return DateTime.Now.ToString("yyMMddHHmmssfff");
                }
            }
        }

        public static string TIDR
        {
            get
            {
                lock (_instance)
                {
                    return DateTime.Now.ToString("fffssmmHHddMMyy");
                }
            }
        }
    }
}

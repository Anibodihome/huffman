using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman.Common.Authentication
{
    public static class Session
    {
        private static string _user = null;
        public static string User
        {
            get
            {
                return (_user ?? string.Empty).ToString();
            }
            set
            {
                _user = (value ?? string.Empty).ToString();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.Helper
{
    public class EncryptDecrypt
    {
        public string GetHex(byte[] text)
        {
            char[] hexChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7',
                                           '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
                                         };

            System.Text.StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                sb.Append(hexChars[text[i] >> 4]);
                sb.Append(hexChars[text[i] & 0x0F]);
            }

            return sb.ToString();
        }
    }
}

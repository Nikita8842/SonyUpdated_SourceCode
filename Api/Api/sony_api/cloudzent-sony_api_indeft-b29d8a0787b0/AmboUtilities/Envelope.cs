using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboUtilities
{
    public class Envelope<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public int MessageCode { get; set; }
    }
}

using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MingDeAdj
{
    public static class Sys
    {
        public static ICaptureDevice Dev;
        public static byte[] SenderMAC;
        public static byte[] ReceiverMAC;
        public static byte[] ProtocolType;
        
    }
}

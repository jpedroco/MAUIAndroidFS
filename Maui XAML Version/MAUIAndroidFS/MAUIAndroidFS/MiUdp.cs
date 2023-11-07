using ClasesGenerales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIAndroidFS
{
    public class MiUdp
    {
        public static void Envia(string mensaje)
        {
            UdpCommunication.Envia("192.168.0.171", 9394, mensaje);
        }
    }
}

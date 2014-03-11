using System;
using System.Collections.Generic;
using System.Text;

using SmartLibraryServer.Dao;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace SmartLibraryServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // membuat objek channel dan mengeset mode "listen" pada port 1234
            TcpChannel tcpChannel = new TcpChannel(1234);

            // mendaftarkan objek channel ke remote system
            ChannelServices.RegisterChannel(tcpChannel, false);

            // mendaftarkan class BukuDao, PenerbitDao
            // sebagai remote object dengan mode "SingleCall"
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(BukuDao), 
            "SmartLibraryServer.BukuDao", WellKnownObjectMode.SingleCall);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(PenerbitDao),
            "SmartLibraryServer.PenerbitDao", WellKnownObjectMode.SingleCall);

            Console.WriteLine("\nServer Running");
            Console.WriteLine("\nPress enter to exit");
            Console.ReadKey();
        }
    }
}

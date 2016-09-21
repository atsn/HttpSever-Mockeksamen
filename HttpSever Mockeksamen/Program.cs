using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpSever_Mockeksamen
{
    public class Program
    {
        static void Main(string[] args)
        {
            TcpListener Httpserver = null;

            try
            {
                Httpserver = new TcpListener(IPAddress.Any, 6789);
                Httpserver.Start();

                Console.WriteLine("Server tent");
                while (true)
                {
                    TcpClient Httpserverconnection = Httpserver.AcceptTcpClient();
                    Console.WriteLine("Client has connectet to server");
                    ClientConectionRequestAndResponseHandler connectionhandler = new ClientConectionRequestAndResponseHandler(Httpserverconnection);
                    Thread Athred = new Thread(connectionhandler.RequestReaderAndResponce);
                    Athred.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

            finally
            {
                if (Httpserver !=null) Httpserver.Stop();
            }


        }
    }
}

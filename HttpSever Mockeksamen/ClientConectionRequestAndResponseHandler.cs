using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpSever_Mockeksamen
{
    class ClientConectionRequestAndResponseHandler
    {
        private TcpClient connection;

        public ClientConectionRequestAndResponseHandler(TcpClient conection)
        {
            this.connection = conection;
        }

        internal void RequestReaderAndResponce()
        {
            Stream ConnectionStream = null;
            StreamWriter Swriter = null;
            StreamReader Sreader = null;
            try
            {

                string Reashivedmessege = "Somthing";
                string SendMessege = "Somthing else";


                while (Reashivedmessege.ToLower().Trim() != "stop")
                {

                    try
                    {
                        ConnectionStream = connection.GetStream();
                        Swriter = new StreamWriter(ConnectionStream);
                        Sreader = new StreamReader(ConnectionStream);
                        Swriter.AutoFlush = true;                      
                        Reashivedmessege = Sreader.ReadLine();

                        if (Reashivedmessege.StartsWith("GET") && Reashivedmessege.EndsWith("HTTP/1.1"))
                        {
                            string[] messeges = new string[3];
                            messeges = Reashivedmessege.Split(' ');
                            if (messeges[3] != null) Swriter.WriteLine(messeges[2]);
                            else Swriter.WriteLine("400 bad request");
                        }
                        else
                        {
                            Swriter.WriteLine("400 bad request");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Swriter.WriteLine("400 bad request");

                    }

                    finally
                    {
                        if (Swriter != null) Swriter.Close();
                        if (Sreader != null) Sreader.Close();
                        if (ConnectionStream != null) ConnectionStream.Close();
                        Reashivedmessege = "stop";
                    }


                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                Console.WriteLine(e.Message);
            }

            finally
            {
                if (ConnectionStream != null) ConnectionStream.Close();
            }




        }
    }
}

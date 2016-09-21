using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpSever_Mockeksamen
{
   public class ClientConectionRequestAndResponseHandler
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
            FileStream myFileStream = null;

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
                            if (messeges[2] != null)
                            {

                                myFileStream =
                                    new FileStream(
                                        @"C:\Users\ander\Documents\Visual Studio Apps\Små Projector\HttpSever Mockeksamen\HttpSever Mockeksamen\" +
                                        messeges[1].Trim('/'), FileMode.Open);
                                myFileStream.CopyTo(Swriter.BaseStream);

                                byte[] bytes = new byte[128];
                                Swriter.BaseStream.Write(bytes, 0, 128);
                            }
                            else Swriter.WriteLine("400 bad request");

                        }
                        else
                        {
                            Swriter.WriteLine("400 bad request");
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        Swriter.WriteLine("400 bad request");
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
                        if (myFileStream != null) myFileStream.Close();
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

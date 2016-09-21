using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpSever_Mockeksamen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HttpSever_Mockeksamen.Tests
{
    [TestClass()]
    public class ClientConectionRequestAndResponseHandlerTests
    {
        private StreamReader clientStreamReader = null;
        private StreamWriter clientStreamWriter = null;

        [TestInitialize]
        public void intialize()
        {
            TcpClient client = new TcpClient("192.168.6.85", 6789);
            Stream clientStream = client.GetStream();
            clientStreamReader = new StreamReader(clientStream);
            clientStreamWriter = new StreamWriter(clientStream);
            clientStreamWriter.AutoFlush = true;

        }
        [TestMethod()]
        public void ClientConectionRequestAndResponseHandlerTest1()
        {
            clientStreamWriter.WriteLine("GET /response.txt HTTP/1.1");
            Assert.AreEqual("Dette er hvad den skal returnere", clientStreamReader.ReadLine());
            Assert.AreEqual("den skal ogs� returnere denne linje", clientStreamReader.ReadLine());
            Assert.AreEqual("denne ogs�", clientStreamReader.ReadLine());
            Assert.AreEqual("jeg vil ogs� med", clientStreamReader.ReadLine());
            Assert.AreEqual("virker det mon ogs� med mig", clientStreamReader.ReadLine().Trim('\0'));

        }
        [TestMethod()]
        public void ClientConectionRequestAndResponseHandlerTest2()
        {
            clientStreamWriter.WriteLine("GET /response2.txt HTTP/1.1");
            Assert.AreEqual("Dette er response 2", clientStreamReader.ReadLine().Trim('\0'));


        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Nzl.Test.CommunicationServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            int port = 2000;
            string host = "127.0.0.1";

            ///创建终结点（EndPoint）
            IPAddress ip = IPAddress.Parse(host);//把ip地址字符串转换为IPAddress类型的实例
            IPEndPoint ipe = new IPEndPoint(ip, port);//用指定的端口和ip初始化IPEndPoint类的新实例

            ///创建socket并开始监听
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个socket对像，如果用udp协议，则要用SocketType.Dgram类型的套接字
            s.Bind(ipe);//绑定EndPoint对像（2000端口和ip地址）
            s.Listen(0);//开始监听
            Console.WriteLine("等待客户端连接");

            ///接受到client连接，为此连接建立新的socket，并接受信息
            Socket temp = s.Accept();//为新建连接创建新的socket
            Console.WriteLine("建立连接");
            string recvStr = "";
            byte[] recvBytes = new byte[1024];
            int bytes;
            bytes = temp.Receive(recvBytes, recvBytes.Length, 0);//从客户端接受信息
            recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);

            ///给client端返回信息
            Console.WriteLine("server get message:{0}", recvStr);//把客户端传来的信息显示出来
            string sendStr = "ok!Client send message successful!";
            byte[] bs = Encoding.ASCII.GetBytes(sendStr);
            temp.Send(bs, bs.Length, 0);//返回信息给客户端
            temp.Close();
            s.Close();
            Console.ReadLine();
        }
    }
}

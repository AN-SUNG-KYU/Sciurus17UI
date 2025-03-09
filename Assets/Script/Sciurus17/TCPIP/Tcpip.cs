using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sciurus17.Input;
using Sciurus17.ControlSystem;
using SharpDX.XInput;
using System.Collections;
/*using System.Runtime.Remoting.Messaging;*/
using System.Threading;



namespace Sciurus17.TcpIp
{

    public class Receive_TcpIP
    {
        public short RightThumbX;
        public short RightThumbY;
        public short LeftThumbX;
        public short LeftThumbY;
        public byte LeftTrigger;
        public byte RightTrigger;
        public GamepadButtonFlags Buttons; // 整数値から列挙型に変換

        private byte[] buffer = new byte[1024];
        private NetworkStream ns;
        private int bytesRead;
        private string[] parts;
        private int Buttons_int;
        private string data;

        public Receive_TcpIP(string IP)
        {
            IPAddress ad = IPAddress.Parse(IP);
            int port = 50000;
            // TCPリスナーの設定
            TcpListener server = new TcpListener(ad, port);
            Console.WriteLine("push_any_key");
            Console.ReadLine();
            server.Start();
            Console.WriteLine("Waiting for a connection...");
            TcpClient cl = server.AcceptTcpClient();
            Console.WriteLine("接続しました");
            ns = cl.GetStream();
        }

        public void GetData()
        {

            while (true)
            {
                bytesRead = ns.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    parts = data.Split(',');
                    LeftThumbX = short.Parse(parts[0]);
                    LeftThumbY = short.Parse(parts[1]);
                    RightThumbX = short.Parse(parts[2]);
                    RightThumbY = short.Parse(parts[3]);
                    LeftTrigger = byte.Parse(parts[4]);
                    RightTrigger = byte.Parse(parts[5]);
                    /*Buttons_int = int.Parse(parts[6]);*/
                    Buttons = (GamepadButtonFlags)int.Parse(parts[6]); // 整数値から列挙型に変換*/
                }
            }
        }

    }
}


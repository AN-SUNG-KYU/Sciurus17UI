using System;
using System.IO;
using System.IO.Ports;

namespace Sciurus17.Port
{
    public static class PortHandler
    {
        public static SerialPort port;

        private static readonly int BaudRate = 3000000;
        private static readonly Parity Parity = Parity.None;
        private static readonly int DataBits = 8;
        private static readonly StopBits StopBits = StopBits.One;

        public static void SetPortName(string name)
        {
            try
            {
                port = new SerialPort(name, BaudRate, Parity, DataBits, StopBits);
            }
            catch (IOException)
            {
                Console.WriteLine("[Exception]:The specified port could not be found or opened.");
            }
        }
        public static void PortOpen()
        {
            try
            {
                port.Open();
                Console.WriteLine("Succeeded to open the port(" + port.PortName + ")");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("[Exception]:The file type of the port is not supported.");
            }
            catch (IOException)
            {
                Console.WriteLine("[Exception]:The port is in an invalid state.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("[Exception]:The specified port on the current instance of the SerialPort is already open.");
            }
        }
        public static void PortClose()
        {
            try
            {
                port.Close();
                Console.WriteLine("Close the port(" + port.PortName + ")");
            }
            catch (IOException)
            {
                Console.WriteLine("[Exception]:The port is in an invalid state.");
            }
        }
        public static void Write(byte[] command)
        {
            try
            {
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                port.Write(command, 0, command.Length);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("[Exception]:The specified port is not open.");
            }
            catch (TimeoutException)
            {
                Console.WriteLine("[Exception]:The operation did not complete before the time-out period ended.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static byte[] Read(int len)
        {
            while (port.BytesToRead < len) { }
            var data = new byte[len];
            try
            {
                port.Read(data, 0, len);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("[Exception]:The specified port is not open.");
            }
            catch (TimeoutException)
            {
                Console.WriteLine("[Exception]:No bytes were available to read.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public static byte ReadByte() => (byte)port.ReadByte();

        public static SerialPort GetSerialPort() => port;

    }
}

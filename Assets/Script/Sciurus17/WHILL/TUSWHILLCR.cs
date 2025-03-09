using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Security.AccessControl;
using WHILL_D435i;
using CR.WHILL.ControlSystem;

namespace TUSWHILLCR
{
    class TUSWHILLCR
    {
        public byte[] readbuf;
        public byte[] tempreadbuf;
        public sbyte forward;
        public sbyte turn;
        public sbyte joystick_forward;
        public sbyte joystick_turn;
        public int printinterval;
        public int controlinterval;

        ///20240801追加
        public double assist_forward;

        public SerialPort crport;
        public Thread readmessagethread;
        public Thread printmessagethread;
        public Thread controlthread;

        WHILLCRMessage crMSG;

        Stopwatch sw1;
        Stopwatch read_sw;
        Stopwatch print_sw;
        Stopwatch control_sw;

        bool crport_open_flag = false;
        bool crpoweron_flag = false;


        public TUSWHILLCR()
        {
            readbuf = new byte[34];
            tempreadbuf = new byte[34];
            sw1 = new Stopwatch();
            read_sw = new Stopwatch();
            print_sw = new Stopwatch();
            control_sw = new Stopwatch();
 
            crMSG = new WHILLCRMessage();

            printinterval = 100;
        }

        public int initialize(String portname="COM4", int sendinterval=100)
        {
            open(portname);
            poweron();
            startsendingdata1(sendinterval);
            return 1;
        }

        public int open(String portname="COM4")
        {
            if (crport_open_flag) return -1;
            crport=new SerialPort(portname,38400, Parity.None, 8, StopBits.Two);
            crport_open_flag = true;
            try
            {
                crport.Open();
                crport.DtrEnable = true;
                crport.RtsEnable = true;
                Console.WriteLine("WHILL CR Port Open Success");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 0: COM Port Open Fail");
                return -1;
            }



            return 1;
        }

        public int poweron()
        {
            if (crpoweron_flag) return -1;
            try
            {
                crport.DiscardInBuffer();
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 0a: Serial Port Inbuffer Discard Fail");
                return -1;
            }
            crMSG.setCommandPower();
            try
            {
                Console.WriteLine(BitConverter.ToString(crMSG.sendMessage));
                crport.Write(crMSG.sendMessage, 0, crMSG.sendMessage_size);
                Console.WriteLine("Sending Power On Command");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 1: COM Write Fail");
                Console.ReadKey();
                return -1;
            }
            sw1.Start();

            while (crport.BytesToRead < 20)
            {
                if (sw1.ElapsedMilliseconds > 10)
                {
                    crport.Write(crMSG.sendMessage, 0, crMSG.sendMessage_size);
/*                    Console.WriteLine("Sending Power On Command");
*/                    Console.WriteLine(sw1.ElapsedMilliseconds);
                    sw1.Stop();
                    sw1.Restart();
                }
            }

            try
            {
                crport.Read(readbuf, 0, 4);
                Console.WriteLine("WHILL CR Power ON");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 1a: COM Read Fail");
                return -1;
            }
            Console.WriteLine(BitConverter.ToString(readbuf,0,4));

            crMSG.setCommandStopSendingData();
            crpoweron_flag = true;

            return 1;
        }

        public int startsendingdata1(int interval=10) 
        {

            crMSG.setCommandStartSendingData(1, interval, 0);
            Console.WriteLine(BitConverter.ToString(crMSG.sendMessage));
            try
            {
                Console.WriteLine(BitConverter.ToString(crMSG.sendMessage));
                crport.Write(crMSG.sendMessage, 0, crMSG.sendMessage_size);
                Console.WriteLine("Sending Start Sending Data Command");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 1: COM Write Fail");
                Console.ReadKey();
                return -1;
            }
            read_sw.Start();
            readmessagethread = new Thread(new ThreadStart(readingmessage));

            readmessagethread.Start();


            return 1;
        }

        public void readingmessage()
        {
            while(CRControlSystem.end)
            {  
                if (crport.BytesToRead > 32)
                {
                    try
                    {
                        crport.Read(tempreadbuf, 0, 33);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Exception 1a: COM Read Fail");
                    }
                    if (tempreadbuf[0] == 0xaf)
                    {
                        for (int i =0; i<34; i++)
                        {
                            readbuf[i] = tempreadbuf[i];
                        }
                        joystick_forward = (sbyte)readbuf[15];
                        joystick_turn = (sbyte)readbuf[16];
                    }
                    crport.DiscardInBuffer();
                }

                Thread.Sleep(10);
            }
        }

        public void printingmessage()
        {
            while(CRControlSystem.end)
            {
                //Console.Write(print_sw.ElapsedMilliseconds);
                //Console.Write(": Forward=");
                //Console.Write((sbyte)readbuf[15]);
                //Console.Write(", Turn=");
                //Console.WriteLine((sbyte)readbuf[16]);
                //Console.WriteLine("Z:" + realsense.Ze[0] + " u_f " + CBF.u_f + " ua_f " + CBF.ua_f);
                //Console.WriteLine("Z:" + realsense.e + " dis " + CBF.R_edge + " I_f " + CBF.I_f + " J_f " + CBF.J_f + " u_f " + CBF.u_f + " ua_f " + CBF.ua_f);
                //Console.WriteLine("real:" + realsense.r1 + " dis " + CBF.R_f1 + " uh_f " + CBF.uh_f + " u_f " + CBF.u_f + " ua_f " + CBF.ua_f);
                //Console.WriteLine("x " + realsense.x1[realsense.i1] + " dis " + CBF.R_f1 + " I_f " + CBF.I_f + " J_f " + CBF.J_f + " u_f " + CBF.u_f + " ua_f " + CBF.ua_f);
                //Console.WriteLine("index " + realsense.i1);
                //Console.WriteLine(" X[0] " + CBF.X[0] + " Y[0] " + CBF.Y[0] + " X[1] " + CBF.X[1] + " Y[1] " + CBF.Y[1] + " X[2] " + CBF.X[2] + " Y[2] " + CBF.Y[2] ) ;
                //Console.WriteLine(" X[3] " + CBF.X[3] + " Y[3] " + CBF.Y[3] + " X[4] " + CBF.X[4] + " Y[4] " + CBF.Y[4] + " n " + CBF.n);
                //time = Convert.ToString(realsense.sw.ElapsedMilliseconds);
                //Program.writer1.WriteLine(Program.time + "," + CBF.uh_f + "," + CBF.ua_f + "," + CBF.u_f + "," + realsense.e + "," + CBF.R_edge + "," + realsense.e2);
                //Program.writer2.WriteLine(time + "," + CBF.a + "," + CBF.b + "," + CBF.X[0] + "," + CBF.X[1] + "," + CBF.X[2] + "," + CBF.X[3] + "," + CBF.X[4] + "," + CBF.Y[0] + "," + CBF.Y[1] + "," + CBF.Y[2] + "," + CBF.Y[3] + "," + CBF.Y[4]);
                Thread.Sleep(printinterval);
            }
        }

        public int printmessage(int interval=100)
        {
            printinterval = interval;
            print_sw.Start();
            printmessagethread = new Thread(new ThreadStart(printingmessage));
            printmessagethread.Start();

            return 1;
        }

        public void control_main()
        {
            while(CRControlSystem.end)
            {
                if(control_sw.ElapsedMilliseconds>controlinterval)
                {
                    crMSG.setCommandSetJoystick(0, forward, turn);
                    crport.Write(crMSG.sendMessage, 0, crMSG.sendMessage_size);
                    control_sw.Restart();
                }

                Thread.Sleep(0);
            }
        }

        public int startcontrol(int t = 20)
        {
            controlinterval = t;
            control_sw.Start();
            controlthread = new Thread(new ThreadStart(control_main));
            controlthread.Start();

            return 1;
        }

    }
}

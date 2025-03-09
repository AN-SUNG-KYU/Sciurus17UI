using System;
using System.Threading;
using Sciurus17.SciurusSystem;
using Sciurus17.Input;
using Sciurus17.TcpIp;
using CR.WHILL.ControlSystem;
using static Sciurus17.Time.Time_management;
using System.Net;

namespace Sciurus17.ControlSystem
{
    public class Controlsystem : IControlSystem
    {
        public IController Padinput { get; set; }
        public ISciurus Sciurus { get; set; }
        public ICRControlSystem CR { get; set; }

        double robolooptime_t0 = 0.0;
        double robolooptime_t = 0.0;

        public void SetControlsystem(byte[] id, byte[] mode, string sciurus_Portname, string whill_Portname, string ip_address, bool Sciurus_On, bool Whill_On, bool padonline)
        {
            //コントローラーのセッティング
            if (padonline) SetControllerOnline(ip_address);
            else Padinput = new ControllerOff();

            Thread Controller_core = new Thread(new ThreadStart(Controller_loop)); //コントローラーの送受信スレッド
            Controller_core.Start();
            Console.WriteLine("Setcontroller_successful");


            //車両のセッティング
            if (Whill_On)
            {
                CR = new CRControlSystem();
                CR.SetPort(whill_Portname);

                Thread whill_core = new Thread(Whill_loop); //電動車いすのスレッド
                whill_core.Start();
                Console.WriteLine("SetWhill_successful");
            }

            //Sciurusのセッティング
            if (Sciurus_On)
            {
                Sciurus = new Sciurus();
                Sciurus.SetSciurus(id, mode, sciurus_Portname);

                Thread Robot_core = new Thread(new ThreadStart(Robot_loop)); //ロボットの送受信スレッド
                Robot_core.Start();
                Console.WriteLine("SetSciurus_successful");

            }
        }

        private void SetControllerOnline(string ip_address)///オンラインのコントローラー
        {
            Receive_TcpIP op = new Receive_TcpIP(ip_address);
            ///TCPIPスレッド
            Thread Tcpip_thread = new Thread(op.GetData);
            Tcpip_thread.Start();
            Padinput = new ControllerOnline(op);
        }

        /// <summary>
        /// idの角度，速度，電流値をコンソールに出力
        /// </summary>
        /// <param name="id"></param>
        public void Write_Parameter(byte id)
        {
            if((id >= 2) && (id <= 20))
            {
                Console.WriteLine("リンク:{0}_角度:{1}_速度:{2}_電流:{3}", id, Sciurus.GetstatePosition(id), Sciurus.GetstateVelocity(id), Sciurus.GetstateCurrent(id));
            }
            else Console.WriteLine("No number");
            
        }

        /// <summary>
        /// Sciurusの通信ループ時間[ms]をコンソールに出力
        /// </summary>
        public void Write_time_Robotloop()
        {
            Console.WriteLine("sciurusループ{0}ミリ秒", robolooptime_t); ///ミリ秒
        }


        private void Robot_loop()
        {
            while (Sciurus.Runnig == 0 && Padinput.Connect)
            {
                robolooptime_t = (Elapsedtime() - robolooptime_t0) * 1000.0; ///ミリ秒
                robolooptime_t0 = Elapsedtime();
                Sciurus.Update();
            }
            Sciurus.FinishSciurus();
            Console.WriteLine("Finish_Sciurus");
        }

        private void Controller_loop()
        {
            while (Padinput.Connect)
            {
                Padinput.Update();
            }
            Console.WriteLine("Finish_Controller");
        }

        private void Whill_loop()
        {
            while (Padinput.Connect)
            {
                CR.Run();
            }
            Console.WriteLine("Finish_WHILL");
        }

    }

}

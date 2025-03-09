using System;
using System.Threading;
using Sciurus17.SciurusSystem;
using Sciurus17.Input;
using Sciurus17.RobotCBF;
using Sciurus17.Linercontrol;
using static Sciurus17.Time.Time_management;
using static Sciurus17.Dynamixel.Converter.SimpleConvert;
using CR.WHILL.ControlSystem;
using System.Security.Cryptography;

namespace Sciurus17.ControlSystem
{
    public class Usercontrol
    {
        public IControlSystem control;
        public IController Pad { get; set; } ///Icontroler参照
        public ISciurus Robo { get; set; }   ///ISciurus参照
        public ICRControlSystem CR { get; set; } ///ICRControlSystem参照

        byte[] Id;
        byte[] Mode;
        string Sciurus_Portname;
        string Whill_Portname;
        string ip_address;

        bool PadOnline = false;
        bool Robo_ON = false;
        bool CR_ON = false;
            
        public Usercontrol() 
        {
            ///動かしたいモーターのmodeを指定する。Idの配列とModeの配列の同じインデックスにあるid,modeが同期する
            Id = new byte[19] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            Mode = new byte[19] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            
            for (int i = 0; i < Mode.Length; i++) 
            {
                Mode[i] = 1;
            }


            Sciurus_Portname = "COM5";
            Whill_Portname = "COM4";
            ip_address = "192.168.2.22";
            PadOnline = false;
            Robo_ON = true;
            CR_ON = true;

            control = new Controlsystem();
            control.SetControlsystem(Id, Mode, Sciurus_Portname, Whill_Portname, ip_address, Robo_ON, CR_ON, PadOnline);

            Pad = control.Padinput;
            Robo = control.Sciurus;
            CR = control.CR;
        }

        public void Update()
        {
            Action action = new Action(this);

            Thread.Sleep(1000);
            double t0 = 0.0, t = 0.0;
            t0 = Elapsedtime();

            int mode = 0;

            while (Robo.Runnig == 0 && Pad.Connect) ///Sciurusのループと入力のループが正常の場合動く
            {
                t = Elapsedtime() - t0;
                Console.WriteLine("{0}秒経過", t);
                Control_SleepTime(8.0);

                if (t > 1.0 && t < 3.0) action.Initial_position();
                else if (t >= 4.0 && t < 5.0) action.Allvelo_zero();
                else if (t >= 5.0)
                {
                    if (mode == 0)
                    {
                        action.personal_Link();
                    }
                    else if (mode == 1)
                    {
                        action.Initial_position();
                        action.Whillmove();
                    }
                    
                }

                if(Pad.ButtonX) mode = 0;
                else if (Pad.ButtonY) mode = 1;
                


            }
            Console.WriteLine("制御終了しました");
            Console.WriteLine("finish_Controlsystem");
        }




    }
}

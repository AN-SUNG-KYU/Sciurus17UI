using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.Time
{
    public static class Globalstopwatch
    {
        public static readonly Stopwatch sw = Stopwatch.StartNew();
    }
    public static class Time_management
    {
        ///時間管理
        static long freq = Stopwatch.Frequency;

        static double t0Mili = 0.0;
        static double tMili;
        static double tseco = 0.0;

        static double robo_t0Micr = 0.0;
        static double robo_tMicr;
        static double robo_tseco = 0.0;

        static double control_t0Mili = 0.0;
        static double control_tMili;
        static double control_tseco = 0.0;


        public static double Elapsedtime() ///秒
        {
            return (double)Globalstopwatch.sw.ElapsedTicks / freq;
        }

        public static void Control_SleepTime(double MiliSleeptime)　///ミリ秒 制御ループのみで使用
        {
            control_t0Mili = (double)Globalstopwatch.sw.ElapsedTicks * 1000 / freq;

            while ((control_tMili = (double)Globalstopwatch.sw.ElapsedTicks * 1000 / freq - control_t0Mili) < MiliSleeptime)
            {
                Thread.Yield();
            }
            control_tMili = 0;

        }
        public static void Sciurus_SleepTime(double MicrSleeptime) ///マイクロ秒　Sciurusループのみで使用
        {
            robo_t0Micr = (double)Globalstopwatch.sw.ElapsedTicks * 1000 * 1000 / freq;


            while ((robo_tMicr = (double)Globalstopwatch.sw.ElapsedTicks * 1000 * 1000 / freq - robo_t0Micr) < MicrSleeptime)
            {
                Thread.Yield();
            }
            robo_tMicr = 0;
        }


    }
}

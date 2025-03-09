using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sciurus17.Dynamixel.Converter.SimpleConvert;
using static Sciurus17.Time.Time_management;


namespace Sciurus17.Linercontrol
{
    public class PIDcontrol
    {
        double t_ = 0.0;
        double dt;
        double u;
        double Presentvelo_f = 0.0;
        double umax = 0.02;
        double umin = -0.04;
        double e, e_, ei, ed= 0.0; ///e:目標値と現在値の偏差, e_:一つ前の偏差の値, ei:偏差の積分値
        double u_before = 0.0;

        /// <summary>
        /// 引数に制御周期[ms]を入れる
        /// </summary>
        /// <param name="t_"></param>
        public PIDcontrol(double t_)
        {
            dt = t_* 0.001;
        }

        /// <summary>
        /// 位置のP制御, Kpは必ず+
        /// </summary>
        /// <param name="Kp"></param>
        /// <param name="Presentdeg"></param>
        /// <param name="Goaldeg"></param>
        /// <returns></returns>
        public double Pcontrol_pos(double Kp, double Presentdeg, double Goaldeg) ///普通のp制御
        {
            return Kp * (ConvertDegIntoRad(Goaldeg) - ConvertDegIntoRad(Presentdeg));
        }

        public double PIcontrol_pos(double Kp, double Ki, double Presentdeg, double Goaldeg)
        {

            e = ConvertDegIntoRad(Goaldeg) - ConvertDegIntoRad(Presentdeg);

            ei += e * dt; ///長方形則
            u = Kp * e + Ki * ei;

            return u;
        }

        public double PIDcontrol_pos(double Kp, double Ki, double Kd, double Presentdeg, double Goaldeg)
        {

            e = ConvertDegIntoRad(Goaldeg) - ConvertDegIntoRad(Presentdeg);
            ed = e - e_ / dt;
            ei += e * dt; ///長方形則
            u = Kp * e + Ki * ei - Kd * ed;

            e_ = e; ///今の偏差の保存
            return u;
        }


        public double PcontrolAbs_pos(double Kp, double Presentdeg, double Goaldeg)///累乗の形のP制御
        {
            return Kp * Math.Pow(Math.Abs(ConvertDegIntoRad(Goaldeg) - ConvertDegIntoRad(Presentdeg)), 0.6) * Math.Sign(ConvertDegIntoRad(Goaldeg) - ConvertDegIntoRad(Presentdeg)); //有限時間制定　
        }

        public double PIcontrol_velo(double Kp, double Ki, double Presentvelo, double Goalvelo)
        {
            
            e = Goalvelo - Presentvelo;
/*            Presentvelo_f = Presentvelo_f -100.0*(Presentvelo_f- Presentvelo)*dt;
*/
/*            ei += e * dt; ///長方形則
*/            /*            if (Goalvelo > 0) u = 0.02;
                        else if (Goalvelo < 0) u = -0.02;
            */
/*            e = Goalvelo - Presentvelo_f;
*/          ei += e * dt; ///長方形則
            u = Kp * e + Ki * ei;
/*            Console.WriteLine(Presentvelo_f);
*/            //u = Kp * Goalvelo;
            /*            u = Kp * Math.Pow(Math.Abs(e), 0.6) * Math.Sign(e);
            */    /*        if (u > umax) u = umax;
                        else if (u < umin) u = umin;*/

            return u;
        }

        public double PIDcontrol_velo( double Kp, double Ki, double Kd, double Presentvelo, double Goalvelo)
        {

            e = Goalvelo - Presentvelo;
/*            dt = Elapsedtime() - t_; ///前の周期と今の周期の経過時間の差　微小時間dt
            t_ = Elapsedtime();      ///今の周期の経過時間の保存*/
/*            Console.WriteLine(dt * 1000);
*/            ed = e - e_ / dt;
            ei += e * dt; ///長方形則
            u = Kp * e + Ki * ei - Kd * ed;

            e_ = e; ///今の偏差の保存
            return u;
        }

        public double PIDcontrol_velo_demo(double Kp, double Ki, double Kd, double Presentcurrnt, double Presentvelo, double Goalvelo)
        {

            e = Goalvelo - Presentvelo;
            ei += e * dt; ///長方形則
            ed = Presentcurrnt;
            u = Kp * e + Ki * ei - Kd * ed;

            u_before = u;
            e_ = e; ///今の偏差の保存
            return u;
        }

    }
}

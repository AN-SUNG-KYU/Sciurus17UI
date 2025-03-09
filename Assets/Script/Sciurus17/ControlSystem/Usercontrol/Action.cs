using CR.WHILL.ControlSystem;
using Sciurus17.Input;
using Sciurus17.Linercontrol;
using Sciurus17.RobotCBF;
using Sciurus17.SciurusSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Sciurus17.Dynamixel.Converter.SimpleConvert;


namespace Sciurus17.ControlSystem
{
    public class Action
    {
        Usercontrol user;
        private IControlSystem control;
        private IController Pad { get; set; } ///Icontroler参照
        private ISciurus Robo { get; set; }   ///ISciurus参照
        private ICRControlSystem CR { get; set; } ///ICRControlSystem参照

        public Action(Usercontrol usercontrol)
        {
            user = usercontrol;
            control = user.control;
            Pad = user.Pad;
            Robo = user.Robo;
            CR = user.CR;
        }
        public double r_x2L = 0.0, r_y2L = 0.17; ///初期の手先位置
        public double pos_x2L, pos_y2L;

        public double r_x3L = 0.17, r_y3L = 0.27, r_z3L = 0.0; ///初期の手先位置
        public double pos_x3L = 0.0, pos_y3L = 0.0, pos_z3L = 0;

        public double dot_x = 0.0, dot_y = 0.0, dot_z = 0.0;
        public double v_2, v_3, v_4, v_5, v_6, v_7, v_8, v_9, v_10, v_11, v_12, v_13, v_14, v_15, v_16, v_17, v_18, v19, v_20;
        static double looptime = 8.0;
        PIDcontrol pidL2 = new PIDcontrol(looptime);
        PIDcontrol pidL3 = new PIDcontrol(looptime);
        PIDcontrol pidL4 = new PIDcontrol(looptime);
        PIDcontrol pidL5 = new PIDcontrol(looptime);
        PIDcontrol pidL6 = new PIDcontrol(looptime);
        PIDcontrol pidL7 = new PIDcontrol(looptime);
        PIDcontrol pidL8 = new PIDcontrol(looptime);
        PIDcontrol pidL9 = new PIDcontrol(looptime);
        PIDcontrol pidL10 = new PIDcontrol(looptime);
        PIDcontrol pidL11 = new PIDcontrol(looptime);
        PIDcontrol pidL12 = new PIDcontrol(looptime);
        PIDcontrol pidL13 = new PIDcontrol(looptime);
        PIDcontrol pidL14 = new PIDcontrol(looptime);
        PIDcontrol pidL15 = new PIDcontrol(looptime);
        PIDcontrol pidL16 = new PIDcontrol(looptime);
        PIDcontrol pidL17 = new PIDcontrol(looptime);
        PIDcontrol pidL18 = new PIDcontrol(looptime);
        PIDcontrol pidL19 = new PIDcontrol(looptime);
        PIDcontrol pidL20 = new PIDcontrol(looptime);

        public void Whillmove()
        {

            CR.whill_input[0] = (sbyte)(Pad.LeftThumbY * 20);
            CR.whill_input[1] = (sbyte)(Pad.LeftThumbX * 10);
        }

        public void Initial_position()
        {
            r_x2L = 0.0;
            r_y2L = 0.17; ///初期の手先位置

            r_x3L = 0.17;
            r_y3L = 0.27; 
            r_z3L = 0.0; ///初期の手先位置

            Robo.SetGoalVelocity(2, pidL2.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(2), 0));
            Robo.SetGoalVelocity(3, pidL3.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(3), -90));
            Robo.SetGoalVelocity(4, pidL4.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(4), 0));
            Robo.SetGoalVelocity(5, pidL5.PIDcontrol_pos(1.0, 0.0, 0.0, Robo.GetstatePosition(5), 156.6));
            Robo.SetGoalVelocity(6, pidL6.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(6), 0));
            Robo.SetGoalVelocity(7, pidL7.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(7), -90));
            Robo.SetGoalVelocity(8, pidL8.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(8), 0));
            Robo.SetGoalVelocity(9, pidL9.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(9), 0));
/*            Robo.SetGoalVelocity(10, pidL10.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(10), 0));
            Robo.SetGoalVelocity(11, pidL11.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(11), 0));
            Robo.SetGoalVelocity(12, pidL12.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(12), 0));
            Robo.SetGoalVelocity(13, pidL13.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(13), -156));
            Robo.SetGoalVelocity(14, pidL14.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(14), 0));
            Robo.SetGoalVelocity(15, pidL15.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(15), 0));
            Robo.SetGoalVelocity(16, pidL16.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(16), 0));
            Robo.SetGoalVelocity(17, pidL17.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(17), 0));*/
            Robo.SetGoalVelocity(18, pidL18.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(18), 0));
/*            Robo.SetGoalVelocity(19, pidL19.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(19), 0));
            Robo.SetGoalVelocity(20, pidL20.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(20), 0));*/
        }

        public void Allvelo_zero()
        {
            Robo.SetGoalVelocity(2, 0);
            Robo.SetGoalVelocity(3, 0);
            Robo.SetGoalVelocity(4, 0);
            Robo.SetGoalVelocity(5, 0);
            Robo.SetGoalVelocity(6, 0);
            Robo.SetGoalVelocity(7, 0);
            Robo.SetGoalVelocity(8, 0);
            Robo.SetGoalVelocity(9, 0);
            Robo.SetGoalVelocity(10, 0);
            Robo.SetGoalVelocity(11, 0);
            Robo.SetGoalVelocity(12, 0);
            Robo.SetGoalVelocity(13, 0);
            Robo.SetGoalVelocity(14, 0);
            Robo.SetGoalVelocity(15, 0);
            Robo.SetGoalVelocity(16, 0);
            Robo.SetGoalVelocity(17, 0);
            Robo.SetGoalVelocity(18, 0);
            Robo.SetGoalVelocity(19, 0);
            Robo.SetGoalVelocity(20, 0);
        }
        public void Head_PIDcontrol()
        {
            Robo.SetGoalVelocity(19, pidL19.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(19), -1 * Robo.GetstatePosition(18)) + -1 * v_18);
            Robo.SetGoalVelocity(20, pidL20.PIDcontrol_pos(1.5, 0.5, 0.0, Robo.GetstatePosition(20), 0));
        }


        byte personal_number = 0;
        byte linknumber = 0;
        byte _linknumber = 2;
        bool Dpadflag = false;
        Robot_CBF robotcbf = new Robot_CBF();
        public void personal_Link()
        {
            if (Pad.DPadUp && Dpadflag)
            {
                personal_number++;
                Dpadflag = false;
            }
            else if (Pad.DPadDown && Dpadflag)
            {
                personal_number--;
                Dpadflag = false;
            }
            else  if (!Pad.DPadUp && !Pad.DPadDown)Dpadflag = true;
            
            linknumber = (byte)(personal_number % 8 + 2);

            Console.WriteLine(linknumber);
            if (linknumber == _linknumber)
            {
                Robo.SetGoalVelocity(linknumber, Pad.RightThumbY + robotcbf.protection_degree_CBF(ConvertDegIntoRad(Robo.GetstatePosition(linknumber)), Pad.RightThumbY, linknumber));
            }
            else
            {
                Robo.SetGoalVelocity(_linknumber, 0.0 + robotcbf.protection_degree_CBF(ConvertDegIntoRad(Robo.GetstatePosition(_linknumber)), 0.0, _linknumber));
            }
            _linknumber = linknumber;

        }

        public void Strict_3link_mode()
        {
            if (-0.05 >= r_x3L && Pad.RightThumbX * 0.001 < 0) dot_x = 0;
            else if (0.16 <= r_x3L && Pad.RightThumbX * 0.001 > 0) dot_x = 0;
            else dot_x = Pad.RightThumbX * 0.001;

            if (0.25 >= r_y3L && Pad.RightThumbY * 0.001 < 0) dot_y = 0;
            else if (0.3 <= r_y3L && Pad.RightThumbY * 0.001 > 0) dot_y = 0;
            else dot_y = Pad.RightThumbY * 0.001;

            if (-0.25 >= r_z3L && Pad.LeftTrigger * -0.001 < 0) dot_z = 0;
            else if (0.35 <= r_z3L && Pad.RightTrigger * 0.001 > 0) dot_z = 0;
            else dot_z = (Pad.RightTrigger * 0.001) + (Pad.LeftTrigger * -0.001);

            Strictlinear.Strictlinear_3Link(ConvertDegIntoRad(Robo.GetstatePosition(2)), ConvertDegIntoRad(Robo.GetstatePosition(5)), ConvertDegIntoRad(Robo.GetstatePosition(18)), ref r_x3L, ref r_y3L, ref r_z3L, ref dot_x, ref dot_y, ref dot_z, ref v_2, ref v_5, ref v_18);
/*            Strictlinear.Strictlinear_3Link_demo(ConvertDegIntoRad(Robo.GetstatePosition(2)), ConvertDegIntoRad(Robo.GetstatePosition(5)), ConvertDegIntoRad(Robo.GetstatePosition(18)), ref r_x3L, ref r_y3L, ref r_z3L, ref dot_x, ref dot_y, ref dot_z, ref v_2, ref v_5, ref v_18);
*/
            /*            Console.WriteLine(r_x3L + " " + r_y3L + " " + r_z3L);
                        Console.WriteLine(v_2 + " " + v_5 + " " + v_18);*/
            Robo.SetGoalVelocity(2, v_2);
            Robo.SetGoalVelocity(5, v_5);
            Robo.SetGoalVelocity(18, v_18);
        }

        public void Strict_2link_mode()
        {

            if (-0.30 >= r_x2L && Pad.RightThumbX * 0.001 < 0) dot_x = 0;
            else if (0.30 <= r_x2L && Pad.RightThumbX * 0.001 > 0) dot_x = 0;
            else dot_x = Pad.RightThumbX * 0.001;

            if (0.15 >= r_y2L && Pad.RightThumbY * 0.001 < 0) dot_y = 0;
            else if (0.30 <= r_y2L && Pad.RightThumbY * 0.001 > 0) dot_y = 0;
            else dot_y = Pad.RightThumbY * 0.001;

            Strictlinear.Strictlinear_2Link(ConvertDegIntoRad(Robo.GetstatePosition(2)), ConvertDegIntoRad(Robo.GetstatePosition(5)), ref r_x2L, ref r_y2L, dot_x, dot_y, ref v_2, ref v_5);


            Robo.SetGoalVelocity(2, v_2);
            Robo.SetGoalVelocity(5, v_5);
            Robo.SetGoalVelocity(7, pidL7.PIDcontrol_pos(1.5, 0.0, 0.0, Robo.GetstatePosition(7), 90 - Robo.GetstatePosition(2) - Robo.GetstatePosition(5)) - v_2 - v_5);
        }
        public void Jacobian_2link_mode()
        {

            if (0.15 >= pos_x2L && Pad.RightThumbX * 0.01 < 0) dot_x = 0;
            else if (0.30 <= pos_x2L && Pad.RightThumbX * 0.01 > 0) dot_x = 0;
            else dot_x = Pad.RightThumbX * 0.5;

            if (-0.3 >= pos_y2L && Pad.RightThumbY * 0.01 < 0) dot_y = 0;
            else if (0.3 <= pos_y2L && Pad.RightThumbY * 0.01 > 0) dot_y = 0;
            else dot_y = Pad.RightThumbY * 0.5;
            
            Console.WriteLine(dot_x + " " + dot_y);
            Console.WriteLine(v_2 + " " + v_5);
            Jacobi.Jacobian_2Link(ConvertDegIntoRad(Robo.GetstatePosition(2)), ConvertDegIntoRad(Robo.GetstatePosition(5)), dot_x, dot_y, ref pos_x2L, ref pos_y2L, ref v_2, ref v_5);

            
            Robo.SetGoalVelocity(2, v_2);
            Robo.SetGoalVelocity(5, v_5);
        }


    }
}

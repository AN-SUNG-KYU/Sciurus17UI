using Sciurus17.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.ControlSystem
{
    public static class Jacobi
    {
        private static double l1 = 0.25, l2 = 0.25, l3 = 0.17;
        private static double x_pos, y_pos, z_pos, J_norumu, gamma_x, gamma_y, gamma_z, K_v2 = 2.5, K_v3 = 2.5, a, b, c, d;
        private static double a11, a12, a13, a21, a22, a23, a31, a32, a33;

        public static void Jacobian_2Link(double state_L2, double state_L5, double dot_x, double dot_y, ref double pos_x2L, ref double pos_y2L, ref double u_L2, ref double u_L5)
        {
            ///2×2行列
            a = l1 * Math.Cos(state_L2) + l2 * Math.Cos(state_L2 + state_L5);
            b = l1 * Math.Cos(state_L2 + state_L5);
            c = -l1 * Math.Sin(state_L2) - l2 * Math.Sin(state_L2 + state_L5);
            d = -l1 * Math.Sin(state_L2 + state_L5);

            J_norumu = (a * d) - (b * c);

            u_L2 = (1 / J_norumu)  * (d - b) * dot_x;
            u_L5 = (1 / J_norumu) * (-c + a) * dot_y;

            pos_x2L = l1 * Math.Sin(state_L2) + l2 * Math.Sin(state_L2 + state_L5);
            pos_y2L = l1 * Math.Cos(state_L2) + l2 * Math.Cos(state_L2 + state_L5);

            Console.WriteLine("position:x:{0}, y:{1}", pos_x2L, pos_y2L);
        }

/*        public static void Strictlinear_3Link(double x1, double x2, double x3, ref double r_x, ref double r_y, ref double r_z, ref double u_L2, ref double u_L5, ref double u_spine, IController Pad)
        {

            if (-0.05 >= r_x && Pad.RightThumbX * 0.001 < 0) dot_r_x = 0;
            else if (0.16 <= r_x && Pad.RightThumbX * 0.001 > 0) dot_r_x = 0;
            else dot_r_x = Pad.RightThumbX * 0.001;

            if (0.25 >= r_y && Pad.RightThumbY * 0.001 < 0) dot_r_y = 0;
            else if (0.3 <= r_y && Pad.RightThumbY * 0.001 > 0) dot_r_y = 0;
            else dot_r_y = Pad.RightThumbY * 0.001;

            if (-0.25 >= r_z && Pad.LeftTrigger * -0.001 < 0) dot_r_z = 0;
            else if (0.35 <= r_z && Pad.RightTrigger * 0.001 > 0) dot_r_z = 0;
            else dot_r_z = (Pad.RightTrigger * 0.001) + (Pad.LeftTrigger * -0.001);

            r_x += dot_r_x;
            r_y += dot_r_y;
            r_z += dot_r_z;

            a11 = -l1 * Math.Cos(x1) * Math.Sin(x3) - l2 * Math.Cos(x1 + x2) * Math.Sin(x3);
            a12 = -l2 * Math.Cos(x1 + x2) * Math.Sin(x3);
            a13 = -l1 * Math.Sin(x1) * Math.Cos(x3) - l2 * Math.Sin(x1 + x2) * Math.Cos(x3) - l3 * Math.Sin(x3);

            a21 = l1 * Math.Cos(x1) * Math.Cos(x3) + l2 * Math.Cos(x1 + x2) * Math.Cos(x3);
            a22 = l2 * Math.Cos(x1 + x2) * Math.Cos(x3);
            a23 = -l1 * Math.Sin(x1) * Math.Sin(x3) - l2 * Math.Sin(x1 + x2) * Math.Sin(x3) + l3 * Math.Cos(x3);

            a31 = -l1 * Math.Sin(x1) - l2 * Math.Sin(x1 + x2);
            a32 = -l2 * Math.Sin(x1 + x2);
            a33 = 0;

            N_norumu = a12 * a23 * a31 + a13 * a21 * a32 - a13 * a22 * a31 - a11 * a23 * a32;

            x_pos = -l1 * Math.Sin(x1) * Math.Sin(x3) - l2 * Math.Sin(x1 + x2) * Math.Sin(x3) + l3 * Math.Cos(x3);
            y_pos = l1 * Math.Sin(x1) * Math.Cos(x3) + l2 * Math.Sin(x1 + x2) * Math.Cos(x3) + l3 * Math.Sin(x3);
            z_pos = l1 * Math.Cos(x1) + l2 * Math.Cos(x1 + x2);

            Console.WriteLine("position:x:{0}, y:{1}, z:{2}", x_pos, y_pos, z_pos);

            gamma_x = x_pos - r_x;
            gamma_y = y_pos - r_y;
            gamma_z = z_pos - r_z;

            u_L2 = (1 / N_norumu) * (-K_v3 * (gamma_x * -(a23 * a32) + gamma_y * (a13 * a32) + gamma_z * (a12 * a23 - a13 * a22)) + dot_r_x);
            u_L5 = (1 / N_norumu) * (-K_v3 * (gamma_x * (a23 * a31) + gamma_y * -(a13 * a31) + gamma_z * -(a11 * a23 - a13 * a21)) + dot_r_y);
            u_spine = (1 / N_norumu) * (-K_v3 * (gamma_x * (a21 * a32 - a22 * a31) + gamma_y * -(a11 * a32 - a12 * a31) + gamma_z * (a11 * a22 - a12 * a21)) + dot_r_z);


        }*/

    }


}

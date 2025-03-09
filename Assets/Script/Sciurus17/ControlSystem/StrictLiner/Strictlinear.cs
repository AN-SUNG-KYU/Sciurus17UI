using System;
using Sciurus17.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.ControlSystem
{
   

    public static class Strictlinear
    { 
        private static double l1 = 0.25, l2 = 0.25, l3 = 0.17;
        private static double x_pos, y_pos, z_pos, N_norumu, gamma_x, gamma_y, gamma_z, K_v2 = 2.5, K_v3 = 2.5,a, b, c, d;
        private static double a11, a12, a13, a21, a22, a23, a31, a32, a33;

        private static double dot_r_x = 0.0, dot_r_y = 0.0, dot_r_z = 0.0;

        private static double world_xx, world_xy, world_xz, world_yx, world_yy, world_yz, world_zx, world_zy, world_zz; 

        public static void Strictlinear_2Link(double state_L2, double state_L5, ref double r_x, ref double r_y, double dot_r_x, double dot_r_y, ref double u_L2, ref double u_L5)
        {
            r_x += dot_r_x;
            r_y += dot_r_y;

            ///2×2行列
            a =  -l1 * Math.Sin(state_L2) - l2 * Math.Sin(state_L2 + state_L5);
            b =  -l1 * Math.Sin(state_L2 + state_L5);
            c = l1 * Math.Cos(state_L2) + l2 * Math.Cos(state_L2 + state_L5);
            d = l1 * Math.Cos(state_L2 + state_L5);
            N_norumu = (a*d) - (b*c);

            x_pos = l1 * Math.Cos(state_L2) + l2 * Math.Cos(state_L2 + state_L5);
            y_pos = l1 * Math.Sin(state_L2) + l2 * Math.Sin(state_L2 + state_L5);
            gamma_x = x_pos - r_x;
            gamma_y = y_pos - r_y;

/*            u_L2 = (1/N_norumu) * -K_v2 * ((gamma_x + dot_x) * d -(gamma_y + dot_y) * b);
            u_L5 = (1/N_norumu) * -K_v2 * ((-gamma_x + dot_x) * c + (gamma_y + dot_y) * a);
*/
            
            u_L2 = (1 / N_norumu) * -K_v2 * (gamma_x * d - gamma_y * b);
            u_L5 = (1 / N_norumu) * -K_v2 * (-gamma_x * c + gamma_y * a);
            Console.WriteLine("dot:x:{0}, y:{1}", dot_r_x, dot_r_y);
            Console.WriteLine("position:x:{0}, y:{1}", x_pos, y_pos);
        }

        public static void Strictlinear_3Link(double x1, double x2, double x3, ref double r_x, ref double r_y, ref double r_z, ref double dot_r_x, ref double dot_r_y, ref double dot_r_z, ref double u_L2, ref double u_L5, ref double u_spine)
        {

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

            world_xx = -Math.Cos(x1 + x2) * Math.Sin(x3);
            world_xy = Math.Cos(x1 + x2) * Math.Cos(x3);
            world_xz = Math.Sin(x1 + x2);

            Console.WriteLine("position:x:{0}, y:{1}, z:{2}", world_xx, world_xy, world_xz);

            gamma_x = x_pos - r_x;
            gamma_y = y_pos - r_y;
            gamma_z = z_pos - r_z;

            u_L2 = (1 / N_norumu) * -K_v3 * (gamma_x * -(a23 * a32) + gamma_y * (a13 * a32) + gamma_z * (a12 * a23 - a13 * a22));
            u_L5 = (1 / N_norumu) * -K_v3 * (gamma_x * (a23 * a31) + gamma_y * -(a13 * a31) + gamma_z * -(a11 * a23 - a13 * a21));
            u_spine = (1 / N_norumu) * -K_v3 * (gamma_x * (a21 * a32 - a22 * a31) + gamma_y * -(a11 * a32 - a12 * a31) + gamma_z * (a11 * a22 - a12 * a21));

        }

        public static void Strictlinear_3Link_demo(double x1, double x2, double x3, ref double r_x, ref double r_y, ref double r_z, ref double dot_r_x, ref double dot_r_y, ref double dot_r_z, ref double u_L2, ref double u_L5, ref double u_spine)
        {

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

            u_L2 = (1 / N_norumu) * (-K_v3 * ((gamma_x + dot_r_x) * -(a23 * a32) + (gamma_y + dot_r_y) * (a13 * a32) + (gamma_z + dot_r_z) * (a12 * a23 - a13 * a22)));
            u_L5 = (1 / N_norumu) * (-K_v3 * ((gamma_x + dot_r_x) * (a23 * a31) + (gamma_y + dot_r_y) * -(a13 * a31) + (gamma_z + dot_r_z) * -(a11 * a23 - a13 * a21)));
            u_spine = (1 / N_norumu) * (-K_v3 * ((gamma_x + dot_r_x) * (a21 * a32 - a22 * a31) + (gamma_y + dot_r_y) * -(a11 * a32 - a12 * a31) + (gamma_z + dot_r_z) * (a11 * a22 - a12 * a21)));


        }
    }
}

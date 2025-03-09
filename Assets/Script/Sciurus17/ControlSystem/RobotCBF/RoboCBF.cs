using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sciurus17.Dynamixel.Converter.SimpleConvert;


namespace Sciurus17.RobotCBF
{
    public class Robot_CBF
    {
        //0731追加
        double l1 = 0.25, l2 = 0.25, l3 = 0.135;
        double u1, u2, u3;
        double Lgh1, Lgh2, h, h_, x_, x_theta1, x_theta2, a1, b1, a2, b2, h_a1, h_b1, h_a2, h_b2, a2_theta2, b2_theta2, a1_theta1, b1_theta1, sigma1, sigma2;
        double I, I_, J, J_;
        double gamma1 = 0.5, gamma2 = 0.3, pob = 0.405, qob = -0.135, epsilon = 0.1, W1 = 0.200;
        double Kp = 0.5;
        double theta1;

        //Link
        double theta;
        double theta_max = 0, theta_min = 0;

        public double kawai_Link2_CBF(double state, double input)
        {

            theta1 = state;
            sigma1 = (pob - l1 * Math.Cos(theta1)) * (pob - l1 * Math.Cos(theta1)) + (qob - l1 * Math.Sin(theta1)) * (qob - l1 * Math.Sin(theta1)) - W1 * W1;
            sigma2 = 2 * l1 * Math.Sin(theta1) * (pob - l1 * Math.Cos(theta1)) - 2 * l1 * Math.Cos(theta1) * (qob - l1 * Math.Sin(theta1));

            h = sigma1 / (1 + epsilon * sigma1);

            Lgh1 = sigma2 / (epsilon * sigma1 + 1) - epsilon * sigma1 * sigma2 / ((epsilon * sigma1 + 1) * (epsilon * sigma1 + 1));

            I = Lgh1 * input;
            J = -gamma1 * h;
            if (I - J >= 0)
            {
                u1 = 0;
            }
            else
            {
                u1 = -((I - J) / (Lgh1 * Lgh1)) * Lgh1;
            }

            return u1;
        }

        public double protection_degree_CBF(double state, double input, byte Link_number)
        {

            theta = state;
            max_min_degree(Link_number, ref theta_max, ref theta_min);


            h = -(theta - theta_min) * (theta - theta_max);
            Lgh1 = -2 * theta + (theta_max + theta_min);

            I = Lgh1 * input;
            J = -gamma1 * h;
            if (I - J >= 0)
            {
                u1 = 0;
            }
            else
            {
                u1 = -((I - J) / (Lgh1 * Lgh1)) * Lgh1;
            }

            return u1;
        }

        void max_min_degree(byte Link_number, ref double theta_max, ref double theta_min)
        {
            switch (Link_number)
            {
                case 2:
                    theta_max = ConvertDegIntoRad(90);
                    theta_min = ConvertDegIntoRad(0);
                    break;
                case 3:
                    theta_max = ConvertDegIntoRad(0);
                    theta_min = ConvertDegIntoRad(-90);
                    break;
                case 4:
                    theta_max = ConvertDegIntoRad(60);
                    theta_min = ConvertDegIntoRad(-40);
                    break;
                case 5:
                    theta_max = ConvertDegIntoRad(157.5);
                    theta_min = ConvertDegIntoRad(50);
                    break;
                case 6:
                    theta_max = ConvertDegIntoRad(90);
                    theta_min = ConvertDegIntoRad(-90);
                    break;
                case 7:
                    theta_max = ConvertDegIntoRad(60);
                    theta_min = ConvertDegIntoRad(-120);
                    break;
                case 8:
                    theta_max = ConvertDegIntoRad(160);
                    theta_min = ConvertDegIntoRad(-160);
                    break;
                case 9:
                    theta_max = ConvertDegIntoRad(85);
                    theta_min = ConvertDegIntoRad(-5);
                    break;

                case 18:
                    theta_max = ConvertDegIntoRad(70);
                    theta_min = ConvertDegIntoRad(-70);
                    break;
                default:
                    Console.WriteLine("Link_number noting");
                    break;
            }
        }


    }
}

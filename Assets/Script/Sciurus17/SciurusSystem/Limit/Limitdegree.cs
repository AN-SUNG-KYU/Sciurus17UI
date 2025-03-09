using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.SciurusSystem.Limit
{
    public static class Limitdegree
    {
        private static double deg_max;
        private static double deg_min;

        public static bool judgedegree(byte Link_number, double deg)
        {
            max_min_degree(Link_number, ref deg_max, ref deg_min);
            if (deg < deg_min || deg > deg_max)
            {
                Console.WriteLine();
                Console.WriteLine("Link{0}:{1}度 危険領域に入りました", Link_number, deg);
                return false;
            }
            else return true;
        }

        private static void max_min_degree(byte Link_number, ref double deg_max, ref double deg_min)
        {
            switch (Link_number)
            {
                ////RightArm角度保護範囲
                case 2:
                    deg_max = 150;
                    deg_min = -60;
/*                    deg_max = 500;
                    deg_min = -500;*/

                    break;
                case 3:
                    deg_max = 90;
                    deg_min = -92;
                    break;
                case 4:
                    deg_max = 150;
                    deg_min = -150;
                    break;
                case 5:
                    deg_max = 157.5;
                    deg_min = 5;
                    break;
                case 6:
                    deg_max = 150;
                    deg_min = -150;
                    break;
                case 7:
                    deg_max = 60;
                    deg_min = -120;
                    break;
                case 8:
                    deg_max = 160;
                    deg_min = -160;
                    break;
                case 9:
                    deg_max = 85;
                    deg_min = -5.5;
                    break;

                ////LeftArm角度保護範囲
                case 10:
                    deg_max = 100;
                    deg_min = -100;
                    break;
                case 11:
                    deg_max = 92;
                    deg_min = -90;
                    break;
                case 12:
                    deg_max = 150;
                    deg_min = -150;
                    break;
                case 13:
                    deg_max = 0;
                    deg_min = -180;
                    break;
                case 14:
                    deg_max = 150;
                    deg_min = -150;
                    break;
                case 15:
                    deg_max = 120;
                    deg_min = -60;
                    break;
                case 16:
                    deg_max = 160;
                    deg_min = -160;
                    break;
                case 17:
                    deg_max = 5;
                    deg_min = -85;
                    break;
                ////Spin角度保護範囲
                case 18:
                    deg_max = 70;
                    deg_min = -70;
                    break;
                case 19:
                    deg_max = 160;
                    deg_min = -160;
                    break;
                case 20:
                    deg_max = 90;
                    deg_min = -90;
                    break;

                default:
                    Console.WriteLine("Link_number {0} noting", Link_number);
                    break;
            }
        }
    }
}

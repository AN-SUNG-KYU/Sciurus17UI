using System;
using System.Linq;


namespace Sciurus17.Dynamixel.Converter
{
    public static class SimpleConvert
    {
        /// <summary>
        /// 度をラジアンに変換する関数
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static double ConvertDegIntoRad(double deg)
        {
            return deg / 180.0 * Math.PI;
        }
        /// <summary>
        /// 度をラジアンに変換する関数
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static double[] ConvertDegIntoRad(double[] deg)
        {
            return deg.Select(i => i / 180.0 * Math.PI).ToArray();
        }
        /// <summary>
        /// ラジアンを度に変換する関数
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static double ConvertRadIntoDeg(double rad)
        {
            return rad / Math.PI * 180.0;
        }
        /// <summary>
        /// ラジアンを度に変換する関数
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>

        public static double[] ConverRadIntoDeg(double[] rad)
        {
            return rad.Select(i => i / Math.PI / 180.0).ToArray();
        }


        public static double ConvertValueIntoPosition(int value)
        {
            //[deg]
            return (0.088*value) - 180;//value : 0.088 deg
        }
        public static double[] ConvertValueIntoPosition(int[] value)
        {
            return value.Select(i => (0.088 * i) - 180).ToArray();
        }

        public static double ConvertValueIntoVelocity(int value)
        {
            //[rad/sec]
            return value * 0.229 * Math.PI / 30;
        }
        public static double[] ConvertValueIntoVelocity(int[] value)
        {
            return value.Select(i => i * 0.229 * Math.PI / 30).ToArray();
        }

        public static double ConvertValueIntoCurrent(int value)
        {
            //[A]
            return value * 2.69 * Math.Pow(10, -3);
        }
        public static double[] ConvertValueIntoCurrent(int[] value)
        {
            return value.Select(i => i * 2.69 * Math.Pow(10, -3)).ToArray();
        }


        public static int ConvertTorqueIntoValue(double tau, double kt)
        {
            return (int)Math.Round(tau / kt * Math.Pow(10, 3) / 2.69);
        }
        public static int[] ConvertTorqueIntoValue(double[] tau, double[] kt)
        {
            var val = new int[tau.Length];
            for (int i = 0; i < tau.Length; i++)
            {
                val[i] = (int)Math.Round(tau[i] / kt[i] * Math.Pow(10, 3) / 2.69);
            }

            return val;
        }

        public static int[] ConvertRadIntoRpm(double[] value)
        {
            //[rad/sec]
            var val = new int[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                val[i] = (int)Math.Round(value[i] / 0.229 * 30 / Math.PI);//(int)Math.Round(tau[i] / kt[i] * Math.Pow(10, 3) / 2.69);
            }
            return val;
        }


        public static int ConvertCurrentIntoValue(double value)
        {
            return (int)Math.Round(value / (2.69 * Math.Pow(10, -3)));
        }
        public static int[] ConvertCurrentIntoValue(double[] value)
        {
            return value.Select(i => (int)Math.Round( i / (2.69 * Math.Pow(10, -3)))).ToArray();
        }

        public static int ConvertVelocityIntoValue(double value)
        {
            return (int)Math.Round(30 * value / (0.229 * Math.PI));  
        }

        public static int[] ConvertVelocityIntoValue(double[] value)
        {
            return value.Select(i => (int)Math.Round(30 * i / (0.229 * Math.PI))).ToArray();
        }

        public static int ConvertPositionIntoValue(double value)///-180°～180°まで
        {
            return (int)Math.Round(4096 * (value + 180 ) / 360);
        }

        public static int[] ConvertPositionIntoValue(double[] value)///-180°～180°まで
        {
            return value.Select(i => (int)Math.Round(4096 * (i + 180) / 360)).ToArray();
        }

     


    }
}

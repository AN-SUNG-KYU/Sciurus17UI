//using System;
//using System.Collections.Generic;
//using System.Text;
//using TUSWHILLCR;

//namespace WHILL_D435i
//{
//    public static class CBF
//    {
//        public static sbyte J_F, J_T, A_F, A_T;
//        public static double uh_f, uh_t, ua_f, ua_t, u_f, u_t, Theta, theta, R_f0, R_f1, R_f2, R_edge;
//        public static double L1, L2, r1, r2, l, D, K_f, C_f, K_t, C_t;
//        public static double X0, X1, X2, X3, X4, Y0, Y1, Y2, Y3, Y4;
//        public static double B, B1, B2, B3, B4;
//        public static double By1, By2, By3, By4, Bz1, Bz2, Bz3, Bz4;
//        public static double LfB, LfB1, LfB2, LfB3, LfB4, LgBf, LgBf1, LgBf2, LgBt, LgBt1, LgBt2;
//        public static double I_f, I_t, J_f, J_t;
//        public static double a, b, x_i, y_i, xy_i, x_i2, d;
//        public static int n;

//        public static double[] X = new double[33];
//        public static double[] Y = new double[33];
//        public static double[] Z = new double[33];
//        public static double[] T = new double[5];

//        //public static string time;

//        public static void CBF_0()
//        {
//            L1 = 1.0;
//            L2 = 0.61;
//            r1 = 0.24;
//            r2 = 0.15;
//            l = 0.55;

//            D = 0.25;

//            K_f = 0.5;
//            C_f = 0.01;

//            K_t = 2.0;
//            C_t = 10.0;
//        }
//        public static void CBF_F()
//        {
//            uh_f = Convert.ToDouble(J_F);

//            if (uh_f > 30.0)
//            {
//                uh_f = 30.0;
//            }
//            if (uh_f < -30.0)
//            {
//                uh_f = -30.0;
//            }

//            //CBF_edge();
//            //CBF0();
//            CBF1();
//            //CBF2();

//            u_f = uh_f + ua_f;
//            A_F = Convert.ToSByte(u_f);
//        }

//        public static void CBF_edge()
//        {
//            R_edge = (-realsense.e + 0.8);

//            I_f = uh_f / (80.0 * Math.Pow(R_edge, 2.0));
//            J_f = -K_f / R_edge + C_f;

//            if (I_f > J_f)
//            {
//                ua_f = -uh_f - 80.0 * K_f * R_edge + 80.0 * C_f * Math.Pow(R_edge, 2.0);
//            }
//            if (J_f >= I_f)
//            {
//                ua_f = 0;
//            }
//        }

//        public static void CBF0()
//        {
//            R_f0 = (-realsense.r0 + 0.26);
//            I_f = uh_f / (80.0 * Math.Pow(R_f0, 2.0));
//            J_f = -K_f / R_f0 + C_f;

//            if (I_f > J_f)
//            {
//                ua_f = -uh_f - 80.0 * K_f * R_f0 + 80.0 * C_f * Math.Pow(R_f0, 2.0);
//            }
//            if (J_f >= I_f)
//            {
//                ua_f = 0;
//            }
//        }

//        public static void CBF1()
//        {
//            R_f1 = (-realsense.r1 + 0.26);
//            I_f = uh_f / (80.0 * Math.Pow(R_f1, 2.0));
//            J_f = -K_f / R_f1 + C_f;

//            if (I_f > J_f)
//            {
//                ua_f = -uh_f - 80.0 * K_f * R_f1 + 80.0 * C_f * Math.Pow(R_f1, 2.0);
//            }
//            if (J_f >= I_f)
//            {
//                ua_f = 0;
//            }
//        }

//        public static void CBF2()
//        {
//            R_f2 = (-realsense.r2 + 0.26);
//            I_f = uh_f / (80.0 * Math.Pow(R_f2, 2.0));
//            J_f = -K_f / R_f0 + C_f;

//            if (I_f > J_f)
//            {
//                ua_f = -uh_f - 80.0 * K_f * R_f2 + 80.0 * C_f * Math.Pow(R_f2, 2.0);
//            }
//            if (J_f >= I_f)
//            {
//                ua_f = 0;
//            }
//        }

//        public static void Least_Squares()
//        {
//            n = 0;

//            x_i = 0;
//            y_i = 0;
//            xy_i = 0;
//            x_i2 = 0;

//            for (int i = 0; i < 5;i++)
//            {
//                if (realsense.index1 - 2 + i >= 0 && realsense.index1 - 2 + i < 33)
//                {
//                    if (realsense.RES1[realsense.index1 - 2 + i] < 10)
//                    {
//                        T[n] = Math.PI / 4.0 * ((realsense.index1 - 2 + i) / 17.0 - 1.0);
//                        X[n] = realsense.RES1[realsense.index1 - 2 + i] * Math.Cos(T[n]);
//                        Y[n] = realsense.RES1[realsense.index1 - 2 + i] * Math.Sin(T[n]);
//                        n++;
//                    }
//                }
//            }
//            for(int j = 1; j <= n; j++)
//            {
//                x_i += X[j - 1];
//                y_i += Y[j - 1];
//                xy_i += X[j - 1] * Y[j - 1];
//                x_i2 += Math.Pow(X[j - 1], 2.0);
//            }
//            a = (n * xy_i - x_i * y_i) / (n * x_i2 - Math.Pow(x_i, 2.0));
//            b = (x_i2 * y_i - xy_i * x_i) / (n * x_i2 - Math.Pow(x_i, 2.0));

//            d = Math.Abs(b) / Math.Sqrt(Math.Pow(a, 2.0) + 1);
//        }

//        public static void CBF_T()
//        {
//            uh_t = Convert.ToDouble(J_T);

//            if (uh_t > 30.0)
//            {
//                uh_t = 30.0;
//            }
//            if (uh_t < -30.0)
//            {
//                uh_t = -30.0;
//            }

//            Theta = Math.PI / 4.0 * ((realsense.index1 + 1.0) / 17.0 - 1.0);

//            //x2 = realsense.R * Math.Tan(Theta);
//            //y2 = realsense.R - 0.25;

//            X0 = 0;
//            Y0 = 0;

//            X1 = X0 + (L1 - r1) * (Math.Cos(Theta)) + l / 2 * (-Math.Sin(Theta));
//            Y1 = Y0 + (L1 - r1) * (Math.Sin(Theta)) + l / 2 * (Math.Cos(Theta));

//            X2 = X0 + (L1 - r1) * (Math.Cos(Theta)) + l / 2 * (Math.Sin(Theta));
//            Y2 = Y0 + (L1 - r1) * (Math.Sin(Theta)) + l / 2 * (-Math.Cos(Theta));

//            X3 = X0 + r1 * (-Math.Cos(Theta)) + l / 2 * (-Math.Sin(Theta));
//            Y3 = Y0 + r1 * (-Math.Sin(Theta)) + l / 2 * (Math.Cos(Theta));

//            X4 = X0 + r1 * (-Math.Cos(Theta)) + l / 2 * (Math.Sin(Theta));
//            Y4 = Y0 + r1 * (-Math.Sin(Theta)) + l / 2 * (-Math.Cos(Theta));

//            Least_Squares();

//            CBF_R();

//            B = B1 + B2;

//            LfB = LfB1 + LfB2;
//            LgBt = LgBt1 + LgBt2;
            
//            I_t = LfB + LgBt * uh_t;
//            J_t = K_t * B + C_t;
            
//            if (I_t > J_t)
//            {
//                ua_t = -(I_t - J_t) / LgBt;
//            }
//            if (J_t >= I_t)
//            {
//                ua_t = 0;
//            }

//            u_t = uh_t + ua_t;
//            A_T = Convert.ToSByte(u_t);

//            Console.WriteLine("R " + realsense.R1 + " Theta " + CBF.Theta + " u_t " + CBF.u_t + " ua_t " + CBF.ua_t);
//            Console.WriteLine("index " + realsense.index1 + " d " + CBF.d + " a " + CBF.a + " b " + CBF.b + " n " + n);
//            //Console.WriteLine(" X[0] " + CBF.X[0] + " Y[0] " + CBF.Y[0] + " X[1] " + CBF.X[1] + " Y[1] " + CBF.Y[1] + " X[2] " + CBF.X[2] + " Y[2] " + CBF.Y[2]);
//            //Console.WriteLine(" X[3] " + CBF.X[3] + " Y[3] " + CBF.Y[3] + " X[4] " + CBF.X[4] + " Y[4] " + CBF.Y[4] + " n " + CBF.n);
            
//            //time = Convert.ToString(Program.sw.ElapsedMilliseconds);
//            //Program.writer1.WriteLine(time + "," + CBF.uh_t + "," + CBF.ua_t + "," + CBF.u_t + "," +  CBF.d + "," + CBF.Theta);
//            //Program.writer2.WriteLine(time + "," + a + "," + b + "," + CBF.X[0] + "," + CBF.X[1] + "," + CBF.X[2] + "," + CBF.X[3] + "," + CBF.X[4] + "," + CBF.Y[0] + "," + CBF.Y[1] + "," + CBF.Y[2] + "," + CBF.Y[3] + "," + CBF.Y[4]);
//        }

//        public static void CBF_L()
//        {
//            B1 = Math.Pow(Theta, 2.0) / 1000.0 + 1.0 / d;
//            By1 = - 1.0 / Math.Pow(d, 2.0);
//            Bz1 = 2.0 * Math.Abs(Theta) / 1000.0;
//            LfB1 = By1 * ((L1 - r1) * (Math.Cos(Theta)) + l / 2.0 * (-Math.Sin(Theta)));
//            //LgBf1 = By1 * Math.Sin(Theta);
//            LgBt1 = Bz1;
//        }

//        public static void CBF_R()
//        {
//            B2 = Math.Pow(Theta, 2.0) / 1.0 + 1.0 / d;
//            By2 = 1.0 / Math.Pow(d, 2.0);
//            Bz2 = -2.0 * Theta / 1.0;
//            LfB2 = By2 * ((L1 - r1) * (Math.Cos(Theta)) + l / 2.0 * (-Math.Sin(Theta)));
//            //LgBf2 = By2 * Math.Sin(Theta);
//            LgBt2 = Bz2;
//        }
//    }
//}

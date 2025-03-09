using System;
//using Intel.RealSense;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace WHILL_D435i
{
    public static class realsense
    {
        //public static string res = "10.000000";
        //public static double sec;
        //public static string res_1 = "10.000000";
        //public static bool flag = true;

        //public static int[] WIDTH = new int[33];
        //public static int[] HEIGHT = new int[3];
        //public static double[] Res0 = new double[33];
        //public static double[] Res1 = new double[33];
        //public static double[] Res2 = new double[33];
        //public static double[] RES0 = new double[33];
        //public static double[] RES1 = new double[33];
        //public static double[] RES2 = new double[33];
        //public static double[] x0 = new double[33];
        //public static double[] x1 = new double[33];
        //public static double[] x2 = new double[33];
        public static bool end2 = true;


        //public static int[] height = new int[200];
        //public static double[] edge = new double[200];
        //public static double[] thetaK = new double[200];
        //public static double[] y = new double[200];
        //public static double[] Ye = new double[200];
        //public static double[] Ze = new double[200];
        //public static double[] Ey = new double[200];
        //public static double[] Ez = new double[200];
        //public static double[] ln = new double[200];
        //public static double[] ary = new double[200];
        //public static int[] ary2 = new int[200];

        //public static double e, e2, R0, R1, R2, r0, r1, r2, theta;

        //public static int e1, i, j, k, l, m, kL, le, lL, I, J, K, index0, index1, index2, i0, i1, i2;

        //public static Stopwatch sw = new Stopwatch();
        //static System.Text.Encoding encoding = System.Text.Encoding.ASCII;

        //public static Pipeline pipe = new Pipeline();
        //public static Config config = new Config();

        //public static DepthFrame frame;

        //public static System.IO.StreamWriter writer3;


        //public static void Start_P()
        //{
        //    config.EnableStream(Stream.Depth, 848, 480, Format.Z16, 90);
        //    pipe.Start(config);
        //    writer3 = new System.IO.StreamWriter("Sys3_.txt", false);
        //}


        //public static void Start_D()
        //{
        //    const int CAPACITY = 1; // allow max latency of 5 frames
        //    var queue = new FrameQueue(CAPACITY);
        //    Task.Run(() =>
        //    {
        //        while (end2)
        //        {
        //            if (queue.PollForFrame(out frame))
        //            {
        //                using (frame)
        //                {
        //                    W33H3();
        //                    //W1H200();

        //                    if (flag)
        //                    {
        //                        sw.Start();
        //                        flag = false;
        //                    }

        //                    Thread.Sleep(1);
        //                }
        //            }
        //        }
        //    });

        //    while (end2)
        //    {
        //        using (var frames = pipe.WaitForFrames())
        //        using (var depth = frames.DepthFrame)
        //            queue.Enqueue(depth);
        //    }

        //}

        //public static void W1H200()
        //{
        //    kL = 0;

        //    for (K = 0; K < 200; K++)
        //    {
        //        height[K] = 241 + K;
        //        edge[K] = frame.GetDistance(424, height[K]);
        //        if (edge[K] == 0)
        //        {
        //            edge[K] = 10.0;
        //        }
        //        if (edge[K] != 10.0)
        //        {
        //            ary[kL] = edge[K];
        //            ary2[kL] = K;
        //            kL++;
        //        }
        //    }

        //    for (m = 0; m < kL; m++)
        //    {
        //        thetaK[m] = Math.Tan(Math.PI / 6.0 * (1.0 - (height[ary2[m]] + 1.0) / 242.0));
        //        y[m] = ary[m] * thetaK[m];

        //        //Console.WriteLine(ary2[m] + "," + y[m] + "," + ary[m]);
        //    }

        //    lL = kL - 1;
        //    le = 0;
        //    for (l = 0; l < lL; l++)
        //    {
        //        Ye[l] = y[l + 1] - y[l];
        //        Ze[l] = ary[l + 1];

        //        if (Ye[l] > 0.01 || Ye[l] < -0.01)
        //        {
        //            //Console.WriteLine(l + " Ye " + Ye[l] + " Ze " + Ze[l]);
        //            ln[le] = l;
        //            Ey[le] = y[l];
        //            Ez[le] = Ze[l];
        //            le++;
        //            //Console.WriteLine("le" + le + "Ye" + Ey[le] + " Ze " + Ez[le]);
        //        }
        //        if(le == 0)
        //        {
        //            Ez[0] = 10.0;
        //            le = 1;
        //        }
        //        //writer3.WriteLine(realsense.Ye[l] + "," + realsense.ary[l]);
        //    }

        //    e = Ez.Take(le).Min();

        //    //Console.WriteLine(Ez[0] + " " + Ez[1] + " " + Ez[2] + " e " + e);
        //    e1 = Array.FindIndex(Ez, n => n == e);
        //    e2 = Ey[e1];
        //}

        //public static void W33H3()
        //{
        //    for (i = 0; i < 33; i++)
        //    {
        //        WIDTH[i] = 4 + 26 * i;
        //    }
        //    for (j = 0; j < 3; j++)
        //    {
        //        HEIGHT[j] = 30 + 180 * j;
        //    }

        //    for (int I = 0; I < 33; I++)
        //    {
        //        Res0[I] = frame.GetDistance(WIDTH[I], HEIGHT[0]);
        //        Res1[I] = frame.GetDistance(WIDTH[I], HEIGHT[1]);
        //        Res2[I] = frame.GetDistance(WIDTH[I], HEIGHT[2]);
        //        if (Res0[I] == 0)
        //        {
        //            Res0[I] = 10.0f;
        //        }
        //        if (Res1[I] == 0)
        //        {
        //            Res1[I] = 10.0f;
        //        }
        //        if (Res2[I] == 0)
        //        {
        //            Res2[I] = 10.0f;
        //        }

        //        theta = Math.Tan(Math.PI / 4.0 * ((I + 1.0) / 17.0 - 1.0));

        //        x0[I] = Res0[I] * theta;
        //        x1[I] = Res1[I] * theta;
        //        x2[I] = Res2[I] * theta;

        //        if (x0[I] < -0.5 || x0[0] > 0.5)
        //        {
        //            Res0[I] = 10.0;
        //        }
        //        if (x1[I] < -0.5 || x1[0] > 0.5)
        //        {
        //            Res1[I] = 10.0;
        //        }
        //        if (x2[I] < -0.5 || x2[0] > 0.5)
        //        {
        //            Res2[I] = 10.0;
        //        }

        //        RES0[I] = Res0[I] / theta;
        //        RES1[I] = Res1[I] / theta;
        //        RES2[I] = Res2[I] / theta;
        //    }
        //    R0 = RES0.Min();
        //    R1 = RES1.Min();
        //    R2 = RES2.Min();
        //    index0 = Array.FindIndex(RES0, n => n == R0);
        //    index1 = Array.FindIndex(RES1, n => n == R1);
        //    index2 = Array.FindIndex(RES2, n => n == R2);

        //    r0 = Res0.Min();
        //    r1 = Res1.Min();
        //    r2 = Res2.Min();
        //    i0 = Array.FindIndex(Res0, n => n == r0);
        //    i1 = Array.FindIndex(Res1, n => n == r1);
        //    i2 = Array.FindIndex(Res2, n => n == r2);
        //}
    }
}

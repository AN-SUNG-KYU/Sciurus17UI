using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using D435RSDepth;
using D435Data;

namespace D435XBarrier
{
    class XBarrier
    {
        public double B,h;
        public double Lfh, Lgh;
        public double u, I, J;
        public double G;
        public double origindistance;
        public double tempdistance;

        //設計パラメータ//
        public double L = 0.0001;
        public double a = 0.3;
        public double X = 0.3;
        public double w = 1.0;
        public double A = 0;
        RSDepth rsDepth;
        Vector3 tempdata;

        private double amax = 0.3;
        private double amin = 0.3;
        private double transdistance = 2.0;
        private double mindistance = 1.0;


        public XBarrier(RSDepth rS)
        {
            rsDepth = rS;
        }
        //プロパー項あり//

        public static double sign(double d)
        {
            if (d > 0)
            {
                return 1.0;
            }
            else if (d == 0.0)
            {
                return 0.0;
            }
            else
            {
                return -1.0;
            }
        }

        public double Barrieru(sbyte u_h)
        {
            //origindistance = -1 * rsDepth.distance_data[640*240+320];
            //origindistance = -1 * rsDepth.distance_data2[320, 240];
 /*           origindistance = -1 * rsDepth.distance_data_reduced[32, 24];
            for(int i=0;i<35;i++)
            {
                for(int j= 0;j<30;j++)
                {
                    tempdistance= -1*rsDepth.distance_data_reduced[27+i,j];
                    if (tempdistance <0 && tempdistance>origindistance)
                    {
                        origindistance = tempdistance;
                    }
                }
            }*/

            origindistance = -1* MyData.rsDepth.vertices_reduced[32, 24].Z;
            /*Console.WriteLine(origindistance);*/
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    tempdata = MyData.rsDepth.vertices_reduced[i, j];
                    if (tempdata.Z < 0.3) tempdata.Z = 10;
                    if (tempdata.X > -0.6 && tempdata.X < 0.6 && tempdata.Y > 0 && tempdata.Y < 1.2 && tempdata.Z < -1 * origindistance)
                    {
                        origindistance = -tempdata.Z;
                    }
                    //if (tempdata.Z < -1 * origindistance) { origindistance = -tempdata.Z; }
                }
            }
            /*Console.WriteLine(origindistance);*/
            //origindistance = -1 * rsDepth.Coordinate_datau;
            amax = 10.0;
            amin = 0.7;
            mindistance = 1.2;
            transdistance = 2.0;
            if (origindistance < -mindistance-transdistance)
            {
                a = amax;
            }
            else if(origindistance >-mindistance)
            {
                a = amin; 
            }
            else
            {
                a = amin - (amax-amin) *(origindistance +mindistance)/transdistance;
            }

            G = X + origindistance;
            h = -(G / (1 + L * G * G * Math.Abs(G)));
            Lfh = 0;
            Lgh = -(1 - 2 * L * G * G * Math.Abs(G)) / (80 * (1 + L * G * G * Math.Abs(G)) * (1 + L * G * G * Math.Abs(G)));
            I = Lfh + Lgh * u_h;
            if (G < 0)
            {
                J = -a * Math.Pow(Math.Abs(h), w) * sign(h);
            }
            else
            {
                J = -A * Math.Pow(Math.Abs(h), w) * sign(h);      //aが0でもよい．
            }

            if (I >= J)
            {
                u = 0;
            }
            else if (I < J)
            {
                u = -(I - J) / Lgh;
            }
            //Console.WriteLine("origin={0}, u={1}, u_h={2}", origindistance, u, u_h);
            /*MyData.message = ("orign="+origindistance+", u="+u+", u_h="+u_h);*/
            return u;
            
        }
    }
}


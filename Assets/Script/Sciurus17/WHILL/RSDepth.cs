using System;
using Intel.RealSense;
using System.Numerics;
using System.Threading.Tasks;
using System.Threading;
using D435Data;

namespace D435RSDepth
{
    internal class RSDepth
    {
        public int width, height, framerate, capa;
        public Pipeline pipe;
        public Config config;
        public FrameQueue queue;
        public DepthFrame? depth;
        public PointCloud pc;
        public float[] distance_data;
        public float[,] distance_data2;
        public float[,] distance_data_reduced;
        public float Coordinate_data;
        public ushort[] depth_data;
        public Vector3[] vertices;
        public Vector3[,] vertices_reduced;
        public bool isUsingRealSense;
        public RSDepth()
        {
            width = 640;
            height = 480;

            framerate = 30;
            capa = 1;
            distance_data = new float[width * height];
            distance_data2 = new float[width, height];
            distance_data_reduced = new float[width / 10, height / 10];
            depth_data = new ushort[width * height];
            vertices = new Vector3[width * height];
            vertices_reduced = new Vector3[width /10, height /10];
            pipe = new Pipeline();
            config = new Config();
            
            config.EnableStream(Intel.RealSense.Stream.Depth, width, height, Format.Z16, framerate);
            //config.EnableStream(Stream.Depth, width, height, Format.Any, framerate);
            queue = new FrameQueue(capa);
            pc = new PointCloud();
            isUsingRealSense = true;

            MyData.message = "RSDepth Initializing...";
           
        }
        public void Start()
        {
            pipe.Start(config);
            Console.WriteLine("pipe start");
            MyData.message = "pipe started!";
        }
        public void DoDepth()
        {
            using (var frames = pipe.WaitForFrames())
            using (var depthF = frames.DepthFrame)
            {
                //GetCopyDepth(depthF);
                //GetUnsafeDepth(depthF);
                //Getdistance(depthF);
                //GetPoint(depthF);
                //GetCoordinate(depthF);
            }
        }

        public void PutTask()
        {
            // data取得とdata使用が別スレッド
            var puttask = Task.Run(() =>
            {
                while (isUsingRealSense)
                {
                    PutFrame();
                }
            });
        }

        public void TakeTask()
        {
            var taketask = Task.Run(() =>
            {
                while (isUsingRealSense)
                {
                    TakeFrame();
                    Thread.Sleep(10);
                }
            });
        }

        public void PutFrame()
        {
            using ( var frames = pipe.WaitForFrames())
            using (var depthQ = frames.DepthFrame)
            {
                queue.Enqueue(depthQ);
            }
        }
        public void TakeFrame()
        {
            if (queue.PollForFrame(out depth))
            {
                using (depth)
                {
                    //GetCopyDepth(depth);
                    //GetUnsafeDepth(depth);
                    //Getdistance(depth);
                    //GetPoint(depth);
                    //GetCoordinate(depth);
                    //GetDistance2(depth);
                    //GetDistanceReduced(depth);
                    GetPointReduced(depth);
                }
            }
        }
        public void GetCopyDepth(DepthFrame depthF)
        {
            depthF.CopyTo(depth_data);
        }
        public void Getdistance(DepthFrame depthF)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    distance_data[j + i * width] = depthF.GetDistance(j, i);
                    Console.WriteLine(depthF.GetDistance(width / 2, height / 2));
                }
            }
            Thread.Sleep(0);
        }
        public void GetDistance2(DepthFrame depthF)
        {
            Parallel.For(0, height, i =>
            {
                Parallel.For(0, width, j =>
                {
                    distance_data2[j, i] = depthF.GetDistance(j, i);

                    //Console.WriteLine(depthF.GetDistance(width / 2, height / 2));
                });
            });
            //Console.WriteLine(distance_data2[320, 240]);
        }

        public void GetDistanceReduced(DepthFrame depthF)
        {
            Parallel.For(0, height / 10, i =>
            {
                Parallel.For(0, width / 10, j =>
                {
                    distance_data_reduced[j, i] = depthF.GetDistance(j * 10, i * 10);

                    //Console.WriteLine(depthF.GetDistance(width / 2, height / 2));
                });
            });
            //Console.WriteLine(distance_data2[320, 240]);
        }

        public void GetPoint(DepthFrame depthF)
        {
            using (var point = pc.Process(depthF))
            using (var points = point.As<Points>())
            {
                points.CopyVertices(vertices); //float[size*3]も可,ただし参照しづらい
            }
        }

        public void GetPointReduced(DepthFrame depthF)
        {
            using (var point = pc.Process(depthF))
            using (var points = point.As<Points>())
            {
                points.CopyVertices(vertices);
                Parallel.For(0, height / 10, i =>
                {
                    Parallel.For(0, width / 10, j =>
                    {
                        vertices_reduced[j, i] = vertices[j * 10 + i * 10 * width];
                    });
                });
            }
        }

        public void GetCoordinate(DepthFrame depthF)
        {
            //Coordinate_data = depthF.GetDistance(width / 2, height / 2);
            Coordinate_data = distance_data2[320, 240];
            Console.WriteLine(distance_data2[320, 240]);
            //Console.WriteLine(Coordinate_data);
            Thread.Sleep(0);
        }

        public void Stop()
        {
            isUsingRealSense = false;
            pipe.Stop();
            pipe.Dispose();
        }
    }
}


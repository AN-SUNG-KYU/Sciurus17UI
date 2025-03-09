using System;
using System.Collections;
using System.IO;

namespace Sciurus17.MyLibrary.Matlab
{
    public static class MatlabUtility
    {
        public static void Save(ArrayList a, string dataname, string filename = "DataFile")
        {
            try
            {
                if (!Directory.Exists(filename)) Directory.CreateDirectory(filename);

                //StreamWriter writer = new StreamWriter(filename + @"\" + "data.txt", false);
                StreamWriter writer = new StreamWriter(filename + @"\" + dataname + "data.txt", false);
                //StreamWriter writer = new StreamWriter(filename + @"\" + dataname + "data.m", false);//データの保存方法txt可能？
                //writer.Write(dataname);
                //writer.WriteLine(" = [");
                IEnumerator enumerator = a.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    double[] data = (double[])enumerator.Current;
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.Write(data[i]);
                        writer.Write(",");
                    }
                    writer.WriteLine("");
                }
                //writer.WriteLine("]");
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Save(ArrayList[] data, string[] name , string filename = "DataFile")
        {
            if (data.Length != name.Length) throw new ArgumentException();
            for (int i = 0; i < data.Length; i++)
            {
                Save(data[i], name[i], filename);
            }
        }

    }
}

